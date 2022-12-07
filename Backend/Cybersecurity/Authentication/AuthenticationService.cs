﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Cybersecurity.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        public AuthenticationService(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
        }

        public async Task<string> Generate(int userId, string roleName)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, roleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return await Task.FromResult(tokenHandler.WriteToken(token));
        }

        public async Task<string> GetIdFromClaim(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwt);

            return await Task.FromResult(jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}