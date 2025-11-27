using Blog.API.Models;
using Blog.API.Models.DTOs.User;
using Blog.API.Repositories;
using Blog.API.Services.Interfaces;

namespace Blog.API.Services
{
    public class UserService : IUserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(UserRequestDTO userRequestDTO)
        {
            var user = new User(
                userRequestDTO.Name,
                userRequestDTO.Email,
                userRequestDTO.PasswordHash,
                userRequestDTO.Bio,
                userRequestDTO.Image,
                userRequestDTO.Name.ToLower().Replace(" ", "-"),
                userRequestDTO.RolesId
            );
            await _userRepository.CreateUser(user);
        }

        public async Task DeleteUserAsync(string slug)
        {
            var userId = await _userRepository.GetUserIdAsync(slug);
            await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (users is null)
                return null;

            return users;
        }

        public async Task<UserResponseDTO> GetUserByEmailAsync(string slug)
        {
            var user = await _userRepository.GetUserByEmailAsync(slug);
            if (user is null)
                return null;

            return user;
        }

        public async Task UpdateUserProfileAsync(string slug, UserProfileRequestDTO userRequestDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(slug) ??
                throw new Exception("Usuário não encontrado!");

            var userId = await _userRepository.GetUserIdAsync(slug);

            var userToUpdate = new UserProfileUpdateDTO
            {
                Id = userId,
                Name = String.IsNullOrEmpty(userRequestDTO.Name) ? user.Name : userRequestDTO.Name,
                Email = String.IsNullOrEmpty(userRequestDTO.Email) ? user.Email : userRequestDTO.Email,
                Bio = String.IsNullOrEmpty(userRequestDTO.Bio) ? user.Bio : userRequestDTO.Bio,
                Image = String.IsNullOrEmpty(userRequestDTO.Image) ? user.Image : userRequestDTO.Image,
                Slug = String.IsNullOrEmpty(userRequestDTO.Name) ?
                                    user.Name.ToLower().Replace(" ", "-") :
                                    userRequestDTO.Name.ToLower().Replace(" ", "-")
            };

            await _userRepository.UpdateUserProfileAsync(userToUpdate);
        }
    }
}
