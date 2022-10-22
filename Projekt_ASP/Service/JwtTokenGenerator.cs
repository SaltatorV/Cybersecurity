using Microsoft.IdentityModel.Tokens;
using Projekt_ASP.Data;
using Projekt_ASP.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models.DTO;

namespace Projekt_ASP.Service
{

 

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public Task<TokenObjectDto> Decode(string token)
        {


            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);


            var login = jwtSecurityToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            var role = jwtSecurityToken.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

            var tokenObject = new TokenObjectDto(login, role);

            return Task.FromResult(tokenObject);

        }
        public Token Generate(string user, string role)
        {
            var keyBytes = Encoding.UTF8.GetBytes("w+1alOGke7bSPTgeMVlDXS5FRg3jcjRxkBtG0u3NrOo=");
            var secret = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                "test",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials
            );

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var Token= jwtTokenHandler.WriteToken(token);

            return new Token(Token);
        }
    }
}

