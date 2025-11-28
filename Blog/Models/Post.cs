namespace Blog.API.Models
{
    public class Post
    {
        public int Id { get; private set; }
        public int CategoryId { get; private set; }
        public int AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Body { get; private set; }
        public string Slug { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? LastUpdateDate { get; private set; }
        public List<int> TagsId { get; private set; } = new();

        public Post(
            int categoryId,
            int authorId, 
            string title,
            string summary, 
            string body,
            string slug,
            List<int> tagsId
        )
        {
            CategoryId = categoryId;
            AuthorId = authorId;
            Title = title;
            Summary = summary;
            Body = body;
            Slug = slug;
            TagsId = tagsId;
        }
    }
}
