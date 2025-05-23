using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces;
using BeltDash.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace BeltDash.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository specific to the Role entity.
    /// Extends the generic repository and adds role-specific operations.
    /// </summary>
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        /// <summary>
        /// Constructor that initializes the repository with a database context.
        /// </summary>
        /// <param name="context">Application database context</param>
        public RoleRepository(BeltDashDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Searches for a role by its name, ignoring case sensitivity.
        /// </summary>
        /// <param name="name">Name of the role to search for</param>
        /// <returns>The found role or null if it does not exist</returns>
        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
        }
    }
}
