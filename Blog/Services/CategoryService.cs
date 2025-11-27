using Blog.API.Models;
using Blog.API.Models.DTOs.Category;
using Blog.API.Repositories;
using Blog.API.Services.Interfaces;
using System.Data.Common;

namespace Blog.API.Services
{
    public class CategoryService : ICategoryService
    {
        private CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<CategoryResponseDTO> GetCategoryBySlugAsync(string slug)
        {
            var category = await _categoryRepository.GetCategoryBySlugAsync(slug);
            if (category is null)
                return null;

            return new CategoryResponseDTO
            {
                Name = category.Name,
                Slug = category.Slug
            };
        }

        public async Task CreateCategoryAsync(CategoryRequestDTO category)
        {
            var newCategory = new Category(
                category.Name,
                category.Name.ToLower().Replace(" ", "-")
            );

            await _categoryRepository.CreateCategoryAsync(newCategory);
        }

        public async Task UpdateCategoryAsync(string slug, CategoryRequestDTO categoryRequestDTO)
        {
            var category = await _categoryRepository.GetCategoryBySlugAsync(slug);
            var categoryToUpdate = new CategoryDTO
            {
                Id = category.Id,
                Name = categoryRequestDTO.Name,
                Slug = categoryRequestDTO.Name.ToLower().Replace(" ", "-")
            };
            await _categoryRepository.UpdateCategoryAsync(categoryToUpdate);
        }

        public async Task DeleteCategoryAsync(string slug)
        {
            var category = await _categoryRepository.GetCategoryBySlugAsync(slug);
            await _categoryRepository.DeleteCategoryAsync(category.Id);
        }
    }
}
