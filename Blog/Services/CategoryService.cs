using Blog.API.Models;
using Blog.API.Repositories;

namespace Blog.API.Services
{
    public class CategoryService
    {
        private CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

    }
}
