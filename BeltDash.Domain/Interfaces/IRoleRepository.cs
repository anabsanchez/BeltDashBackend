using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name);

    }
}
