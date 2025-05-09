using BeltDash.Application.DTOs.Auth;
using BeltDash.Application.DTOs.Common;

namespace BeltDash.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto);

        Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto);
    }
}
