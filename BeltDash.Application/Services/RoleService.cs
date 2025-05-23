using AutoMapper;
using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.Role;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Application.Services
{
    /// <summary>
    /// Service for managing user roles in the system.
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the role service.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for repository access</param>
        /// <param name="mapper">Mapper to convert between entities and DTOs</param>
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all available roles in the system.
        /// </summary>
        /// <returns>List of existing roles</returns>
        public async Task<ApiResponseDto<IEnumerable<RoleDto>>> GetAllRolesAsync()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return ApiResponseDto<IEnumerable<RoleDto>>.SuccessResponse(roleDtos);
        }
    }
}
