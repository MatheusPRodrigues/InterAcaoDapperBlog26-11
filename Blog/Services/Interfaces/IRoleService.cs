using Blog.API.Models.DTOs.Category;
using Blog.API.Models.DTOs.Role;

namespace Blog.API.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<List<RoleResponseDTO>> GetAllRolesAsync();

        public Task<RoleResponseDTO> GetRoleBySlugAsync(string slug);

        public Task CreateRoleAsync(RoleRequestDTO role);

        public Task UpdateRoleAsync(string slug, RoleRequestDTO roleRequestDTO);

        public Task DeleteRoleAsync(string slug);
    }
}
