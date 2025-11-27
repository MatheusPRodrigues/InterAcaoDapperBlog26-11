using Blog.API.Models.DTOs.Role;
using Blog.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateRoleAsync(RoleRequestDTO role)
        {
            await _roleService.CreateRoleAsync(role);
            return Created();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<RoleResponseDTO>>> GetAllRolesAsync()
        {
            var roles = await _roleService.GetAllRolesAsync();
            if (roles is null)
                return NotFound();

            return Ok(roles);
        }

        [HttpGet("GetBySlug/{slug}")]
        public async Task<ActionResult<RoleResponseDTO>> GetRoleBySlugAsync(string slug)
        {
            var role = await _roleService.GetRoleBySlugAsync(slug);
            if (role is null)
                return NotFound();
            
            return Ok(role);
        }

        [HttpPut("Update/{slug}")]
        public async Task<ActionResult> UpdateRoleAsync(string slug, RoleRequestDTO roleRequestDTO)
        {
            await _roleService.UpdateRoleAsync(slug, roleRequestDTO);
            return Ok();
        }

        [HttpDelete("Delete/{slug}")]
        public async Task<ActionResult> DeleteRoleAsync(string slug)
        {
            await _roleService.DeleteRoleAsync(slug);
            return NoContent();
        }
    }
}
