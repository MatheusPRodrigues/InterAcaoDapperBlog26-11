using Blog.API.Models;
using Blog.API.Models.DTOs.User;

namespace Blog.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task CreateUser(User user);
        
        public Task<List<UserResponseDTO>> GetAllUsersAsync();
        
        public Task<UserResponseDTO> GetUserByEmailAsync(string slug);

        public Task<int> GetUserIdAsync(string slug);

        public Task UpdateUserProfileAsync(UserProfileUpdateDTO updateDTO);

        public Task DeleteUserAsync(int id);
    }
}
