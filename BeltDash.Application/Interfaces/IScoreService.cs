using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.Score;

namespace BeltDash.Application.Interfaces
{
    /// <summary>
    /// Interface for the score management service.
    /// Defines CRUD operations for game scores.
    /// </summary>
    public interface IScoreService
    {
        /// <summary>
        /// Creates a new score for a user.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="createScoreDto">Data for the score to create</param>
        /// <returns>Created score</returns>
        Task<ApiResponseDto<ScoreDto>> CreateScoreAsync(int userId, CreateScoreDto createScoreDto);

        /// <summary>
        /// Retrieves scores with pagination and filtering applied.
        /// </summary>
        /// <param name="queryParams">Query parameters for filtering and sorting</param>
        /// <returns>Paginated set of scores</returns>
        Task<ApiResponseDto<PaginatedScoresDto>> GetPaginatedScoresAsync(ScoreQueryParams queryParams);

        /// <summary>
        /// Retrieves all scores of a specific user.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>List of user scores</returns>
        Task<ApiResponseDto<IEnumerable<ScoreDto>>> GetScoresByUserIdAsync(int userId);
    }
}
