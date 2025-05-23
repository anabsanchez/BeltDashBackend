using BeltDash.API.Controllers.Common;
using BeltDash.Application.DTOs.User;
using BeltDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeltDash.API.Controllers.v1
{
    /// <summary>
    /// Controller to manage system users.
    /// Implements CRUD operations and additional functionalities.
    /// </summary>
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IScoreService _scoreService;

        /// <summary>
        /// Constructor that injects the required services.
        /// </summary>
        /// <param name="userService">User service</param>
        /// <param name="scoreService">Score service</param>
        public UsersController(IUserService userService, IScoreService scoreService)
        {
            _userService = userService;
            _scoreService = scoreService;
        }

        /// <summary>
        /// Endpoint to get all users.
        /// Restricted to administrators.
        /// GET: /api/v1/users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Endpoint to get a user by ID.
        /// Requires authentication.
        /// GET: /api/v1/users/{id}
        /// </summary>
        /// <param name="id">ID of the user to retrieve</param>
        /// <returns>User data</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Endpoint to update user data.
        /// The user can only update their own profile unless they are an administrator.
        /// PUT: /api/v1/users/{id}
        /// </summary>
        /// <param name="id">ID of the user to update</param>
        /// <param name="updateUserDto">Updated user data</param>
        /// <returns>Response with the update result</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            // Verify if the user is an admin or the resource owner
            if (!IsAdmin() && !IsResourceOwner(id))
            {
                return Forbid();
            }

            var response = await _userService.UpdateUserAsync(id, updateUserDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Endpoint to update a user's status (active/inactive).
        /// Restricted to administrators.
        /// PATCH: /api/v1/users/{id}/status
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="updateStatusDto">New user status</param>
        /// <returns>Response with the update result</returns>
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] UpdateUserStatusDto updateStatusDto)
        {
            var response = await _userService.UpdateUserStatusAsync(id, updateStatusDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Endpoint to update a user's role.
        /// Restricted to administrators.
        /// PATCH: /api/v1/users/{id}/role
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="updateRoleDto">New user role</param>
        /// <returns>Response with the update result</returns>
        [HttpPatch("{id}/role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleDto updateRoleDto)
        {
            var response = await _userService.UpdateUserRoleAsync(id, updateRoleDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Endpoint to delete a user.
        /// Restricted to administrators.
        /// DELETE: /api/v1/users/{id}
        /// </summary>
        /// <param name="id">ID of the user to delete</param>
        /// <returns>Response with the deletion result</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Endpoint to get scores of a specific user.
        /// The user can only view their own scores unless they are an administrator.
        /// GET: /api/v1/users/{id}/scores
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>List of user scores</returns>
        [HttpGet("{id}/scores")]
        [Authorize]
        public async Task<IActionResult> GetUserScores(int id)
        {
            // Verify if the user is an admin or the resource owner
            if (!IsAdmin() && !IsResourceOwner(id))
            {
                return Forbid();
            }

            var response = await _scoreService.GetScoresByUserIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Helper method to check if the current user has the administrator role.
        /// </summary>
        /// <returns>True if the user is an admin, false otherwise</returns>
        private bool IsAdmin()
        {
            return User.IsInRole("admin");
        }

        /// <summary>
        /// Helper method to check if the current user is the owner of the resource.
        /// </summary>
        /// <param name="id">Resource/user ID</param>
        /// <returns>True if the user is the owner, false otherwise</returns>
        private bool IsResourceOwner(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null && int.Parse(userIdClaim) == id;
        }
    }
}
