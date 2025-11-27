using Blog.API.Models.DTOs.Role;
using Blog.API.Models.DTOs.Tag;

namespace Blog.API.Services.Interfaces
{
    public interface ITagService
    {
        public Task<List<TagResponseDTO>> GetAllTagsAsync();

        public Task<TagResponseDTO> GetTagBySlugAsync(string slug);

        public Task CreateTagAsync(TagRequestDTO tag);

        public Task UpdateTagAsync(string slug, TagRequestDTO tagRequestDTO);

        public Task DeleteTagAsync(string slug);
    }
}
