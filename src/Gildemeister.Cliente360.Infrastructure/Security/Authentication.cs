using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Gildemeister.Cliente360.Infrastructure.Security
{
    public class Authentication
    {
        public JwtAuthToken RefreshJwtToken(string refreshToken)
        {
            var desencriptarToken = Encoding.ASCII.GetString(Convert.FromBase64String(refreshToken));
            var usuario = desencriptarToken.Split(',').ToList().FirstOrDefault();

            return GenerateJwtToken(usuario);
        }

        public JwtAuthToken GenerateJwtToken(string usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, "1")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(JwtSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                JwtSettings.JwtIssuer,
                JwtSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var output = new JwtAuthToken
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Concat(usuario, ",", Guid.NewGuid().ToString()))),
                ExpiresOn = expires
            };

            return output;
        }

    }
}
