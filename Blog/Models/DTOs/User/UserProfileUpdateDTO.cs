namespace Blog.API.Models.DTOs.User
{
    public class UserProfileUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
    }
}
