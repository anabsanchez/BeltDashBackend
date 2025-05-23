using BeltDash.Domain.Enums;

namespace BeltDash.Application.DTOs.User
{
    /// <summary>
    /// DTO to represent complete information of a user.
    /// Contains all relevant data of a system user.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Username for display and access.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// User's email for contact and notifications.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Current status of the user in the system.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Name of the role assigned to the user.
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Date and time when the account was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time of the last data update.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
