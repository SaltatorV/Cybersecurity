using AutoMapper;
using Cybersecurity.Authentication;
using Cybersecurity.Entities;
using Cybersecurity.Exceptions;
using Cybersecurity.Interfaces.Repositories;
using Cybersecurity.Interfaces.Services;
using Cybersecurity.Models.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Cybersecurity.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<OldPassword> _oldPasswordRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IPasswordHasher<OldPassword> _oldPasswordHasher;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IAuthenticationService _authenticationService;
        private readonly IValidator<RegisterUserDto> _registerDtoValidator;
        private readonly IValidator<ChangePasswordDto> _changePasswordDtoValidator;
        private readonly IValidator<UpdateUserDto> _updateDtoValidator;

        public AccountService(IGenericRepository<User> userRepository, IPasswordHasher<User> passwordHasher, IPasswordHasher<OldPassword> oldPasswordHasher,
            IMapper mapper, IGenericRepository<Role> roleRepository, IGenericRepository<OldPassword> oldPasswordRepository, ILogService logService, 
            IHttpContextAccessor accessor, IAuthenticationService authenticationService, IValidator<RegisterUserDto> registerDtoValidator,
            IValidator<ChangePasswordDto> changePasswordDtoValidator, IValidator<UpdateUserDto> updateDtoValidator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _oldPasswordHasher = oldPasswordHasher;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _oldPasswordRepository = oldPasswordRepository;
            _logService = logService;
            _accessor = accessor;
            _authenticationService = authenticationService;
            _registerDtoValidator = registerDtoValidator;
            _changePasswordDtoValidator = changePasswordDtoValidator;
            _updateDtoValidator = updateDtoValidator;
        }

        public async Task RegisterUser(RegisterUserDto registerDto)
        {
            var jwt = _accessor.HttpContext.Request.Cookies["jwt"];

            if (jwt is null)
            {
                throw new CookieNotFoundException("Brak cookie");
            }

            var userId = await _authenticationService.GetIdFromClaim(jwt);
            var validationResult = _registerDtoValidator.Validate(registerDto);

            if (!validationResult.IsValid)
            {
                await _logService.AddLog($"Rejestracja użytkownika {registerDto.Login} nie udała się", "Rejestracja", userId);

                throw new BadRequestException(validationResult.ToString());
            }

            var register = _mapper.Map<User>(registerDto);

            var hashedPassword = _passwordHasher.HashPassword(register, register.Password);

            register.Password = hashedPassword;
            register.PasswordExpire = DateTime.UtcNow.AddDays(30);
            register.IsPasswordExpire = false;

            register.MaxFailLogin = 5;
            register.FailLoginCounter = 0;
            register.IsLoginLockOn = false;

            register.SessionTime = 15;
            
            await _userRepository.InsertAsync(register);
            await _userRepository.SaveAsync();
            await _logService.AddLog($"Rejestracja użytkownika {registerDto.Login}", "Rejestracja", userId);
            await Task.CompletedTask;
        }

        public async Task<int> LoginUser(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetByPredicateAsync(u => u.Login == loginDto.Login);

            if (user is null)
            {
                await _logService.AddLog($"Nie znaleziono użytkownika {loginDto.Login}", "Logowanie", 0);
                throw new BadRequestException("Niepoprawny login lub hasło");
            }

            if (user.IsLoginLockOn)
            {
                throw new ForbiddenException(user.LoginLockOnTime.ToString());
            }

            var role = await _roleRepository.GetByIdAsync(user.RoleId);

            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

            if (passwordVerification == PasswordVerificationResult.Failed)
            {
                user.FailLoginCounter++;

                if (user.FailLoginCounter == user.MaxFailLogin)
                {
                    user.IsLoginLockOn = true;
                    user.FailLoginCounter = 0;
                    user.LoginLockOnTime = DateTime.UtcNow.AddMinutes(15);
                }

                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveAsync();

                await _logService.AddLog("Wpisane złe hasło", "Logowanie", user.Id);
                throw new BadRequestException("Niepoprawny login lub hasło");
            }

            await _logService.AddLog("Poprawna próba logowania", "Logowanie", user.Id);

            if (user.IsPasswordExpire)
            {
                _accessor.HttpContext.Response.Cookies.Append("changePassword", user.Id.ToString(), new CookieOptions { });
                throw new BadRequestException("Należy zmienić hało");
            }

            var token = await _authenticationService.Generate(user.Id, role.Name, user.SessionTime);

            _accessor.HttpContext.Response.Cookies.Append("jwt", token, new CookieOptions {Expires = DateTime.Now.AddMinutes(user.SessionTime)});
            _accessor.HttpContext.Response.Cookies.Append("login", "true", new CookieOptions { Expires = DateTime.Now.AddMinutes(user.SessionTime)});

            return user.RoleId;
        }

        public async Task Logout()
        {
            var jwt = _accessor.HttpContext.Request.Cookies["jwt"];

            if (jwt is null)
            {
                throw new CookieNotFoundException("Brak cookie");
            }

            var userId = await _authenticationService.GetIdFromClaim(jwt);

            _accessor.HttpContext.Response.Cookies.Delete("jwt");
            _accessor.HttpContext.Response.Cookies.Delete("login");

            await _logService.AddLog("Wylogowanie użytkownika", "Wylgowanie", userId);

            await Task.CompletedTask;
        }

        public async Task UpdateUser(int id, UpdateUserDto updateDto)
        {
            var jwt = _accessor.HttpContext.Request.Cookies["jwt"];

            if (jwt is null)
            {
                throw new CookieNotFoundException("Brak cookie");
            }

            var userId = await _authenticationService.GetIdFromClaim(jwt);
            var validationResult = _updateDtoValidator.Validate(updateDto);

            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser is null)
            {
                await _logService.AddLog($"Zmiana danych użytkownika {updateDto.Login} nie udała się", "Edycja", userId);
                throw new NotFoundException("User not found");
            }

            if (!validationResult.IsValid)
            {
                await _logService.AddLog($"Zmiana danych użytkownika {existingUser.Login} nie udała się", "Edycja", userId);
                throw new BadRequestException(validationResult.ToString());
            }

            var user = _mapper.Map(updateDto, existingUser);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();
            await _logService.AddLog($"Zmiana danych użytkownika Id użytkownika: {user.Id}", "Edycja", userId);
            await Task.CompletedTask;
        }

        public async Task ChangePassword(int id, ChangePasswordDto changePasswordDto)
        {
            var jwt = _accessor.HttpContext.Request.Cookies["jwt"];

            if (jwt is null)
            {
                throw new CookieNotFoundException("Brak cookie");
            }

            var userId = await _authenticationService.GetIdFromClaim(jwt);
            var validationResult = _changePasswordDtoValidator.Validate(changePasswordDto);

            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser is null)
            {
                await _logService.AddLog($"Zmiana hasła użytkownika {changePasswordDto.UserId} nie udała się", "Zmiana hasła", userId);
                throw new NotFoundException("Nie znaleziono użytkownika");
            }

            if (!validationResult.IsValid)
            {
                await _logService.AddLog($"Zmiana hasła użytkownika {existingUser.Login} nie udała się", "Zmiana hasła", userId);
                throw new BadRequestException(validationResult.ToString());
            }

            var existingUserOldPassword = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, changePasswordDto.OldPassword);

            if (existingUserOldPassword == PasswordVerificationResult.Failed)
            {
                await _logService.AddLog($"Zmiana hasła użytkownika {existingUser.Login} nie udała się", "Zmiana hasła", userId);
                throw new BadHttpRequestException("Nie poprawne stare hasło");
            }

            var oldPasswords = await _oldPasswordRepository.GetAllAsync();

            foreach (var item in oldPasswords)
            { 
                if(item.UserId == id)
                {
                    var oldPasswordVerification = _oldPasswordHasher.VerifyHashedPassword(item, item.Password, changePasswordDto.Password);

                    if(oldPasswordVerification == PasswordVerificationResult.Success)
                    {
                        await _logService.AddLog($"Zmiana hasła użytkownika {existingUser.Login} nie udała się", "Zmiana hasła", userId);
                        throw new BadRequestException("Hasło było używane");
                    }
                }
            }

            var oldPassword = _mapper.Map<OldPassword>(changePasswordDto);
            var hashedOldPassword = _oldPasswordHasher.HashPassword(oldPassword, oldPassword.Password);

            oldPassword.Password = hashedOldPassword;

            var user = _mapper.Map(changePasswordDto, existingUser);

            user.Password = _passwordHasher.HashPassword(user, user.Password);
            user.IsPasswordExpire = false;
            user.PasswordExpire = DateTime.UtcNow.AddDays(30);

            await _userRepository.UpdateAsync(user);
            await _oldPasswordRepository.InsertAsync(oldPassword);
            await _oldPasswordRepository.SaveAsync();
            await _userRepository.SaveAsync();
            await _logService.AddLog($"Zmiana hasła użytkownika {existingUser.Login}", "Zmiana hasła", userId);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var roles = await _roleRepository.GetAllAsync();

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return usersDto;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var role = await _roleRepository.GetAllAsync();

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllAsync();

            var rolesDto = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return rolesDto;
        }

        public async Task DeleteUser(int id)
        {
            var jwt = _accessor.HttpContext.Request.Cookies["jwt"];

            if(jwt is null)
            {
                throw new CookieNotFoundException("Brak cookie");
            }

            var userId = await _authenticationService.GetIdFromClaim(jwt);

            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser is null)
            {
                await _logService.AddLog($"Usunięcie użytkownika {id} nie udało się", "Usunięcie", userId);
                throw new NotFoundException("Nie istnieje taki użytkownik");
            }

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveAsync();
            await _logService.AddLog($"Usunięcie użytkownika {existingUser.Login}", "Usunięcie", userId);
            await Task.CompletedTask;
        }
    }
}
