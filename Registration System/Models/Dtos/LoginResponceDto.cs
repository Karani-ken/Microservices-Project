namespace Registration_System.Models.Dtos
{
    public class LoginResponceDto
    {
        public UserDto User { get; set; } = default!;

        public string Token { get; set; } = string.Empty;
    }
}
