using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces;
using BeltDash.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace BeltDash.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository specific to the User entity.
    /// Extends the generic repository and adds user-specific operations.
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        /// <summary>
        /// Constructor that initializes the repository with a database context.
        /// </summary>
        /// <param name="context">Application database context</param>
        public UserRepository(BeltDashDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Searches for a user by username, ignoring case sensitivity.
        /// </summary>
        /// <param name="username">Username to search for</param>
        /// <returns>The found user or null if not found</returns>
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        /// <summary>
        /// Searches for a user by email address, ignoring case sensitivity.
        /// </summary>
        /// <param name="email">Email address to search for</param>
        /// <returns>The found user or null if not found</returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        /// <summary>
        /// Retrieves a user along with their role information (eager loading).
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>User with their role loaded or null if not found</returns>
        public async Task<User?> GetUserWithRoleAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema junto con su información de rol (carga eager).
        /// </summary>
        /// <returns>Una colección de usuarios con sus roles asociados</returns>
        public async Task<IEnumerable<User>> GetAllUsersWithRolesAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
