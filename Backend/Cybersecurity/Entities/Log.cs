namespace Cybersecurity.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public string Action { get; set; }
        public string? Message { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
