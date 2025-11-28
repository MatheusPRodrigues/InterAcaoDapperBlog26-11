using Blog.API.Models.DTOs.Post;

namespace Blog.API.Services.Interfaces
{
    public interface IPostService
    {
        public Task CreatePostAsync(PostRequestDTO post);
        
        public Task<List<PostResponseDTO>> GetAllPostsAsync();

        public Task<PostResponseDTO> GetPostBySlugAsync(string slug);

        public Task UpdatePostAsync(string slug, PostUpdateDTO postUpdateDTO);
    }
}
