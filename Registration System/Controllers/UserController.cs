using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Models.Dtos;
using Registration_System.Services.IServices;

namespace Registration_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ResponseDto _response;
        private readonly IUserInterface _userInterface;

        public UserController(IUserInterface userInterface)
        {
            _response = new ResponseDto();
            _userInterface=userInterface;
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
            return Ok(_response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponceDto>> UserLogin(LoginRequestDto loginRequestDto)
        {
            var res = await _userInterface.GetLoginAsync(loginRequestDto);
            if(res.User == null)
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
