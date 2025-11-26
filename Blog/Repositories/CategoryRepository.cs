using Blog.API.Data;
using Blog.API.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.API.Repositories
{
    public class CategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        // Task gera uma thread de execução da função
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var sql = "SELECT * FROM Category";
            var categories = new List<Category>();

            using (_connection)
            {
                await _connection.OpenAsync();

                var reader = await _connection.ExecuteReaderAsync(sql);

                while (reader.Read())
                {
                    var category = new Category(
                        reader["Name"].ToString(),
                        reader["Slug"].ToString()
                    );
                    categories.Add(category);
                }
            }
            return categories;
        }
    }
}
