using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.User;

namespace BeltDash.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponseDto<IEnumerable<UserDto>>> GetAllUsersAsync();

        Task<ApiResponseDto<UserDto>> GetUserByIdAsync(int id);

        Task<ApiResponseDto<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto);

        Task<ApiResponseDto<UserDto>> UpdateUserStatusAsync(int id, UpdateUserStatusDto updateStatusDto);

        Task<ApiResponseDto<UserDto>> UpdateUserRoleAsync(int id, UpdateUserRoleDto updateRoleDto);

        Task<ApiResponseDto<bool>> DeleteUserAsync(int id);
    }
}
