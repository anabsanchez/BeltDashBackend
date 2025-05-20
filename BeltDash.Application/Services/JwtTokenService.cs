using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BeltDash.Infrastructure.Services
{
    /// <summary>
    /// Service responsible for generating JWT (JSON Web Tokens).
    /// Implements token-based authentication for the API.
    /// </summary>
    public class JwtTokenService : IJwtTokenService
    {
        /// <summary>
        /// Application configuration to access JWT parameters.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor that initializes the service with application configuration.
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a JWT token for a specific user with their role.
        /// The token contains claims that identify the user and their permissions.
        /// </summary>
        /// <param name="user">User for whom the token is generated</param>
        /// <param name="roleName">Name of the user's role</param>
        /// <returns>JWT token as a string</returns>
        /// <exception cref="ArgumentNullException">Thrown if the JWT key is not configured</exception>
        public string GenerateToken(User user, string roleName)
        {
            // Retrieve the secret key to sign the token from configuration
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key is not configured"));

            // Retrieve issuer and audience from configuration
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            // Set token expiration time (1 hour)
            var expires = DateTime.UtcNow.AddHours(1);

            // Define claims that identify the user
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),         // Unique identifier
                new Claim(JwtRegisteredClaimNames.Email, user.Email),               // Email address
                new Claim(ClaimTypes.Name, user.Username),                          // Username
                new Claim(ClaimTypes.Role, roleName),                               // Role for authorization
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())   // Unique token ID
            };

            // Create the token object with all configured parameters
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256) // HMAC SHA-256 signing algorithm
            );

            // Convert the token to string format for transmission
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
