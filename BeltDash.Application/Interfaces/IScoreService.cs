using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.Score;

namespace BeltDash.Application.Interfaces
{
    public interface IScoreService
    {
        Task<ApiResponseDto<ScoreDto>> CreateScoreAsync(int userId, CreateScoreDto createScoreDto);

        Task<ApiResponseDto<PaginatedScoresDto>> GetPaginatedScoresAsync(ScoreQueryParams queryParams);

        Task<ApiResponseDto<IEnumerable<ScoreDto>>> GetScoresByUserIdAsync(int userId);
    }
}
