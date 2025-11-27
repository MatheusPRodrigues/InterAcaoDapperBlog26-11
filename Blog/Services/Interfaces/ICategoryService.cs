using Blog.API.Models;
using Blog.API.Models.DTOs;

namespace Blog.API.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();

        public Task<CategoryResponseDTO> GetCategoryBySlugAsync(string slug);

        public Task CreateCategoryAsync(CategoryRequestDTO category);

        public Task UpdateCategoryAsync(string slug, CategoryRequestDTO categoryRequestDTO);

        public Task DeleteCategoryAsync(string slug);
    }
}
