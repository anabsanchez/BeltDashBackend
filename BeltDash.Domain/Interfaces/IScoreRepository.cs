using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Domain.Interfaces
{
    /// <summary>
    /// Specific interface for the score repository.
    /// Extends the generic repository with score-specific operations.
    /// </summary>
    public interface IScoreRepository : IRepository<Score>
    {
        /// <summary>
        /// Retrieves all scores of a specific user.
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>Collection of the user's scores</returns>
        Task<IEnumerable<Score>> GetScoresByUserIdAsync(int userId);

        /// <summary>
        /// Retrieves the highest scores in the system.
        /// Useful for implementing leaderboards.
        /// </summary>
        /// <param name="count">Maximum number of scores to retrieve</param>
        /// <returns>Collection of the top scores</returns>
        Task<IEnumerable<Score>> GetTopScoresAsync(int count);

        /// <summary>
        /// Retrieves scores with pagination and sorting.
        /// Enables efficient listings with large volumes of data.
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Page size (items per page)</param>
        /// <param name="sortBy">Field by which to sort the results</param>
        /// <param name="ascending">Indicates if sorting is ascending (true) or descending (false)</param>
        /// <returns>Paginated collection of scores</returns>
        Task<IEnumerable<Score>> GetPaginatedScoresAsync(int pageNumber, int pageSize, string? sortBy = null, bool ascending = false);
    }
}
