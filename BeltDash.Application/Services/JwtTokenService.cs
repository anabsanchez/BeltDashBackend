using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BeltDash.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, string roleName)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key is not configured"));

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var expires = DateTime.UtcNow.AddHours(1);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),     
                new Claim(JwtRegisteredClaimNames.Email, user.Email),        
                new Claim(ClaimTypes.Name, user.Username),                     
                new Claim(ClaimTypes.Role, roleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())   
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256) 
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
