using BeltDash.API.Controllers.Common;
using BeltDash.Application.DTOs.User;
using BeltDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeltDash.API.Controllers.v1
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IScoreService _scoreService;

        public UsersController(IUserService userService, IScoreService scoreService)
        {
            _userService = userService;
            _scoreService = scoreService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!IsAdmin() && !IsResourceOwner(id))
            {
                return Forbid();
            }

            var response = await _userService.UpdateUserAsync(id, updateUserDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] UpdateUserStatusDto updateStatusDto)
        {
            var response = await _userService.UpdateUserStatusAsync(id, updateStatusDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}/role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleDto updateRoleDto)
        {
            var response = await _userService.UpdateUserRoleAsync(id, updateRoleDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}/scores")]
        [Authorize]
        public async Task<IActionResult> GetUserScores(int id)
        {
            if (!IsAdmin() && !IsResourceOwner(id))
            {
                return Forbid();
            }

            var response = await _scoreService.GetScoresByUserIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        private bool IsAdmin()
        {
            return User.IsInRole("admin");
        }

        private bool IsResourceOwner(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null && int.Parse(userIdClaim) == id;
        }
    }
}
