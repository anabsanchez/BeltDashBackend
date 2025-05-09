using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.User;

namespace BeltDash.Application.Interfaces
{
    /// <summary>
    /// Interface for the user management service.
    /// Defines CRUD operations for user administration.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all registered users in the system.
        /// </summary>
        /// <returns>List of users</returns>
        Task<ApiResponseDto<IEnumerable<UserDto>>> GetAllUsersAsync();

        /// <summary>
        /// Retrieves a user by their identifier.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Requested user data</returns>
        Task<ApiResponseDto<UserDto>> GetUserByIdAsync(int id);

        /// <summary>
        /// Updates basic information of a user.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="updateUserDto">Updated user data</param>
        /// <returns>User with updated information</returns>
        Task<ApiResponseDto<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto);

        /// <summary>
        /// Updates the status of a user (active, inactive, suspended).
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="updateStatusDto">New user status</param>
        /// <returns>User with updated status</returns>
        Task<ApiResponseDto<UserDto>> UpdateUserStatusAsync(int id, UpdateUserStatusDto updateStatusDto);

        /// <summary>
        /// Updates the role of a user.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="updateRoleDto">New user role</param>
        /// <returns>User with updated role</returns>
        Task<ApiResponseDto<UserDto>> UpdateUserRoleAsync(int id, UpdateUserRoleDto updateRoleDto);

        /// <summary>
        /// Deletes a user from the system.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Indicator of the operation's success</returns>
        Task<ApiResponseDto<bool>> DeleteUserAsync(int id);
    }
}
