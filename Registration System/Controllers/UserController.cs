using Microsoft.AspNetCore.Mvc;
using Registration_System.Models;
using Registration_System.Models.Dtos;
using Registration_System.Services.IServices;
using Service_Bus;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Registration_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ResponseDto _response;
        private readonly IUserInterface _userInterface;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;
        public UserController(IUserInterface userInterface)
        {
            _response = new ResponseDto();
            _userInterface = userInterface;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDto>> AddUser(RegisterDto registerDto)
        {
            var errorMessage = await _userInterface.RegisterUser(registerDto);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;

                return BadRequest(_response);
            }

            //send email to queue
            var queueName = _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();
            var message = new UserMessage()
            {
                Email = registerDto.Email,
                Name = registerDto.Name
            };
            await _messageBus.PublishMessage(message, queueName);
            return Ok(_response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponceDto>> UserLogin(LoginRequestDto loginRequestDto)
        {
            var res = await _userInterface.GetLoginAsync(loginRequestDto);
            if (res.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid Credentials";

                return BadRequest(_response);
            }
            _response.Result = res;
            return Ok(_response);
        }
        //get userPosts
        [HttpGet("Posts")]
        public async Task<ActionResult<PostDto>> UserPosts(string jwtToken)
        {
            var UserId = await GetUserIdFromToken(jwtToken);
            bool Converted = Guid.TryParse(UserId.ToString(), out Guid userId);
            if (Converted)
            {
                var res = await _userInterface.GetPostsAsync(userId);
                if (res == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Could not fetch Posts";
                    return BadRequest(_response);
                }
                _response.Result = res;
              
            }
            return Ok(_response);
        }
        private async Task<ActionResult<string>> GetUserIdFromToken(string JwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(JwtToken);

            Claim UserIdClaim = token.Claims.FirstOrDefault(c => c.Type == "Sub");
            if (UserIdClaim != null)
            {
                return UserIdClaim.Value;
            }
            return null;
        }
    }
}
