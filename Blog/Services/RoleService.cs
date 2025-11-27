using Blog.API.Models;
using Blog.API.Models.DTOs.Role;
using Blog.API.Repositories;
using Blog.API.Services.Interfaces;

namespace Blog.API.Services
{
    public class RoleService : IRoleService
    {
        private RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task CreateRoleAsync(RoleRequestDTO role)
        {
            var newRole = new Role(
                role.Name,
                role.Name.ToLower().Replace(" ", "-")
            );

            await _roleRepository.CreateRoleAsync(newRole);
        }

        public async Task<List<RoleResponseDTO>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            if (roles is null)
                return null;

            return roles;
        }

        public async Task<RoleResponseDTO> GetRoleBySlugAsync(string slug)
        {
            var role = await _roleRepository.GetRoleBySlugAsync(slug);
            if (role is null)
                return null;

            var roleResponse = new RoleResponseDTO
            {
                Name = role.Name,
                Slug = role.Slug
            };

            return roleResponse;
        }

        public async Task DeleteRoleAsync(string slug)
        {
            var role = await _roleRepository.GetRoleBySlugAsync(slug);
            await _roleRepository.DeleteRoleAsync(role.Id);
        }

        public async Task UpdateRoleAsync(string slug, RoleRequestDTO roleRequestDTO)
        {
            var role = await _roleRepository.GetRoleBySlugAsync(slug);
            var roleToUpdate = new RoleDTO
            {
                Id = role.Id,
                Name = roleRequestDTO.Name,
                Slug = roleRequestDTO.Name.ToLower().Replace(" ", "-")
            };
            await _roleRepository.UpdateRoleAsync(roleToUpdate);
        }
    }
}
