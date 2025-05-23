namespace BeltDash.Application.DTOs.Auth
{
    /// <summary>
    /// DTO for the new user registration request.
    /// Contains the information required to create a new account in the system.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Username chosen for the new account.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Email for contact and authentication.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password chosen for the account (will be hashed before storing).
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Password confirmation to verify correct input.
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
