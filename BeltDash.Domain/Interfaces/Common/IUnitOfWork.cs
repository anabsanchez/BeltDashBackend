namespace BeltDash.Domain.Interfaces.Common
{
    /// <summary>
    /// Interface for the Unit of Work pattern.
    /// Coordinates the work of multiple repositories and maintains a single transaction.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// User repository.
        /// </summary>
        IUserRepository Users { get; }

        /// <summary>
        /// Role repository.
        /// </summary>
        IRoleRepository Roles { get; }

        /// <summary>
        /// Score repository.
        /// </summary>
        IScoreRepository Scores { get; }

        /// <summary>
        /// Saves all changes made in the repositories as a single transaction.
        /// </summary>
        /// <returns>Number of records affected in the database</returns>
        Task<int> SaveChangesAsync();
    }
}
