using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Domain.Interfaces
{
    /// <summary>
    /// Specific interface for the role repository.
    /// Extends the generic repository with role-specific operations.
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// Searches for a role by its name.
        /// </summary>
        /// <param name="name">Name of the role to search for</param>
        /// <returns>The found role or null if it does not exist</returns>
        Task<Role?> GetByNameAsync(string name);
    }
}
