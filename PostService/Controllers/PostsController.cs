﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PostService.Models.Dto;
using PostService.Services.IServices;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostInterface _postService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public PostsController(IPostInterface post, IMapper mapper)
        {
            _mapper = mapper;
            _postService = post;
            _response = new ResponseDto();
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreatePost(PostRequestDto postRequestDto)
        {
            var response = await _postService.AddPostAsync(postRequestDto);
            if (!string.IsNullOrWhiteSpace(response))
            {
                _response.IsSuccess = false;
                _response.Message = response;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseDto>>> GetUserPosts(Guid UserId)
        {
            var response = await _postService.GetAllPostsAsync(UserId);
            if (response == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not Get posts";
                return BadRequest(_response);
            }
            _response.Result = response;
            return Ok(_response);
        } 
        
    }
}