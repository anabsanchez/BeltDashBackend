using AutoMapper;
using BeltDash.Application.DTOs.Auth;
using BeltDash.Application.DTOs.Role;
using BeltDash.Application.DTOs.Score;
using BeltDash.Application.DTOs.User;
using BeltDash.Domain.Entities;

namespace BeltDash.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role!.Name));

            CreateMap<UpdateUserDto, User>();

            CreateMap<Role, RoleDto>();

            CreateMap<Score, ScoreDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User!.Username));

            CreateMap<CreateScoreDto, Score>();
        }
    }
}
