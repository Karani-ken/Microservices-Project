using AutoMapper;
using Registration_System.Models;
using Registration_System.Models.Dtos;

namespace Registration_System.Profiles
{
    public class UserProfiles:Profile
    {
     
        public UserProfiles()
        {
            //set email as username
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(reg => reg.Email));

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
