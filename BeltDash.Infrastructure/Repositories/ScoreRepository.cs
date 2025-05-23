using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces;
using BeltDash.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BeltDash.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository specific to the Score entity.
    /// Extends the generic repository and adds score-specific operations.
    /// </summary>
    public class ScoreRepository : Repository<Score>, IScoreRepository
    {
        /// <summary>
        /// Constructor that initializes the repository with a database context.
        /// </summary>
        /// <param name="context">Application database context</param>
        public ScoreRepository(BeltDashDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves all scores for a specific user, ordered from highest to lowest.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Collection of the user's scores</returns>
        public async Task<IEnumerable<Score>> GetScoresByUserIdAsync(int userId)
        {
            return await _context.Scores
                .AsNoTracking()
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Points)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves the highest scores in the system, including user information.
        /// Implements leaderboard functionality.
        /// </summary>
        /// <param name="count">Maximum number of scores to retrieve</param>
        /// <returns>Collection of top scores with user data</returns>
        public async Task<IEnumerable<Score>> GetTopScoresAsync(int count)
        {
            return await _context.Scores
                .AsNoTracking()
                .Include(s => s.User)
                .OrderByDescending(s => s.Points)
                .Take(count)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves scores with pagination and dynamic sorting.
        /// Enables efficient listings with large data volumes.
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Page size (items per page)</param>
        /// <param name="sortBy">Field name to sort by</param>
        /// <param name="ascending">Indicates if sorting is ascending (true) or descending (false)</param>
        /// <returns>Paginated collection of scores</returns>
        public async Task<IEnumerable<Score>> GetPaginatedScoresAsync(int pageNumber, int pageSize, string? sortBy = null, bool ascending = false)
        {
            // Inicia con todas las puntuaciones, incluyendo información del usuario
            IQueryable<Score> query = _context.Scores
                .AsNoTracking()
                .Include(s => s.User);

            // Apply dynamic sorting based on the property name
            if (!string.IsNullOrEmpty(sortBy))
            {
                // Use reflection to get the property by name
                PropertyInfo? property = typeof(Score).GetProperty(
                    sortBy.First().ToString().ToUpper() + sortBy.Substring(1),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    // Apply ascending or descending order as requested
                    if (ascending)
                    {
                        query = query.OrderBy(s => EF.Property<object>(s, property.Name));
                    }
                    else
                    {
                        query = query.OrderByDescending(s => EF.Property<object>(s, property.Name));
                    }
                }
                else
                {
                    // Default sorting if the property is not found
                    query = ascending
                        ? query.OrderBy(s => s.Points)
                        : query.OrderByDescending(s => s.Points);
                }
            }
            else
            {
                // Default sorting if no sort field is specified
                query = query.OrderByDescending(s => s.Points);
            }

            // Apply pagination to the result
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
