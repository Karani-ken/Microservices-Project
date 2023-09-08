using Registration_System.Models.Dtos;

namespace Registration_System.Services.IServices
{
    public interface IUserInterface
    {
        //Add user
        Task<string> RegisterUser(RegisterDto registerDto);

        //get all users
        Task<IEnumerable<UserDto>> GetUsersAsync();
        //get a single user
        Task<UserDto> GetUserById(Guid id);
        //UserLogin
        Task<LoginResponceDto> GetLoginAsync(LoginRequestDto login);
       
       
    }
}
