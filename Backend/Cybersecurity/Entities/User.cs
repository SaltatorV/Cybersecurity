namespace Cybersecurity.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }

        public bool IsPasswordExpire { get; set; }
        public DateTime PasswordExpire { get; set; }

        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Log>? Logs { get; set; }
        public virtual ICollection<OldPassword>? OldPasswords { get; set; }

    }
}
