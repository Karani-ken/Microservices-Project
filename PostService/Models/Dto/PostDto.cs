namespace PostService.Models.Dto
{
    public class PostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
