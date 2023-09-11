using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.Models;
using PostService.Models.Dto;
using PostService.Services.IServices;

namespace PostService.Services
{
    public class PostServices : IPostInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PostServices(ApplicationDbContext context,IMapper mapper) 
        {         
            _context=context;
            _mapper = mapper;
        }
        public async Task<string> AddPostAsync(Post newPost)
        {
            var NewPost = _mapper.Map<Post>(newPost);  
            try
            {
                 _context.Posts.Add(NewPost);
                await _context.SaveChangesAsync();
                return "";
            }catch(Exception ex)
            {
                return ex.Message;
            }
          
        }

        public async Task<string> DeletePostAsync(Post post)
        {
            try
            {
                 _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return "";
            }catch(Exception ex)
            {
                return ex.Message;
            }
           
        }

        //Get user Posts
        public async Task<IEnumerable<Post>> GetAllPostsAsync(string UserId)
        {
            return await _context.Posts.Where(p=>p.UserId == UserId).ToListAsync();
           
        }

        public async Task<PostDto> GetPostByIdAsync(Guid id)
        {
            var result = await _context.Posts.Where(p => p.PostId == id).FirstOrDefaultAsync();
            var postToReturn = _mapper.Map<PostDto>(result);

            return postToReturn;
        }

        public async Task<string> UpdatePostAsync(Guid PostId, PostRequestDto updatedPost)
        {
            var postToUpdate = await _context.Posts.Where(p => p.PostId == PostId).FirstOrDefaultAsync();
           var _UpdatedPost = _mapper.Map<Post>(postToUpdate);
            try
            {
                _context.Posts.Update(_UpdatedPost);
                await _context.SaveChangesAsync();
                return "";
            }catch(Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
