using Blog.API.Models.DTOs.User;

namespace Blog.API.Services.Interfaces
{
    public interface IUserService
    {
        public Task CreateUserAsync(UserRequestDTO userRequestDTO);

        public Task<List<UserResponseDTO>> GetAllUsersAsync();

        public Task<UserResponseDTO> GetUserByEmailAsync(string slug);

        public Task UpdateUserProfileAsync(string slug, UserProfileRequestDTO userRequestDTO);

        public Task DeleteUserAsync(string slug);
    }
}
