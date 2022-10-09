using Microsoft.IdentityModel.Tokens;
using Projekt_ASP.Data;
using Projekt_ASP.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Projekt_ASP.Service
{

 

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
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

