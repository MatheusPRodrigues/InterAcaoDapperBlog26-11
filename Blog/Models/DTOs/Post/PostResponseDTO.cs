namespace Blog.API.Models.DTOs.Post
{
    public class PostResponseDTO
    {
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public List<string> TagName { get; set; } = new();
    }
}
