using BeltDash.Application.DTOs.Auth;
using BeltDash.Application.DTOs.Common;

namespace BeltDash.Application.Interfaces
{
    /// <summary>
    /// Interface for the authentication service.
    /// Defines operations for user registration and login.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="registerDto">User registration data</param>
        /// <returns>Response with authentication token if registration is successful</returns>
        Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto);

        /// <summary>
        /// Authenticates an existing user.
        /// </summary>
        /// <param name="loginDto">Login credentials</param>
        /// <returns>Response with authentication token if credentials are valid</returns>
        Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto);
    }
}
