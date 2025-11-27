using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs;
using Blog.API.Services;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult HeartBeat()
        {
            return Ok("Category controller is alive!");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("GetBySlug/{slug}")]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryBySlugAsync(string slug)
        {
            var category = await _categoryService.GetCategoryBySlugAsync(slug);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateCategory(CategoryRequestDTO category)
        {
            await _categoryService.CreateCategoryAsync(category);
            return Created();
        }

        [HttpPut("Update/{slug}")]
        public async Task<ActionResult> UpdateCategoryAsync(string slug, CategoryRequestDTO category)
        {
            await _categoryService.UpdateCategoryAsync(slug, category);
            return Ok();
        }

        [HttpDelete("Delete/{slug}")]
        public async Task<ActionResult> DeleteCategoryAsync(string slug)
        {
            await _categoryService.DeleteCategoryAsync(slug);
            return NoContent();
        }
    }
}
