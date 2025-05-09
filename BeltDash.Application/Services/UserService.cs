using AutoMapper;
using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.User;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDto<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllUsersWithRolesAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return ApiResponseDto<IEnumerable<UserDto>>.SuccessResponse(userDtos);
        }

        public async Task<ApiResponseDto<UserDto>> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetUserWithRoleAsync(id);
            if (user == null)
            {
                return ApiResponseDto<UserDto>.ErrorResponse("User not found.", 404);
            }

            var userDto = _mapper.Map<UserDto>(user);
            return ApiResponseDto<UserDto>.SuccessResponse(userDto);
        }

        public async Task<ApiResponseDto<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponseDto<UserDto>.ErrorResponse("User not found.", 404);
            }

            if (user.Email != updateUserDto.Email)
            {
                var existingUserWithEmail = await _unitOfWork.Users.GetByEmailAsync(updateUserDto.Email);
                if (existingUserWithEmail != null && existingUserWithEmail.Id != id)
                {
                    return ApiResponseDto<UserDto>.ErrorResponse("Email is already in use.", 400);
                }
            }

            if (user.Username != updateUserDto.Username)
            {
                var existingUserWithUsername = await _unitOfWork.Users.GetByUsernameAsync(updateUserDto.Username);
                if (existingUserWithUsername != null && existingUserWithUsername.Id != id)
                {
                    return ApiResponseDto<UserDto>.ErrorResponse("Username is already taken.", 400);
                }
            }

            user.Username = updateUserDto.Username;
            user.Email = updateUserDto.Email;
            user.UpdatedAt = DateTime.UtcNow; 

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            var updatedUser = await _unitOfWork.Users.GetUserWithRoleAsync(id);
            var userDto = _mapper.Map<UserDto>(updatedUser);

            return ApiResponseDto<UserDto>.SuccessResponse(userDto);
        }

        public async Task<ApiResponseDto<UserDto>> UpdateUserStatusAsync(int id, UpdateUserStatusDto updateStatusDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponseDto<UserDto>.ErrorResponse("User not found.", 404);
            }

            user.Status = updateStatusDto.Status;
            user.UpdatedAt = DateTime.UtcNow; 

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            var updatedUser = await _unitOfWork.Users.GetUserWithRoleAsync(id);
            var userDto = _mapper.Map<UserDto>(updatedUser);

            return ApiResponseDto<UserDto>.SuccessResponse(userDto);
        }

        public async Task<ApiResponseDto<UserDto>> UpdateUserRoleAsync(int id, UpdateUserRoleDto updateRoleDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponseDto<UserDto>.ErrorResponse("User not found.", 404);
            }

            var role = await _unitOfWork.Roles.GetByIdAsync(updateRoleDto.RoleId);
            if (role == null)
            {
                return ApiResponseDto<UserDto>.ErrorResponse("Role not found.", 404);
            }

            user.RoleId = updateRoleDto.RoleId;
            user.UpdatedAt = DateTime.UtcNow; 

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            var updatedUser = await _unitOfWork.Users.GetUserWithRoleAsync(id);
            var userDto = _mapper.Map<UserDto>(updatedUser);

            return ApiResponseDto<UserDto>.SuccessResponse(userDto);
        }

        public async Task<ApiResponseDto<bool>> DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponseDto<bool>.ErrorResponse("User not found.", 404);
            }

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponseDto<bool>.SuccessResponse(true);
        }
    }
}
