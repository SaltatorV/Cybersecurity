namespace Cybersecurity.Entities
{
    public class OldPassword
    {
        public int Id { get; set; }
        public string Password { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }

    }
}
