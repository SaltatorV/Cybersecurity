using Projekt_ASP.Controllers;
using Projekt_ASP.Data;
using Projekt_ASP.DTO;
using WebApi.Models.DTO;

namespace Projekt_ASP.Interfaces
{
    public interface IUserService
    {

        public Task ChangePassword(ChangePassword user);
        public Task GetCreateUser(CreateUserDto user);
        public Task RegisterAsync(string Login, string Paswword, string role);
        Task<TokenObjectDto> GetAccountByToken(string token);
        public Task<Token> LoginAsync(string Login, string password);
        public Task<List<User>> GetAllUser();
    }
}
