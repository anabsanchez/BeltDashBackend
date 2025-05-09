using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.Role;

namespace BeltDash.Application.Interfaces
{
    public interface IRoleService
    {
        Task<ApiResponseDto<IEnumerable<RoleDto>>> GetAllRolesAsync();
    }
}
