using Projekt_ASP.Controllers;
using Projekt_ASP.Data;
using Projekt_ASP.DTO;
using WebApi.Models.DTO;

namespace Projekt_ASP.Interfaces
{
    public interface IUserService
    {
        public Task<TokenObjectDto> Verify(string token);
        public Task<OptionsDto> ZaIleDniWygasnie(string login);
        public Task<bool> CzyWygasloService(string login);
        public Task OpcjeHasel(OpcjeHaselDto opcje);
        public Task ChangePassword(ChangePassword user);
        public Task GetCreateUser(CreateUserDto user);
        public Task DeleteUserLoginAsync(string login);
        public Task ZablokujUserAsync(string login);
        public Task OdblokujUserAsync(string login);
        public Task RegisterAsync(string Login, string Paswword, string role);
        Task<TokenObjectDto> GetAccountByToken(string token);
        public Task<Token> LoginAsync(string Login, string password);
        public Task<List<User>> GetAllUser();
    }
}
