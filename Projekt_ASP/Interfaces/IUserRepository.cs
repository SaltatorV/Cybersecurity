using Projekt_ASP.Controllers;
using Projekt_ASP.Data;
using Projekt_ASP.DTO;

namespace Projekt_ASP.Interfaces
{
    public interface IUserRepository
    {
        
        public Task OpcjeHaselUser(OpcjeHaselDto opcje);
        public Task<bool> CzyWygaslo(string login);
        public Task ChangePassword(ChangePassword user);
        public  Task<User> GetAsync(string email);
        public  Task<List<User>> GetAll();
        public  Task AddAsync(CreateUserDto user);
        public  Task DeleteAsync(string user);
        public  Task UpdateAsync(User user);
        public  Task Zablokuj(string login);
        public  Task Odblokuj(string login);
    
    }
}
