using Projekt_ASP.Data;

namespace Projekt_ASP.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Token Generate(string user, string role);
    }
}
