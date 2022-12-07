namespace Projekt_ASP.Controllers
{
    public class CreateUserDto
    {
        public string Login { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}