namespace CommentsService.Models.Dto
{
    public class CommentRequestDto
    {
        public Guid PostId { get; set; }

        public string Details { get; set; }
    }
}
