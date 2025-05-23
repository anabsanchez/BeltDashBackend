using BeltDash.Domain.Enums;

namespace BeltDash.Application.DTOs.User
{
    /// <summary>
    /// DTO to update a user's status.
    /// Allows activating, deactivating, or suspending user accounts.
    /// </summary>
    public class UpdateUserStatusDto
    {
        /// <summary>
        /// New status of the user (Active, Inactive, Suspended, etc.)
        /// </summary>
        public UserStatus Status { get; set; }
    }
}
