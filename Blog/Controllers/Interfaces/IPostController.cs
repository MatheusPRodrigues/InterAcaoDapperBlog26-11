using Blog.API.Models.DTOs.Post;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers.Interfaces
{
    public interface IPostController
    {        
        public Task<ActionResult> CreatePostAsync(PostRequestDTO postRequestDTO);

        public Task<ActionResult<List<PostResponseDTO>>> GetAllPostsAsync();

        public Task<ActionResult<PostResponseDTO>> GetPostBySlugAsync(string slug);

        public Task<ActionResult> UpdatePostAsync(string slug, PostUpdateDTO postUpdateDTO);
    }
}
