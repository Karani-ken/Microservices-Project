using Newtonsoft.Json;
using Registration_System.Models.Dtos;

namespace Registration_System.Services.IServices
{
    public class PostService:IPostInterface
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PostService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<PostDto>> GetPosts()
        {

            var client = _httpClientFactory.CreateClient("Post");
            var response = await client.GetAsync("/api/Post");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<PostDto>>(Convert.ToString(responseDto.Result));
            }
            return new List<PostDto>();
        }

      
    }
}
