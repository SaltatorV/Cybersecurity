namespace Cybersecurity.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> Generate(int userId, string roleName);
        Task<string> GetIdFromClaim(string jwt);
    }
}
