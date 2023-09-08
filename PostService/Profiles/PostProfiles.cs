using AutoMapper;
using PostService.Models;
using PostService.Models.Dto;

namespace PostService.Profiles
{
    public class PostProfiles:Profile
    {
        public PostProfiles()
        {
            CreateMap<PostRequestDto, Post>().ReverseMap();
            CreateMap<PostDto, Post>().ReverseMap();    
        }
    }
}
