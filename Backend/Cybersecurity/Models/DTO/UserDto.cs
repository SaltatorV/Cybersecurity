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
        public int MaxFailLogin { get; set; }
        public bool IsLoginLockOn { get; set; }
        public DateTime? LoginLockOnTime { get; set; }
        public int SessionTime { get; set; }
        public bool IsOneTimePasswordSet { get; set; }
    }
}
