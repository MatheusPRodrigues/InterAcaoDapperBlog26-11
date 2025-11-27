using Blog.API.Models;
using Blog.API.Models.DTOs.Tag;
using Blog.API.Repositories;
using Blog.API.Repositories.Interfaces;
using Blog.API.Services.Interfaces;

namespace Blog.API.Services
{
    public class TagService : ITagService
    {
        private TagRepository _tagRepository;

        public TagService(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task CreateTagAsync(TagRequestDTO tag)
        {
            var newTag = new Tag(
                tag.Name,
                tag.Name.ToLower().Replace(" ", "-")
            );

            await _tagRepository.CreateTagAsync(newTag);
        }

        public async Task DeleteTagAsync(string slug)
        {
            var tag = await _tagRepository.GetTagBySlugAsync(slug);
            await _tagRepository.DeleteTagAsync(tag.Id);
        }

        public async Task<List<TagResponseDTO>> GetAllTagsAsync()
        {
            var tags = await _tagRepository.GetAllTagsAsync();
            if (tags is null)
                return null;

            return tags;
        }

        public async Task<TagResponseDTO> GetTagBySlugAsync(string slug)
        {
            var tag = await _tagRepository.GetTagBySlugAsync(slug);
            if (tag is null)
                return null;

            return new TagResponseDTO
            {
                Name = tag.Name,
                Slug = tag.Slug,
            };
        }

        public async Task UpdateTagAsync(string slug, TagRequestDTO tagRequestDTO)
        {
            var tag = await _tagRepository.GetTagBySlugAsync(slug);
            var tagToUpdate = new TagDTO
            {
                Id = tag.Id,
                Name = tagRequestDTO.Name,
                Slug = tagRequestDTO.Name.ToLower().Replace(" ", "-")
            };

            await _tagRepository.UpdateTagAsync(tagToUpdate);
        }
    }
}
