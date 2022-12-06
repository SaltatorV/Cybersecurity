namespace Cybersecurity.Models.DTO
{
    public class LogDto
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public string Action { get; set; }
        public string? Message { get; set; }
        public string UserName { get; set; }
    }
}
