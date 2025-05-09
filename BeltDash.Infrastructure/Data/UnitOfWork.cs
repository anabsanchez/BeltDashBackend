using BeltDash.Domain.Interfaces;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BeltDashDbContext _context;

        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IScoreRepository _scoreRepository;

        public UnitOfWork(
            BeltDashDbContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IScoreRepository scoreRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _scoreRepository = scoreRepository;
        }

        public IUserRepository Users => _userRepository;

        public IRoleRepository Roles => _roleRepository;

        public IScoreRepository Scores => _scoreRepository;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
