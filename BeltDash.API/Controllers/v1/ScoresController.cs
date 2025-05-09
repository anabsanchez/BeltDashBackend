using BeltDash.API.Controllers.Common;
using BeltDash.Application.DTOs.Score;
using BeltDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeltDash.API.Controllers.v1
{
    public class ScoresController : ApiControllerBase
    {
        private readonly IScoreService _scoreService;

        public ScoresController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateScore([FromBody] CreateScoreDto createScoreDto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var response = await _scoreService.CreateScoreAsync(userId, createScoreDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedScores([FromQuery] ScoreQueryParams queryParams)
        {
            var response = await _scoreService.GetPaginatedScoresAsync(queryParams);
            return StatusCode(response.StatusCode, response);
        }
    }
}
