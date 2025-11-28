namespace Blog.API.Models.DTOs.Post
{
    public class PostRequestDTO
    {
        public string CategorySlug { get; set; }
        public string UserSlug { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; } 
        public string Body { get; set; }
        public List<string> TagsSlug { get; set; }
    }
}
