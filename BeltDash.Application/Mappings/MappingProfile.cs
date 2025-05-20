using AutoMapper;
using BeltDash.Application.DTOs.Auth;
using BeltDash.Application.DTOs.Role;
using BeltDash.Application.DTOs.Score;
using BeltDash.Application.DTOs.User;
using BeltDash.Domain.Entities;

namespace BeltDash.Application.Mappings
{
    /// <summary>
    /// AutoMapper profile to configure mappings between domain entities and application DTOs.
    /// Provides mapping rules for User, Role, and Score related objects.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Configures the mapping definitions between entities and DTOs.
        /// </summary>
        public MappingProfile()
        {
            // Map User entity to UserDto, mapping Role name explicitly
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role!.Name));

            // Map UpdateUserDto to User entity for updating user data
            CreateMap<UpdateUserDto, User>();

            // Map Role entity to RoleDto
            CreateMap<Role, RoleDto>();

            // Map Score entity to ScoreDto, mapping Username explicitly
            CreateMap<Score, ScoreDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User!.Username));

            // Map CreateScoreDto to Score entity for creating new scores
            CreateMap<CreateScoreDto, Score>();
        }
    }
}
