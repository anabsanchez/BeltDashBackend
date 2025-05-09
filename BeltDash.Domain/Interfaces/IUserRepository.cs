using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetUserWithRoleAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersWithRolesAsync();

    }
}
