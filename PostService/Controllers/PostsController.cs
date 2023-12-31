﻿using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using PostService.Models.Dto;
using PostService.Services.IServices;
using System.Security.Claims;

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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreatePost(PostRequestDto postRequestDto)
        {
            var newPost = _mapper.Map<Post>(postRequestDto);

            //get user id from the token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            newPost.UserId = userIdClaim.Value;
            var response = await _postService.AddPostAsync(newPost);

            if (!string.IsNullOrWhiteSpace(response))
            {
                _response.IsSuccess = false;
                _response.Message = response;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        [Authorize]
        //get user posts
        [HttpGet("My Posts")]
        public async Task<ActionResult<IEnumerable<ResponseDto>>> GetUserPosts()
        {
            //get user id from the token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = userIdClaim.Value;
            var response = await _postService.GetAllUserPostsAsync(UserId);
            if (response == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not Get posts";
                return BadRequest(_response);
            }
            _response.Result = response;
            return Ok(_response);
        }
        //get all posts
        [HttpGet("All-Posts")]
        public async Task<ActionResult<IEnumerable<ResponseDto>>> GetAllPosts()
        {
            var response = await _postService.GetAllPosts();
            if (response == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not Get posts";
                return BadRequest(_response);
            }
            _response.Result = response;
            return Ok(_response);
        }
        //delete a post
        [HttpDelete]
        public async Task<ActionResult<ResponseDto>> DeletePost(Guid Id)
        {
            var postToDelete = await _postService.GetPostByIdAsync(Id);
            var response = await _postService.DeletePostAsync(postToDelete);

            if (!string.IsNullOrWhiteSpace(response))
            {
                _response.IsSuccess = false;
                _response.Message = response;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        //UpdatePost

    }
}
