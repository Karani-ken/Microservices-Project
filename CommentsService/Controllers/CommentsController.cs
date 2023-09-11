using AutoMapper;
using CommentsService.Models;
using CommentsService.Models.Dto;
using CommentsService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommentsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentsService _commentService;
        private readonly ResponseDto _response;
        public CommentsController( IMapper mapper,ICommentsService service)
        {
            _mapper = mapper;
            _commentService = service;
            _response = new ResponseDto();
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddComment(CommentRequestDto commentRequestDto)
        {
            var newComment = _mapper.Map<Comments>(commentRequestDto);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            newComment.UserId = userIdClaim.Value;
            var response = await _commentService.AddCommentAsync(newComment);
            if (!string.IsNullOrWhiteSpace(response))
            {
                _response.IsSuccess = false;
                _response.Message = response;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        [Authorize]
        [HttpGet("Get-Post-Comments")]
        public async Task<ActionResult<ResponseDto>> GetPostComments(Guid PostId)
        {
            var response = await _commentService.GetAllCommentsAsync(PostId);
            if (response == null)
            {
                _response.IsSuccess = false;
                _response.Message = "No comments to fetch";
                return BadRequest(_response);
            }
            var mappedResponse = _mapper.Map<CommentDto>(response);
            _response.Result = mappedResponse;
            return Ok(_response);
        }

    }
}
