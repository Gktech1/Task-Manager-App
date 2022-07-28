using ApiTask.DTOs;
using ApiTask.Models;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ApiTask.Settings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AppUserToAddDto, AppUser>()
               .ForMember(Des => Des.UserName, option => option.MapFrom(src => src.Email));
            CreateMap<AppUser, UserDetailsToReturnDto>();
            CreateMap<Task, TaskToReturnDto>();
        }
    }
}
