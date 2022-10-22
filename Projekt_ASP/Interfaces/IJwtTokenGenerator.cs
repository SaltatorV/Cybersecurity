using Projekt_ASP.Data;
using WebApi.Models.DTO;

namespace Projekt_ASP.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Token Generate(string user, string role);
        Task<TokenObjectDto> Decode(string token);
    }
}
