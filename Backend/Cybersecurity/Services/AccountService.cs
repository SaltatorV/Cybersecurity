﻿using AutoMapper;
using Cybersecurity.Entities;
using Cybersecurity.Exceptions;
using Cybersecurity.Interfaces.Repositories;
using Cybersecurity.Interfaces.Services;
using Cybersecurity.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;

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

        public AccountService(IGenericRepository<User> userRepository, IPasswordHasher<User> passwordHasher, IPasswordHasher<OldPassword> oldPasswordHasher,
            IMapper mapper, IGenericRepository<Role> roleRepository, IGenericRepository<OldPassword> oldPasswordRepository, ILogService logService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _oldPasswordHasher = oldPasswordHasher;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _oldPasswordRepository = oldPasswordRepository;
            _logService = logService;
        }

        public async Task RegisterUser(RegisterUserDto registerDto)
        {
            var register = _mapper.Map<User>(registerDto);

            var hashedPassword = _passwordHasher.HashPassword(register, register.Password);

            register.Password = hashedPassword;
            register.PasswordExpire = DateTime.UtcNow.AddDays(30);
            register.IsPasswordExpire = false;

            await _userRepository.InsertAsync(register);
            await _userRepository.SaveAsync();
        }

        public async Task<UserDto> LoginUser(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetByPredicateAsync(u => u.Login == loginDto.Login);

            if (user is null)
            {
                await _logService.AddLog($"Nie znaleziono użytkownika {loginDto.Login}", "Logowanie", 0);
                throw new BadRequestException("Niepoprawny login lub hasło");
            }

            var role = await _roleRepository.GetByIdAsync(user.RoleId);

            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

            if (passwordVerification == PasswordVerificationResult.Failed)
            {
                await _logService.AddLog("Wpisane złe hasło", "Logowanie", user.Id);
                throw new BadRequestException("Niepoprawny login lub hasło");
            }

            await _logService.AddLog("Poprawna próba logowania", "Logowanie", user.Id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUser(int id, UpdateUserDto updateDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser is null)
                throw new NotFoundException("User not found");

            var user = _mapper.Map(updateDto, existingUser);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();
        }

        public async Task AdminUpdateUser(int id, AdminUpdateUserDto adminUpdateDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser is null)
                throw new NotFoundException("User not found");

            var user = _mapper.Map(adminUpdateDto, existingUser);

            user.PasswordExpire = DateTime.UtcNow.AddDays(adminUpdateDto.DayExpire);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();
        }

        public async Task ChangePassword(int id, ChangePasswordDto changePasswordDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser is null)
                throw new NotFoundException("Nie znaleziono użytkownika");

            var existingUserOldPassword = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, changePasswordDto.OldPassword);

            if (existingUserOldPassword == PasswordVerificationResult.Failed)
                throw new BadHttpRequestException("Nie poprawne stare hasło");

            var oldPasswords = await _oldPasswordRepository.GetAllAsync();

            foreach (var item in oldPasswords)
            { 
                if(item.UserId == id)
                {
                    var oldPasswordVerification = _oldPasswordHasher.VerifyHashedPassword(item, item.Password, changePasswordDto.Password);

                    if(oldPasswordVerification == PasswordVerificationResult.Success)
                    {
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

        public async Task<AdminUserDto> AdminGetUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var role = await _roleRepository.GetAllAsync();

            var userDto = _mapper.Map<AdminUserDto>(user);

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
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser is null)
                throw new NotFoundException("Nie istnieje taki użytkownik");

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveAsync();
            await Task.CompletedTask;
        }
    }
}