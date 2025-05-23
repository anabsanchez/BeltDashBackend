using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Domain.Interfaces
{
    /// <summary>
    /// Specific interface for the user repository.
    /// Extends the generic repository with user-specific operations.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Searches for a user by their username.
        /// </summary>
        /// <param name="username">Username to search for</param>
        /// <returns>The found user or null if none exists</returns>
        Task<User?> GetByUsernameAsync(string username);

        /// <summary>
        /// Searches for a user by their email address.
        /// </summary>
        /// <param name="email">Email address to search for</param>
        /// <returns>The found user or null if none exists</returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Retrieves a user along with their role information (eager loading).
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>User with their role loaded or null if none exists</returns>
        Task<User?> GetUserWithRoleAsync(int userId);

        /// <summary>
        /// Retrieves all users in the system along with their role information (eager loading).
        /// </summary>
        /// <returns>A collection of users with their associated roles</returns>
        Task<IEnumerable<User>> GetAllUsersWithRolesAsync();
    }
}
