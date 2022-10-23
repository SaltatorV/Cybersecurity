using Projekt_ASP.Controllers;
using Projekt_ASP.Data;
using Projekt_ASP.DTO;
using Projekt_ASP.Interfaces;

namespace Projekt_ASP.Repository
{
    public class UserRepository : IUserRepository
    {


        private static List<User> _users = new List<User>() { new User("Admin","admin","Admin",true),
        new User("User","user","User",true),
        new User("User2","user2","User",true),
        new User("User3","user3","User",true),
        new User("User4","user4","User",true),
        new User("User5","user5","User",true)
        };

        public async Task<User> GetAsync(string login)
         => await Task.FromResult(_users.SingleOrDefault(x => x.Login.ToLowerInvariant() == login.ToLowerInvariant()));


        public async Task<List<User>> GetAll()
        {
            return await Task.FromResult(_users);
        }
        public async Task AddAsync(CreateUserDto user)
        {
            var userExists = _users.SingleOrDefault(x => x.Login == user.Login);
            if (userExists != null)
            {
                throw new Exception("Login jest zajety");
            }
            var userUser = new User(user.Login, user.NewPassword,user.Role,true);
            _users.Add(userUser);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string login)
        {
            var user = _users.SingleOrDefault(x=>x.Login == login);
            _users.Remove(user);
            await Task.CompletedTask;
        }


        public async Task UpdateAsync(User user)
        {

            await Task.CompletedTask;
        }

        public async Task ChangePassword(ChangePassword change)
        {
            var user = _users.SingleOrDefault(x => x.Login == change.Login);

            if (user.Password == change.OldPassword)
            {
                user.Password = change.NewPassword;

                await Task.CompletedTask;
            }
            else
            {
                throw new Exception("Bad old password!");
                
            }


        }

        public async Task Zablokuj(string login)
        {
            var user = _users.SingleOrDefault(x => x.Login == login);
            user.isActive = false;
            
            await Task.CompletedTask;

        }
    }
}
