using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Tag;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.API.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SqlConnection _connection;

        public TagRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public async Task CreateTagAsync(Tag tag)
        {
            var sql = "INSERT INTO Tag (Name, Slug) VALUES (@Name, @Slug)";
            await _connection.ExecuteAsync(sql, new { Name = tag.Name, Slug = tag.Slug });
        }

        public async Task DeleteTagAsync(int id)
        {
            var sql = "DELETE FROM Tag WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<TagResponseDTO>> GetAllTagsAsync()
        {
            var sql = "SELECT Name, Slug FROM Tag";
            return (await _connection.QueryAsync<TagResponseDTO>(sql)).ToList();
        }

        public async Task<TagDTO> GetTagBySlugAsync(string slug)
        {
            var sql = "SELECT * FROM Tag WHERE Slug = @Slug";
            return await _connection.QueryFirstOrDefaultAsync<TagDTO>(sql, new { Slug = slug });
        }

        public async Task UpdateTagAsync(TagDTO tagDTO)
        {
            var sql = "UPDATE Tag SET Name = @Name, Slug = @Slug WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Name = tagDTO.Name, Slug = tagDTO.Slug, Id = tagDTO.Id });
        }
    }
}
