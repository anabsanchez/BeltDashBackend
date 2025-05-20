using BeltDash.Domain.Entities.Common;

namespace BeltDash.Domain.Entities
{
    /// <summary>
    /// Entity that represents a user role in the system.
    /// Implements the role-based access control (RBAC) pattern.
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Name of the role, identifying its function in the system (e.g., "player", "admin").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Collection of users assigned to this role.
        /// Represents a one-to-many relationship between roles and users.
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}

