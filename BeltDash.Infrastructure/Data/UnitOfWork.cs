using BeltDash.Domain.Interfaces;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Infrastructure.Data
{
    /// <summary>
    /// Implementation of the Unit of Work pattern to manage transactions and repositories.
    /// Coordinates the work of multiple repositories ensuring data integrity.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        // Injected database context.
        private readonly BeltDashDbContext _context;

        // Injected repositories.
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IScoreRepository _scoreRepository;

        /// <summary>
        /// Constructor that receives the necessary dependencies for the unit of work.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="userRepository">User repository</param>
        /// <param name="roleRepository">Role repository</param>
        /// <param name="scoreRepository">Score repository</param>
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

        /// <summary>
        /// Property exposing the user repository.
        /// </summary>
        public IUserRepository Users => _userRepository;

        /// <summary>
        /// Property exposing the role repository.
        /// </summary>
        public IRoleRepository Roles => _roleRepository;

        /// <summary>
        /// Property exposing the score repository.
        /// </summary>
        public IScoreRepository Scores => _scoreRepository;

        /// <summary>
        /// Saves all changes made in the context to the database.
        /// Ensures transaction consistency.
        /// </summary>
        /// <returns>Number of entities affected in the database</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Releases the resources used by the context.
        /// Implements the Disposable pattern.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
