using AutoMapper;
using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.Role;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDto<IEnumerable<RoleDto>>> GetAllRolesAsync()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return ApiResponseDto<IEnumerable<RoleDto>>.SuccessResponse(roleDtos);
        }
    }
}
