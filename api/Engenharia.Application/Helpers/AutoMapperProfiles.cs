using AutoMapper;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Entities.Identity;

namespace AssistenciaTecnica.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, LoginRequestDto>().ReverseMap();
            CreateMap<User, LoginResponseDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}