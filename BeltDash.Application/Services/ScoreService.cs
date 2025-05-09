using AutoMapper;
using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.Score;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Application.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDto<ScoreDto>> CreateScoreAsync(int userId, CreateScoreDto createScoreDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponseDto<ScoreDto>.ErrorResponse("User not found.", 404);
            }

            var score = new Score
            {
                UserId = userId,
                Points = createScoreDto.Points
            };

            await _unitOfWork.Scores.AddAsync(score);
            await _unitOfWork.SaveChangesAsync();

            var createdScore = await _unitOfWork.Scores.GetByIdAsync(score.Id);
            if (createdScore == null)
            {
                return ApiResponseDto<ScoreDto>.ErrorResponse("Error creating score.", 500);
            }

            var scoreDto = _mapper.Map<ScoreDto>(createdScore);
            scoreDto.Username = user.Username;

            return ApiResponseDto<ScoreDto>.SuccessResponse(scoreDto, 201);
        }

        public async Task<ApiResponseDto<PaginatedScoresDto>> GetPaginatedScoresAsync(ScoreQueryParams queryParams)
        {
            var scores = await _unitOfWork.Scores.GetPaginatedScoresAsync(
                queryParams.PageNumber,
                queryParams.PageSize,
                queryParams.SortBy,
                queryParams.Ascending);

            var allScores = await _unitOfWork.Scores.GetAllAsync();
            var totalCount = allScores.Count();

            var scoreDtos = _mapper.Map<IEnumerable<ScoreDto>>(scores);

            var result = new PaginatedScoresDto
            {
                Scores = scoreDtos,
                TotalCount = totalCount,
                PageSize = queryParams.PageSize,
                CurrentPage = queryParams.PageNumber
            };

            return ApiResponseDto<PaginatedScoresDto>.SuccessResponse(result);
        }

        public async Task<ApiResponseDto<IEnumerable<ScoreDto>>> GetScoresByUserIdAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponseDto<IEnumerable<ScoreDto>>.ErrorResponse("User not found.", 404);
            }

            var scores = await _unitOfWork.Scores.GetScoresByUserIdAsync(userId);

            var scoreDtos = _mapper.Map<IEnumerable<ScoreDto>>(scores);

            foreach (var scoreDto in scoreDtos)
            {
                scoreDto.Username = user.Username;
            }

            return ApiResponseDto<IEnumerable<ScoreDto>>.SuccessResponse(scoreDtos);
        }
    }
}
