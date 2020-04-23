using AutoMapper;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Identity;

namespace AssistenciaTecnica.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}