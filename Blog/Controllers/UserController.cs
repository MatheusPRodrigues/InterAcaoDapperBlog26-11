using Blog.API.Models.DTOs.User;
using Blog.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateUserAsync(UserRequestDTO userRequestDTO)
        {
            await _userService.CreateUserAsync(userRequestDTO);
            return Created();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users is null)
                return NotFound("Não foi encontrado usuários!");

            return Ok(users);
        }

        [HttpGet("GetBySlug/{slug}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserByEmailAsync(string slug)
        {
            var user = await _userService.GetUserByEmailAsync(slug);
            if (user is null)
                return NotFound("Usuário não encontrado!");
            
            return Ok(user);
        }

        [HttpPut("Update/{slug}")]
        public async Task<ActionResult> UpdateUserProfileAsync(string slug, UserProfileRequestDTO userRequestDTO)
        {
            try
            {
                await _userService.UpdateUserProfileAsync(slug, userRequestDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{slug}")]
        public async Task<ActionResult> DeleteUserAsync(string slug)
        {
            await _userService.DeleteUserAsync(slug);
            return NoContent();
        }
    }
}
