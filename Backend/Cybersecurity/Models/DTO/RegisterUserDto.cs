using System.ComponentModel;

namespace Cybersecurity.Models.DTO
{
    public class RegisterUserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [DefaultValue(2)]
        public int RoleId { get; set; }


    }
}
