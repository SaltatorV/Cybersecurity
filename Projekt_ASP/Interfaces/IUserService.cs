using Projekt_ASP.Data;
using Projekt_ASP.DTO;

namespace Projekt_ASP.Interfaces
{
    public interface IUserService
    {

        public Task ChangePassword(ChangePassword user);
        public Task RegisterAsync(string Login, string Paswword, string role);
        public Task<Token> LoginAsync(string Login, string password);
        public Task<List<User>> GetAllUser();
    }
}
