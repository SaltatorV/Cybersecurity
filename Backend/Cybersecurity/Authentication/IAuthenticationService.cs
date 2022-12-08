namespace Cybersecurity.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> Generate(int userId, string roleName, int sessionTime);
        Task<int> GetIdFromClaim(string jwt);
    }
}
