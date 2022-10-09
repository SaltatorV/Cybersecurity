namespace Projekt_ASP.Data
{
    public class Login
    {
        public Login(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public string login { get; set; }
        public string password { get; set; }
    }
}
