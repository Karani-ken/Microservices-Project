namespace PostService.Models
{
    public class Post
    {
        public Guid PostId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;
        public string UserId { get; set; }
    }
}
