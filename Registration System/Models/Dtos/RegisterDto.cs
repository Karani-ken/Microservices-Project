using System.ComponentModel.DataAnnotations;

namespace Registration_System.Models.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }=string.Empty;


    }
}
