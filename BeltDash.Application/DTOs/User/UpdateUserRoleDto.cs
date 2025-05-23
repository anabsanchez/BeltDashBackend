namespace BeltDash.Application.DTOs.User
{
    /// <summary>
    /// DTO to update a user's role.
    /// Allows changing the user's permissions in the system.
    /// </summary>
    public class UpdateUserRoleDto
    {
        /// <summary>
        /// Identifier of the newly assigned role.
        /// </summary>
        public int RoleId { get; set; }
    }
}
