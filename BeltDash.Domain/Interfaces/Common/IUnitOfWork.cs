using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeltDash.Domain.Interfaces.Common
{
    internal interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IScoreRepository Scores { get; }
        Task<int> SaveChangesAsync();

    }
}
