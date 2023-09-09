using CommentsService.Models;
using CommentsService.Models.Dto;

namespace CommentsService.Services.IServices
{
    public interface ICommentsService
    {
        Task<string> AddCommentAsync(CommentRequestDto commentRequestDto);
        Task<IEnumerable<Comments>> GetAllCommentsAsync(Guid PostId);

        Task<string> DeleteComments(Comments comment);        

    }
}
