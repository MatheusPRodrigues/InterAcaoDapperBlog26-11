using Blog.API.Models.DTOs.Tag;
using Blog.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateTagAsync(TagRequestDTO tag)
        {
            await _tagService.CreateTagAsync(tag);
            return Created();
        }

        [HttpDelete("Delete/{slug}")]
        public async Task<ActionResult> DeleteTagAsync(string slug)
        {
            await _tagService.DeleteTagAsync(slug);
            return NoContent();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<TagResponseDTO>>> GetAllTagsAsync()
        {
            var tags = await _tagService.GetAllTagsAsync();
            if (tags is null)
                return NotFound();

            return Ok(tags);
        }

        [HttpGet("GetBySlug/{slug}")]
        public async Task<ActionResult<TagResponseDTO>> GetTagBySlugAsync(string slug)
        {
            var tag = await _tagService.GetTagBySlugAsync(slug);
            if (tag is null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPut("Update/{slug}")]
        public async Task<ActionResult> UpdateTagAsync(string slug, TagRequestDTO tagRequestDTO)
        {
            await _tagService.UpdateTagAsync(slug, tagRequestDTO); 
            return Ok();
        }
    }
}
