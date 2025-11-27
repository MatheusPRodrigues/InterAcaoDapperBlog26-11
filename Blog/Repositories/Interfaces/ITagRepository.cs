using Blog.API.Models;
using Blog.API.Models.DTOs.Role;
using Blog.API.Models.DTOs.Tag;

namespace Blog.API.Repositories.Interfaces
{
    public interface ITagRepository
    {
        public Task<List<TagResponseDTO>> GetAllTagsAsync();

        public Task<TagDTO> GetTagBySlugAsync(string slug);

        public Task CreateTagAsync(Tag tag);

        public Task UpdateTagAsync(TagDTO tagDTO);

        public Task DeleteTagAsync(int id);
    }
}
