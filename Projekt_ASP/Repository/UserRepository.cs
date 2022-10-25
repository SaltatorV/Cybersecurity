using Projekt_ASP.Controllers;
using Projekt_ASP.Data;
using Projekt_ASP.DTO;
using Projekt_ASP.Interfaces;

namespace Projekt_ASP.Repository
{
    public class UserRepository : IUserRepository
    {


        private static List<User> _users = new List<User>() {
        new User("Admin","admin","Admin",true,DateTime.Now.AddDays(30),false),
        new User("User","user","User",true,DateTime.Now.AddDays(30),false),
        new User("User2","user2","User",true,DateTime.Now.AddDays(30),false),
        new User("User3","user3","User",true,DateTime.Now.AddDays(30),false),
        new User("User4","user4","User",true,DateTime.Now.AddDays(30),false),
        new User("User5","user5","User",true,DateTime.Now.AddDays(30),false)
        };

        private static List<OldPassword> _oldPasswords = new List<OldPassword>(){
            new OldPassword("Admin",new List<string>(){"admin","admin123","Admin1234@"}),
            new OldPassword("User",new List<string>(){"user","user123","user1234"})
        };

        public async Task<User> GetAsync(string login)
         => await Task.FromResult(_users.SingleOrDefault(x => x.Login.ToLowerInvariant() == login.ToLowerInvariant()));


        public async Task<List<User>> GetAll()
        {
            var listUser = _users.ToList();
            //Zakaz edycji danych Admina
            var admin = _users.SingleOrDefault(admin => admin.Login == "Admin");

            listUser.Remove(admin);
            return await Task.FromResult(listUser);
        }
        public async Task AddAsync(CreateUserDto user)
        {
            var userExists = _users.SingleOrDefault(x => x.Login == user.Login);
            if (userExists != null)
            {
                throw new Exception("Login jest zajety");
            }
            var userUser = new User(user.Login, user.NewPassword, user.Role, true, DateTime.Now.AddDays(30), true);
            _users.Add(userUser);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string login)
        {
            var user = _users.SingleOrDefault(x => x.Login == login);
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
            var oldPasswordUser = _oldPasswords.SingleOrDefault(x => x.Login == change.Login);

            if (change.OldPassword != user.Password)
            {
                throw new Exception("Bad old password");
            }


            if (user.PolitykaHasel == true)
            {
                await PasswordOptions.PasswordChecker.varunkiZmianyHasla(user, change);
            }

            await PasswordOptions.PasswordChecker.poprzednieHasla(change, oldPasswordUser);

            oldPasswordUser.Passwords.Add(change.NewPassword);
            user.DataWygasnieciaHasla = DateTime.Now.AddDays(30);
            user.Password = change.NewPassword;


        }

        public async Task Zablokuj(string login)
        {
            var user = _users.SingleOrDefault(x => x.Login == login);
            user.isActive = false;

            await Task.CompletedTask;

        }

        public async Task Odblokuj(string login)
        {

            var user = _users.SingleOrDefault(x => x.Login == login);
            user.isActive = true;

            await Task.CompletedTask;


        }

        public async Task OpcjeHaselUser(OpcjeHaselDto opcje)
        {
            var user = _users.SingleOrDefault(x => x.Login == opcje.Login);

            user.PolitykaHasel = opcje.PolitykaHasla;
            var ifTrue = int.TryParse(opcje.Wygasniecie, out int resDays);
            if (ifTrue)
            {
                user.DataWygasnieciaHasla = DateTime.Now.AddDays(resDays);
            }
            else if (ifTrue != null)
            {
                throw new Exception("Blad parsowania liczby");
            }



            await Task.CompletedTask;
        }

        public async Task<bool> CzyWygaslo(string login)
        {
            var user = _users.SingleOrDefault(x => x.Login == login);

            if (user.DataWygasnieciaHasla < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
            await Task.CompletedTask;
        }
        public async Task<OptionsDto> ZaIleDniWygasnie(string login)
        {
            var user = _users.SingleOrDefault(x => x.Login == login);

            var opt = new OptionsDto();
              opt.Days = user.DataWygasnieciaHasla.DayOfYear - DateTime.Now.DayOfYear;
            opt.PolitykaHasel = user.PolitykaHasel.ToString();

            return opt;
        }
    }
}
