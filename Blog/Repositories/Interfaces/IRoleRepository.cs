using Blog.API.Models;
using Blog.API.Models.DTOs.Category;
using Blog.API.Models.DTOs.Role;

namespace Blog.API.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        public Task<List<RoleResponseDTO>> GetAllRolesAsync();

        public Task<RoleDTO> GetRoleBySlugAsync(string slug);

        public Task CreateRoleAsync(Role role);

        public Task UpdateRoleAsync(RoleDTO roleDTO);

        public Task DeleteRoleAsync(int id);
    }
}
