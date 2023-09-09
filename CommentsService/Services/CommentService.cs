using AutoMapper;
using CommentsService.Data;
using CommentsService.Models;
using CommentsService.Models.Dto;
using CommentsService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CommentsService.Services
{
    public class CommentService : ICommentsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CommentService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }
        public async Task<string> AddCommentAsync(CommentRequestDto commentRequestDto)
        {
           var newComment = _mapper.Map<Comments>(commentRequestDto);
            try
            {
                _context.Comments.Add(newComment);
                await _context.SaveChangesAsync();
                return "";
            }catch(Exception ex)
            {
                return ex.Message;
            }
            
        }

        public async Task<string> DeleteComments(Comments comment)
        {
            try
            {
                 _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return "";
            }catch(Exception ex )
            {
                return ex.Message;
            }
           
        }

        public async Task<IEnumerable<Comments>> GetAllCommentsAsync(Guid PostId)
        {
            var Comments = await _context.Comments.Where(c => c.PostId == PostId).ToListAsync();
            return Comments;
        }
    }
}
