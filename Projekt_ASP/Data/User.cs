namespace Projekt_ASP.Data
{
    public class User
    {
        public User(string login, string password, string role)
        {
            Login = login;
            Password = password;
            Role = role;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
