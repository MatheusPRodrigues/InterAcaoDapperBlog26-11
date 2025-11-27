using Blog.API.Models;
using Blog.API.Models.DTOs;
using System.Data.Common;

namespace Blog.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();

        public Task<CategoryDTO> GetCategoryBySlugAsync(string slug);

        public Task CreateCategoryAsync(Category category);

        public Task UpdateCategoryAsync(CategoryDTO categoryDTO);

        public Task DeleteCategoryAsync(int id);
    }
}
