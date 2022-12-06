namespace Cybersecurity.Authentication
{
    public interface IAuthenticationService
    {
        string Generate(int userId, string roleName);
    }
}
