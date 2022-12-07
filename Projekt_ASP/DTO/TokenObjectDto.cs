namespace WebApi.Models.DTO
{
    public class TokenObjectDto
    {
        public TokenObjectDto(string login, string role)
        {
            Login = login;
            Role = role;
        }

        public string Login { get; set; }
        public string Role { get; set; }
    }
}
