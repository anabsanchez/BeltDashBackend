using BeltDash.Domain.Entities;

namespace BeltDash.Application.Interfaces
{
    /// <summary>
    /// Interface for the JWT token generation service.
    /// Encapsulates the logic for creating authentication tokens.
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token for an authenticated user.
        /// </summary>
        /// <param name="user">User for whom the token is generated</param>
        /// <param name="roleName">Name of the user's role</param>
        /// <returns>Generated JWT token</returns>
        string GenerateToken(User user, string roleName);
    }
}
