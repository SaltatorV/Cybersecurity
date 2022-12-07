namespace Projekt_ASP.Data
{
    public class OldPassword
    {
        public OldPassword(string login, List<string> passwords)
        {
            Login = login;
            Passwords = passwords;
        }

        public OldPassword(String login)
        {
            Login = login;
            Passwords = new List<string>();
        }

        public string Login { get; set; }
        public List<string> Passwords { get; set; }
    }
}
