using System.ComponentModel.DataAnnotations;

namespace PostService.Models.Dto
{
    public class PostRequestDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;
        [Required]
        public Guid UserId { get; set; }
    }
}
