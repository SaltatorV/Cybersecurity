using System.ComponentModel;

namespace Cybersecurity.Models.DTO
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string? Login { get; set; }

        [DefaultValue(false)]
        public bool IsPasswordExpire { get; set; }

        [DefaultValue(30)]
        public int DayExpire { get; set; }

        [DefaultValue(2)]
        public int RoleId { get; set; }

        [DefaultValue(15)]
        public int SessionTime { get; set; }

        [DefaultValue(5)]
        public int MaxFailLogin { get; set; }

    }
}
