using Blog.API.Models;
using Blog.API.Models.DTOs.Post;
using Blog.API.Repositories;
using Blog.API.Services.Interfaces;

namespace Blog.API.Services
{
    public class PostService : IPostService
    {
        private PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task CreatePostAsync(PostRequestDTO postRequestDTO)
        {
            var categoryId = await _postRepository.SearchCategoryIdBySlugAsync(postRequestDTO.CategorySlug);
            if (categoryId == 0)
                throw new ArgumentException("Categoria não encontrada!");

            var authorId = await _postRepository.SearchUserIdBySlugAsync(postRequestDTO.UserSlug);
            if (authorId == 0)
                throw new ArgumentException("Usuário não encontrado!");

            var tagsIds = await _postRepository.SearchTagsIdsBySlugAsync(postRequestDTO.TagsSlug);
            if (tagsIds.Count == 0 || tagsIds is null)
                throw new ArgumentException("Tags não encontradas! É necessário pelo menos uma tag para cadastrar o post!");

            var post = new Post(
                categoryId,
                authorId,
                postRequestDTO.Title,
                postRequestDTO.Summary,
                postRequestDTO.Body,
                postRequestDTO.Title.ToLower().Replace(" ", "-"),
                tagsIds
            );

            await _postRepository.CreatePostAsync(post);
        }

        public async Task<List<PostResponseDTO>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            if (posts is null || posts.Count() == 0)
                return null;

            return posts;
        }

        public async Task<PostResponseDTO?> GetPostBySlugAsync(string slug)
        {
            var post = await _postRepository.GetPostBySlugAsync(slug);
            return post;
        }

        public async Task UpdatePostAsync(string slug, PostUpdateDTO postUpdateDTO)
        {
            var post = await _postRepository.GetPostUpdateDTOBySlugAsync(slug);
            if (post is null)
                throw new ArgumentException("Post não encontrado!");

            var postId = await _postRepository.SearchPostIdBySlugAsync(slug);

            var postToUpdate = new PostUpdateDTO
            {
                Title = String.IsNullOrEmpty(postUpdateDTO.Title) ? post.Title : postUpdateDTO.Title,
                Summary = String.IsNullOrEmpty(postUpdateDTO.Summary) ? post.Summary : postUpdateDTO.Summary,
                Body = String.IsNullOrEmpty(postUpdateDTO.Body) ? post.Body : postUpdateDTO.Body
            };

            string newSlug = postToUpdate.Title.ToLower().Replace(" ", "-");

            await _postRepository.UpdatePostAsync(postId, postToUpdate, newSlug);
        }
    }
}
