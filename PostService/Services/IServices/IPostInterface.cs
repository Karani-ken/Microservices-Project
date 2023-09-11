using PostService.Models;
using PostService.Models.Dto;

namespace PostService.Services.IServices
{
    public interface IPostInterface
    {
        Task<string> AddPostAsync(Post newPost);
        Task<string> DeletePostAsync(Post post);
        Task<string>UpdatePostAsync(Guid PostId,PostRequestDto updatedPost);
        Task<IEnumerable<Post>> GetAllUserPostsAsync(string UserId);
        Task<PostDto> GetPostByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllPosts();
    }
}
