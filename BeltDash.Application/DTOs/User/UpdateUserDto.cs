namespace BeltDash.Application.DTOs.User
{
    /// <summary>
    /// DTO to update basic user information.
    /// Used when modifying the username or email.
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// New username.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// New email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
