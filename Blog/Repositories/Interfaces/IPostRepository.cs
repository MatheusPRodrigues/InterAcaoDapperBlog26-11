using Blog.API.Models;
using Blog.API.Models.DTOs.Post;

namespace Blog.API.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public Task CreatePostAsync(Post post);

        public Task<int> SearchUserIdBySlugAsync(string slug);

        public Task<int> SearchCategoryIdBySlugAsync(string slug);

        public Task<List<int>> SearchTagsIdsBySlugAsync(List<string> slug);

        public Task<List<PostResponseDTO>> GetAllPostsAsync();

        public Task<PostResponseDTO?> GetPostBySlugAsync(string slug);

        public Task<int> SearchPostIdBySlugAsync(string slug);

        public Task<PostUpdateDTO?> GetPostUpdateDTOBySlugAsync(string slug);

        public Task UpdatePostAsync(int postId, PostUpdateDTO postUpdateDTO, string newSlug);
    }
}
