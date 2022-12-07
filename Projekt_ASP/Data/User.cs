namespace Projekt_ASP.Data
{
    public class User
    {
        public User(string login, string password, string role, bool isActive, DateTime dataWygasnieciaHasla, bool politykaHasel)
        {
            Login = login;
            Password = password;
            Role = role;
            this.isActive = isActive;
            DataWygasnieciaHasla = dataWygasnieciaHasla;
            PolitykaHasel = politykaHasel;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool isActive { get; set; }
        public DateTime DataWygasnieciaHasla { get; set; }
        public bool PolitykaHasel { get; set; }

    }
}
