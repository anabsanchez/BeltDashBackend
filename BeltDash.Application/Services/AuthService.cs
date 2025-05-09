using AutoMapper;
using BeltDash.Application.DTOs.Auth;
using BeltDash.Application.DTOs.Common;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
        {
            var existingUserByEmail = await _unitOfWork.Users.GetByEmailAsync(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Email is already registered.", 400);
            }

            var existingUserByUsername = await _unitOfWork.Users.GetByUsernameAsync(registerDto.Username);
            if (existingUserByUsername != null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Username is already taken.", 400);
            }

            var playerRole = await _unitOfWork.Roles.GetByNameAsync("player");
            if (playerRole == null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Default role not found.", 500);
            }

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password), 
                RoleId = playerRole.Id
            };

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

        public async Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Invalid email or password.", 401);
            }

            if (!VerifyPassword(loginDto.Password, user.PasswordHash)) 
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Invalid email or password.", 401);
            }

            if (user.Status == Domain.Enums.UserStatus.Banned)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("Your account has been banned.", 403);
            }

            var userWithRole = await _unitOfWork.Users.GetUserWithRoleAsync(user.Id);
            if (userWithRole?.Role == null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResponse("User role not found.", 500);
            }

            var token = _jwtTokenService.GenerateToken(user, userWithRole.Role.Name);

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
        
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
