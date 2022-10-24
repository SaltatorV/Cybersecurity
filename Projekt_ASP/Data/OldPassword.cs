namespace Projekt_ASP.Data
{
    public class OldPassword
    {
        public OldPassword(string login, List<string> passwords)
        {
            Login = login;
            Passwords = passwords;
        }

        public string Login { get; set; }
        public List<string> Passwords { get; set; }
    }
}
