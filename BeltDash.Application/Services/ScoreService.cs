using AutoMapper;
using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.Score;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Application.Services
{
    /// <summary>
    /// Service for managing player scores.
    /// Implements operations to create and retrieve scores.
    /// </summary>
    public class ScoreService : IScoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the score service.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for repository access</param>
        /// <param name="mapper">Mapper to convert between entities and DTOs</param>
        public ScoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new score for a specific user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="createScoreDto">Data for the score to create</param>
        /// <returns>Created score</returns>
        public async Task<ApiResponseDto<ScoreDto>> CreateScoreAsync(int userId, CreateScoreDto createScoreDto)
        {
            // Verify that the user exists
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponseDto<ScoreDto>.ErrorResponse("User not found.", 404);
            }

            // Create new score
            var score = new Score
            {
                UserId = userId,
                Points = createScoreDto.Points
            };

            await _unitOfWork.Scores.AddAsync(score);
            await _unitOfWork.SaveChangesAsync();

            // Retrieve the created score with user information
            var createdScore = await _unitOfWork.Scores.GetByIdAsync(score.Id);
            if (createdScore == null)
            {
                return ApiResponseDto<ScoreDto>.ErrorResponse("Error creating score.", 500);
            }

            // Map to DTO
            var scoreDto = _mapper.Map<ScoreDto>(createdScore);
            scoreDto.Username = user.Username;

            return ApiResponseDto<ScoreDto>.SuccessResponse(scoreDto, 201);
        }

        /// <summary>
        /// Retrieves paginated scores according to specified parameters.
        /// </summary>
        /// <param name="queryParams">Query and pagination parameters</param>
        /// <returns>Paginated set of scores</returns>
        public async Task<ApiResponseDto<PaginatedScoresDto>> GetPaginatedScoresAsync(ScoreQueryParams queryParams)
        {
            // Get scores with pagination, sorting, etc.
            var scores = await _unitOfWork.Scores.GetPaginatedScoresAsync(
                queryParams.PageNumber,
                queryParams.PageSize,
                queryParams.SortBy,
                queryParams.Ascending);

            // Get total count of scores for pagination info
            var allScores = await _unitOfWork.Scores.GetAllAsync();
            var totalCount = allScores.Count();

            // Map to DTOs
            var scoreDtos = _mapper.Map<IEnumerable<ScoreDto>>(scores);

            // Create paginated result
            var result = new PaginatedScoresDto
            {
                Scores = scoreDtos,
                TotalCount = totalCount,
                PageSize = queryParams.PageSize,
                CurrentPage = queryParams.PageNumber
            };

            return ApiResponseDto<PaginatedScoresDto>.SuccessResponse(result);
        }

        /// <summary>
        /// Retrieves all scores of a specific user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>List of user's scores</returns>
        public async Task<ApiResponseDto<IEnumerable<ScoreDto>>> GetScoresByUserIdAsync(int userId)
        {
            // Verify that the user exists
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponseDto<IEnumerable<ScoreDto>>.ErrorResponse("User not found.", 404);
            }

            // Get user's scores
            var scores = await _unitOfWork.Scores.GetScoresByUserIdAsync(userId);

            // Map to DTOs
            var scoreDtos = _mapper.Map<IEnumerable<ScoreDto>>(scores);

            // Set username since it is available
            foreach (var scoreDto in scoreDtos)
            {
                scoreDto.Username = user.Username;
            }

            return ApiResponseDto<IEnumerable<ScoreDto>>.SuccessResponse(scoreDtos);
        }
    }
}
