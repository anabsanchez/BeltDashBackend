using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.Role;

namespace BeltDash.Application.Interfaces
{
    /// <summary>
    /// Interface for the role management service.
    /// Defines operations related to system roles.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Retrieves all available roles in the system.
        /// </summary>
        /// <returns>List of existing roles</returns>
        Task<ApiResponseDto<IEnumerable<RoleDto>>> GetAllRolesAsync();
    }
}
