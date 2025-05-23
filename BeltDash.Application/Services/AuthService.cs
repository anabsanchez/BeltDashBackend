using AutoMapper;
using BeltDash.Application.DTOs.Auth;
using BeltDash.Application.DTOs.Common;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Application.Services
{
    /// <summary>
    /// Service responsible for user authentication in the system.
    /// Implements operations for user registration and login.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the authentication service.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for repository access</param>
        /// <param name="jwtTokenService">Service for JWT token generation</param>
        /// <param name="mapper">Mapper to convert between entities and DTOs</param>
        public AuthService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="registerDto">User registration data</param>
        /// <returns>Response with authentication token if registration is successful</returns>
        public async Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
        {
            // Check if the email already exists
            var existingUserByEmail = await _unitOfWork.Users.GetByEmailAsync(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Email is already registered.", 400);
            }

            // Check if the username already exists
            var existingUserByUsername = await _unitOfWork.Users.GetByUsernameAsync(registerDto.Username);
            if (existingUserByUsername != null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Username is already taken.", 400);
            }

            // Get the player role (default role)
            var playerRole = await _unitOfWork.Roles.GetByNameAsync("player");
            if (playerRole == null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Default role not found.", 500);
            }

            // Create new user
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password), // Use BCrypt to hash the password
                RoleId = playerRole.Id
            };

            // Save the user to the database
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Generate authentication token
            var token = _jwtTokenService.GenerateToken(user, playerRole.Name);

            // Create response
            var response = new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = playerRole.Name,
                Token = token
            };

            return ApiResponseDto<AuthResponseDto>.SuccessResponse(response, 201);
        }

        /// <summary>
        /// Authenticates an existing user.
        /// </summary>
        /// <param name="loginDto">Login credentials</param>
        /// <returns>Response with authentication token if credentials are valid</returns>
        public async Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            // Find user by email
            var user = await _unitOfWork.Users.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Invalid email or password.", 401);
            }

            // Verify password using BCrypt
            if (!VerifyPassword(loginDto.Password, user.PasswordHash)) // Compare password with stored hash
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Invalid email or password.", 401);
            }

            // Check if user is banned
            if (user.Status == Domain.Enums.UserStatus.Banned)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Your account has been banned.", 403);
            }

            // Get user's role
            var userWithRole = await _unitOfWork.Users.GetUserWithRoleAsync(user.Id);
            if (userWithRole?.Role == null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("User role not found.", 500);
            }

            // Generate authentication token
            var token = _jwtTokenService.GenerateToken(user, userWithRole.Role.Name);

            // Create response
            var response = new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = userWithRole.Role.Name,
                Token = token
            };

            return ApiResponseDto<AuthResponseDto>.SuccessResponse(response, 200);
        }

        /// <summary>
        /// Generates a password hash using BCrypt.
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <returns>Password hash generated by BCrypt</returns>
        private string HashPassword(string password)
        {
            // Use BCrypt to generate a secure hash of the password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies if a password matches a stored hash.
        /// </summary>
        /// <param name="password">Password to verify</param>
        /// <param name="hash">Stored hash</param>
        /// <returns>True if the password is correct</returns>
        private bool VerifyPassword(string password, string hash)
        {
            // Use BCrypt to verify if the password matches the stored hash
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
