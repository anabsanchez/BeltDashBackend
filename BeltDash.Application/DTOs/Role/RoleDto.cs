namespace BeltDash.Application.DTOs.Role
{
    /// <summary>
    /// DTO to represent a user role in the system.
    /// Roles define the permissions and capabilities of each user.
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Unique identifier of the role.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descriptive name of the role (for example: Administrator, Player, Moderator).
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
