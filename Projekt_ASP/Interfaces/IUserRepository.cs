using Projekt_ASP.Data;
using Projekt_ASP.DTO;

namespace Projekt_ASP.Interfaces
{
    public interface IUserRepository
    {

        public Task ChangePassword(ChangePassword user);
        public  Task<User> GetAsync(string email);
        public  Task<List<User>> GetAll();
        public  Task AddAsync(User user);
        public  Task DeleteAsync(User user);
        public  Task UpdateAsync(User user);
    
    }
}
