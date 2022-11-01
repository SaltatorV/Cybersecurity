using Projekt_ASP.Controllers;
using Projekt_ASP.Data;
using Projekt_ASP.DTO;
using Projekt_ASP.Interfaces;
using WebApi.Models.DTO;

namespace Projekt_ASP.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtHandler;

        public UserService(IUserRepository userRepository, IJwtTokenGenerator jwtHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
        }

        public async Task ChangePassword(ChangePassword user)
        {
            await _userRepository.ChangePassword(user);

        }

        public async Task DeleteUserLoginAsync(string login)
        {
            await _userRepository.DeleteAsync(login);
           
        }

        public Task<TokenObjectDto> GetAccountByToken(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                throw new Exception("Token is bad!");
            }
            var tokenObject = _jwtHandler.Decode(token);
            return tokenObject;

        }

        public async Task<List<User>> GetAllUser()
        {
            var collection = await _userRepository.GetAll();
            return collection;

        }

        public async Task GetCreateUser(CreateUserDto user)
        {
            await _userRepository.AddAsync(user);
            
        }

        public async Task<Token> LoginAsync(string login, string password)
        {
            var userLogin = await _userRepository.GetAsync(login);
            if (userLogin.Password != PasswordEncryption.Hash(password))
            {
                throw new Exception("Password is bad");

            }
            else if(userLogin.isActive == false)
            {
                throw new Exception("Konto zablokowane!");
            }
            


            var jwt = _jwtHandler.Generate(userLogin.Login, userLogin.Role);


            return jwt;
        }

        public async Task RegisterAsync(CreateUserDto user)
        {


            await _userRepository.AddAsync(user);
        }

        public Task RegisterAsync(string Login, string Paswword, string role)
        {
            throw new NotImplementedException();
        }

        public async Task ZablokujUserAsync(string login)
        {
            await _userRepository.Zablokuj(login);
        }
        public async Task OdblokujUserAsync(string login)
        {
            await _userRepository.Odblokuj(login);
        }

        public async Task OpcjeHasel(OpcjeHaselDto opcje)
        {
            await _userRepository.OpcjeHaselUser(opcje);
        }

        public async Task<bool> CzyWygasloService(string login)
        {
            var res = await _userRepository.CzyWygaslo(login);
            return res;
        }

        public async Task<TokenObjectDto> Verify(string token)
        {
            var jwt = new JwtTokenGenerator();
           var tUser= await jwt.Decode(token);
            
            return tUser;

        }

        public async Task<OptionsDto> ZaIleDniWygasnie(string login)
        {
             var ile = await _userRepository.ZaIleDniWygasnie(login);
            return ile;
        }
    }
}
