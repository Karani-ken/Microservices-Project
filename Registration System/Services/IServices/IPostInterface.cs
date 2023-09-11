using Registration_System.Models.Dtos;

namespace Registration_System.Services.IServices
{
    public interface IPostInterface
    {
        Task<IEnumerable<PostDto>> GetPosts();
    }
}
