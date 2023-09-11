using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Registration_System.Data;
using Registration_System.Models;
using Registration_System.Models.Dtos;
using Registration_System.Services.IServices;

namespace Registration_System.Services
{
    public class UserService : IUserInterface
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IJwtInterface _jwtGenerator;
        private readonly IPostInterface _postInterface;
        public UserService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context, IJwtInterface jwtToken)
        {
            _mapper = mapper;
            _userManager=userManager;
            _context = context;
            _jwtGenerator = jwtToken;
        }
        public async Task<UserDto> GetUserById(Guid id)
        {
            var res = await _context.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
            var user = _mapper.Map<UserDto>(res);
            return user;
        }

        public Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            
            throw new NotImplementedException();
        }

        public async Task<string> RegisterUser(RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            try
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }catch(Exception ex)
            {
                return ex.Message;
            }
           
        }

       public async Task<LoginResponceDto> GetLoginAsync(LoginRequestDto login)
        {
            //checks if user exists
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == login.Username.ToLower());

            //check if password is the right one
            var isValid = await _userManager.CheckPasswordAsync(user, login.Password);
            //check if user is null or password is wrong
            if (!isValid || user == null)
            {
                new LoginRequestDto();
            }
            var token = _jwtGenerator.GenerateToken(user);
            var LoggedUser = new LoginResponceDto()
            {
                User = _mapper.Map<UserDto>(user),
                Token = token
            };
            return LoggedUser;
        }

      
    }
}
