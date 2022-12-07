namespace Cybersecurity.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> Generate(int userId, string roleName);
        Task<int> GetIdFromClaim(string jwt);
    }
}
