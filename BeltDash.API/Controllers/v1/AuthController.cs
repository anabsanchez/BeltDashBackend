using BeltDash.API.Controllers.Common;
using BeltDash.Application.DTOs.Auth;
using BeltDash.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeltDash.API.Controllers.v1
{
    /// <summary>
    /// Controller to manage user authentication.
    /// Supports user registration and login.
    /// </summary>
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor that injects the authentication service.
        /// </summary>
        /// <param name="authService">Authentication service</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Endpoint to register a new user.
        /// POST: /api/v1/auth/register
        /// </summary>
        /// <param name="registerDto">User registration data</param>
        /// <returns>Response with the registration result</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var response = await _authService.RegisterAsync(registerDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Endpoint to log in.
        /// POST: /api/v1/auth/login
        /// </summary>
        /// <param name="loginDto">Login credentials</param>
        /// <returns>Response with JWT token if authentication is successful</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
