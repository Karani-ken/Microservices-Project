using AutoMapper;
using CommentsService.Models;
using CommentsService.Models.Dto;

namespace CommentsService.Profiles
{
    public class CommentsProfile:Profile
    {
        public CommentsProfile()
        {
            CreateMap<CommentRequestDto,Comments>().ReverseMap();
            CreateMap<CommentDto,Comments>().ReverseMap();
        }
    }
}
