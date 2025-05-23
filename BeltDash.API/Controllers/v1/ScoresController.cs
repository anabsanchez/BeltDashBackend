using BeltDash.API.Controllers.Common;
using BeltDash.Application.DTOs.Score;
using BeltDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeltDash.API.Controllers.v1
{
    /// <summary>
    /// Controller to manage scores for the Belt Dash game.
    /// Allows creating and querying scores.
    /// </summary>
    public class ScoresController : ApiControllerBase
    {
        private readonly IScoreService _scoreService;

        /// <summary>
        /// Constructor that injects the score service.
        /// </summary>
        /// <param name="scoreService">Score service</param>
        public ScoresController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        /// <summary>
        /// Endpoint to create a new score.
        /// Requires authentication.
        /// POST: /api/v1/scores
        /// </summary>
        /// <param name="createScoreDto">Score creation data</param>
        /// <returns>Response with the created score</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateScore([FromBody] CreateScoreDto createScoreDto)
        {
            // Retrieve the user ID from the JWT token claims
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var response = await _scoreService.CreateScoreAsync(userId, createScoreDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Endpoint to get paginated scores with filters.
        /// GET: /api/v1/scores
        /// </summary>
        /// <param name="queryParams">Query parameters for filtering and pagination</param>
        /// <returns>Paginated list of scores</returns>
        [HttpGet]
        public async Task<IActionResult> GetPaginatedScores([FromQuery] ScoreQueryParams queryParams)
        {
            var response = await _scoreService.GetPaginatedScoresAsync(queryParams);
            return StatusCode(response.StatusCode, response);
        }
    }
}
