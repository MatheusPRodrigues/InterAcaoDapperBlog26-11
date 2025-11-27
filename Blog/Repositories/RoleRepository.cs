using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Role;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SqlConnection _connection;

        public RoleRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public async Task CreateRoleAsync(Role role)
        {
            var sql = "INSERT INTO Role (Name, Slug) VALUES (@Name, @Slug)";
            await _connection.ExecuteAsync(sql, new { Name = role.Name, Slug = role.Slug });
        }

        public async Task DeleteRoleAsync(int id)
        {
            var sql = "DELETE FROM Role WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<RoleResponseDTO>> GetAllRolesAsync()
        {
            var sql = "SELECT Name, Slug FROM Role";
            return (await _connection.QueryAsync<RoleResponseDTO>(sql)).ToList();
        }

        public async Task<RoleDTO> GetRoleBySlugAsync(string slug)
        {
            var sql = "SELECT * FROM Role WHERE Slug = @Slug";
            return await _connection.QueryFirstOrDefaultAsync<RoleDTO>(sql, new { Slug = slug });
        }

        public async Task UpdateRoleAsync(RoleDTO roleDTO)
        {
            var sql = "UPDATE Role SET Name = @Name, Slug = @Slug WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Name = roleDTO.Name, Slug = roleDTO.Slug, Id = roleDTO.Id });
        }
    }
}
