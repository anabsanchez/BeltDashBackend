namespace BeltDash.Application.DTOs.Auth
{
    /// <summary>
    /// DTO for the authentication response.
    /// Contains information about the authenticated user and their JWT token for accessing protected resources.
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        /// Unique identifier of the authenticated user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Username to display in the interface.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Email associated with the user's account.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Role assigned to the user that determines their permissions.
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// JWT token for authorization in subsequent requests.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
