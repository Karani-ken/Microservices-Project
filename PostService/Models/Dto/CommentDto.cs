namespace PostService.Models.Dto
{
    public class CommentDto
    {
        public Guid PostId { get; set; }

        public string Details { get; set; }
    }
}
