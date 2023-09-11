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
        public UserController(IUserInterface userInterface, IMessageBus message, IConfiguration configuration)
        {
            _response = new ResponseDto();
            _userInterface = userInterface;
            _configuration = configuration;
            _messageBus= message;
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
      
       
    }
}
