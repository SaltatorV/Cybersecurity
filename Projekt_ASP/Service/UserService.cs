using Projekt_ASP.Data;
using Projekt_ASP.Interfaces;

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

        public async Task<List<User>> GetAllUser()
        {
            var collection = await _userRepository.GetAll();
            return collection;
            
        }

        public async Task<Token> LoginAsync(string login, string password)
        {
            var userLogin = await _userRepository.GetAsync(login);
            if(userLogin.Password != password)
            {
                throw new Exception("Password is bad");
            }

            var jwt = _jwtHandler.Generate(userLogin.Login, userLogin.Role);


            return jwt;
        }

        public async Task RegisterAsync(string login, string password,string role)
        {

            var user = new User(login, password,role);
            await _userRepository.AddAsync(user);
        }
    }
}
