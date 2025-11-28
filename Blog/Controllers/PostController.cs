using Blog.API.Controllers.Interfaces;
using Blog.API.Models.DTOs.Post;
using Blog.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase, IPostController
    {
        private PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreatePostAsync(PostRequestDTO postRequestDTO)
        {
            try
            {
                await _postService.CreatePostAsync(postRequestDTO);
                return Created();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PostResponseDTO>>> GetAllPostsAsync()
        {
            var posts = await _postService.GetAllPostsAsync();
            if (posts is null)
                return NotFound();

            return Ok(posts);
        }

        [HttpGet("GetPostBySlug/{slug}")]
        public async Task<ActionResult<PostResponseDTO>> GetPostBySlugAsync(string slug)
        {
            var post = await _postService.GetPostBySlugAsync(slug);
            if (post is null)
                return NotFound("Post não encontrado!");

            return Ok(post);
        }

        [HttpPut("Update/{slug}")]
        public async Task<ActionResult> UpdatePostAsync(string slug, PostUpdateDTO postUpdateDTO)
        {
            try
            {
                await _postService.UpdatePostAsync(slug, postUpdateDTO);
                return Ok("Post atualizado com sucesso!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
