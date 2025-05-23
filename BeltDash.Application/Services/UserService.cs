using AutoMapper;
using BeltDash.Application.DTOs.Common;
using BeltDash.Application.DTOs.User;
using BeltDash.Application.Interfaces;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Application.Services
{
    /// <summary>
    /// Service responsible for managing users within the application.
    /// Implements the IUserService interface and provides CRUD operations for users.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// User service constructor.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for repository access</param>
        /// <param name="mapper">Mapper for converting between entities and DTOs</param>
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all users registered in the system.
        /// </summary>
        /// <returns>Response containing a collection of user DTOs</returns>
        public async Task<ApiResponseDto<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            // Retrieve all users from the repository
            var users = await _unitOfWork.Users.GetAllUsersWithRolesAsync();
            // Map user entities to DTOs
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            // Return a successful response with the user DTOs
            return ApiResponseDto<IEnumerable<UserDto>>.SuccessResponse(userDtos);
        }

        /// <summary>
        /// Retrieves a specific user by their identifier.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Response containing the user DTO or an error if not found</returns>
        public async Task<ApiResponseDto<UserDto>> GetUserByIdAsync(int id)
        {
            // Retrieve the user with their associated role from the repository
            var user = await _unitOfWork.Users.GetUserWithRoleAsync(id);
            if (user == null)
            {
                // If the user does not exist, return an error response
                return ApiResponseDto<UserDto>.ErrorResponse("User not found.", 404);
            }

            // Map the user entity to a DTO
            var userDto = _mapper.Map<UserDto>(user);
            // Return a successful response with the user DTO
            return ApiResponseDto<UserDto>.SuccessResponse(userDto);
        }

        /// <summary>
        /// Updates an existing user's information.
        /// </summary>
        /// <param name="id">Identifier of the user to update</param>
        /// <param name="updateUserDto">DTO containing updated data</param>
        /// <returns>Response with the updated user DTO or an error</returns>
        public async Task<ApiResponseDto<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            // Retrieve the user by their ID
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                // If the user does not exist, return an error response
                return ApiResponseDto<UserDto>.ErrorResponse("User not found.", 404);
            }

            // Check if the email has changed and is already in use by another user
            if (user.Email != updateUserDto.Email)
            {
                var existingUserWithEmail = await _unitOfWork.Users.GetByEmailAsync(updateUserDto.Email);
                if (existingUserWithEmail != null && existingUserWithEmail.Id != id)
                {
                    // If the email is already in use, return an error response
                    return ApiResponseDto<UserDto>.ErrorResponse("Email is already in use.", 400);
                }
            }

            // Check if the username has changed and is already in use by another user
            if (user.Username != updateUserDto.Username)
            {
                var existingUserWithUsername = await _unitOfWork.Users.GetByUsernameAsync(updateUserDto.Username);
                if (existingUserWithUsername != null && existingUserWithUsername.Id != id)
                {
                    // If the username is already taken, return an error response
                    return ApiResponseDto<UserDto>.ErrorResponse("Username is already taken.", 400);
                }
            }

            // Update user data
            user.Username = updateUserDto.Username;
            user.Email = updateUserDto.Email;
            user.UpdatedAt = DateTime.UtcNow; // Update the modification timestamp

            // Persist changes to the database
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            // Retrieve the updated user with their associated role
            var updatedUser = await _unitOfWork.Users.GetUserWithRoleAsync(id);
            var userDto = _mapper.Map<UserDto>(updatedUser);

            // Return a successful response with the updated user DTO
            return ApiResponseDto<UserDto>.SuccessResponse(userDto);
        }

        /// <summary>
        /// Updates a user's status.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="updateStatusDto">DTO containing the new status</param>
        /// <returns>Response with the updated user DTO or an error</returns>
        public async Task<ApiResponseDto<UserDto>> UpdateUserStatusAsync(int id, UpdateUserStatusDto updateStatusDto)
        {
            // Retrieve the user by their ID
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                // If the user does not exist, return an error response
                return ApiResponseDto<UserDto>.ErrorResponse("User not found.", 404);
            }

            // Update the user's status
            user.Status = updateStatusDto.Status;
            user.UpdatedAt = DateTime.UtcNow; // Update the modification timestamp

            // Persist changes to the database
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            // Retrieve the updated user with their associated role
            var updatedUser = await _unitOfWork.Users.GetUserWithRoleAsync(id);
            var userDto = _mapper.Map<UserDto>(updatedUser);

            // Return a successful response with the updated user DTO
            return ApiResponseDto<UserDto>.SuccessResponse(userDto);
        }

        /// <summary>
        /// Updates a user's role.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="updateRoleDto">DTO containing the new role</param>
        /// <returns>Response with the updated user DTO or an error</returns>
        public async Task<ApiResponseDto<UserDto>> UpdateUserRoleAsync(int id, UpdateUserRoleDto updateRoleDto)
        {
            // Retrieve the user by their ID
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                // If the user does not exist, return an error response
                return ApiResponseDto<UserDto>.ErrorResponse("User not found.", 404);
            }

            // Check if the specified role exists
            var role = await _unitOfWork.Roles.GetByIdAsync(updateRoleDto.RoleId);
            if (role == null)
            {
                // If the role does not exist, return an error response
                return ApiResponseDto<UserDto>.ErrorResponse("Role not found.", 404);
            }

            // Update the user's role
            user.RoleId = updateRoleDto.RoleId;
            user.UpdatedAt = DateTime.UtcNow; // Update the modification timestamp

            // Persist changes to the database
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            // Retrieve the updated user with their associated role
            var updatedUser = await _unitOfWork.Users.GetUserWithRoleAsync(id);
            var userDto = _mapper.Map<UserDto>(updatedUser);

            // Return a successful response with the updated user DTO
            return ApiResponseDto<UserDto>.SuccessResponse(userDto);
        }

        /// <summary>
        /// Deletes a user from the system.
        /// </summary>
        /// <param name="id">Identifier of the user to delete</param>
        /// <returns>Response indicating operation success or an error</returns>
        public async Task<ApiResponseDto<bool>> DeleteUserAsync(int id)
        {
            // Retrieve the user by their ID
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                // If the user does not exist, return an error response
                return ApiResponseDto<bool>.ErrorResponse("User not found.", 404);
            }

            // Delete the user
            _unitOfWork.Users.Delete(user);
            await _unitOfWork.SaveChangesAsync();

            // Return a successful response indicating the deletion was successful
            return ApiResponseDto<bool>.SuccessResponse(true);
        }
    }
}
