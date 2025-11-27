using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        // Task gera uma thread de execução da função
        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var sql = "SELECT Name, Slug FROM Category";


            //await _connection.OpenAsync();

            // Usando ADO.NET
            //    var reader = await _connection.ExecuteReaderAsync(sql);

            //    while (reader.Read())
            //    {
            //        var category = new Category(
            //            reader["Name"].ToString(),
            //            reader["Slug"].ToString()
            //        );
            //        categories.Add(category);
            //    }

            return (await _connection.QueryAsync<CategoryResponseDTO>(sql)).ToList();
        }

        public async Task<CategoryDTO> GetCategoryBySlugAsync(string slug)
        {
            var sql = "SELECT * FROM Category WHERE Slug = @Slug";
            return await _connection.QueryFirstOrDefaultAsync<CategoryDTO>(sql, new { Slug = slug });
        }

        public async Task CreateCategoryAsync(Category category)
        {
            var sql = "INSERT INTO Category (Name, Slug) VALUES (@Name, @Slug)";

            //await _connection.OpenAsync();
            await _connection.ExecuteAsync(sql, new { category.Name, category.Slug });
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var sql = "UPDATE Category SET Name = @Name, Slug = @Slug WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { categoryDTO.Name, categoryDTO.Slug, categoryDTO.Id });
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var sql = "DELETE FROM Category WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
