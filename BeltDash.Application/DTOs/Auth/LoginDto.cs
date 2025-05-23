namespace BeltDash.Application.DTOs.Auth
{
    /// <summary>
    /// DTO for the login request.
    /// Contains the credentials required to authenticate a user.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// User's email for identification.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's password for verification.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
