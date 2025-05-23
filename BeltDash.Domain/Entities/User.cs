using BeltDash.Domain.Entities.Common;
using BeltDash.Domain.Enums;

namespace BeltDash.Domain.Entities
{
    /// <summary>
    /// Entity that represents a registered user in the system.
    /// Stores authentication information and relationships with other entities.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Unique username in the system.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// User's unique email, used for communications and account recovery.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's password hash, securely stored without plain text password.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Current status of the user in the system (Active or Suspended).
        /// </summary>
        public UserStatus Status { get; set; } = UserStatus.Active;

        /// <summary>
        /// Identifier of the role assigned to the user.
        /// Foreign key for the relationship with the Role entity.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Reference to the role assigned to the user.
        /// </summary>
        public Role? Role { get; set; }

        /// <summary>
        /// Collection of scores achieved by the user.
        /// Represents a one-to-many relationship between users and scores.
        /// </summary>
        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
