namespace Cybersecurity.Models.DTO
{
    public class ChangePasswordDto
    {
        public string? Login { get; set; }
        public string? OldPassword { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
