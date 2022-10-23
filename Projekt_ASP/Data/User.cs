namespace Projekt_ASP.Data
{
    public class User
    {
        public User(string login, string password, string role, bool isActive)
        {
            Login = login;
            Password = password;
            Role = role;
            this.isActive = isActive;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool isActive { get; set; }
    }
}
