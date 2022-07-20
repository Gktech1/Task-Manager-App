using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiTask.Security
{
    public class JWTSecurity
    {
        private readonly IConfiguration _config;

        public JWTSecurity(IConfiguration config)
        {
            _config = config;

        }

        public string JWTGen()
        {
            //Setup Symmetric Security Scheme
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            
            //descriptor
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials =
                    new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            // create token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
