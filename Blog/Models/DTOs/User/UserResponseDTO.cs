namespace Blog.API.Models.DTOs.User
{
    public class UserResponseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public List<string> RolesName { get; set; }
    }
}
