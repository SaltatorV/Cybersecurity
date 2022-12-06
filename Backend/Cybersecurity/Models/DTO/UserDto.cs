namespace Cybersecurity.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsPasswordExpire { get; set; }
        public DateTime PasswordExpire { get; set; }

    }
}
