using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Post;
using Blog.API.Models.DTOs.User;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.API.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SqlConnection _connection;

        public PostRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public async Task CreatePostAsync(Post post)
        {
            var sql = @"INSERT INTO Post (CategoryId, AuthorId, Title, Summary, Body, Slug)
                        VALUES (@CategoryId, @AuthorId, @Title, @Summary, @Body, @Slug);
                        SELECT SCOPE_IDENTITY();";

            var id = await _connection.ExecuteScalarAsync<int>(sql, new
            {
                CategoryId = post.CategoryId,
                AuthorId = post.AuthorId,
                Title = post.Title,
                Summary = post.Summary,
                Body = post.Body,
                Slug = post.Slug
            });

            var postTags = post.TagsId.Select(tagId => new
            {
                PostId = id,
                TagId = tagId
            });

            sql = "INSERT INTO PostTag (PostId, TagId) VALUES (@PostId, @TagId)";
            await _connection.ExecuteAsync(sql, postTags);
        }

        public async Task<List<PostResponseDTO>> GetAllPostsAsync()
        {
            var sql = @"SELECT 
                            u.[Name] AS AuthorName,
                            c.[Name] AS CategoryName,
                            p.CreateDate, p.LastUpdateDate, p.Title, p.Summary, p.Body, p.Slug,
                            t.[Name] AS TagName
                      FROM Post p
                      JOIN Category c
                      ON c.Id = p.CategoryId
                      JOIN [User] u
                      ON u.Id = p.AuthorId
                      JOIN PostTag pt
                      ON p.Id = pt.PostId
                      JOIN Tag t
                      ON t.Id = pt.TagId";

            var lookup = new Dictionary<string, PostResponseDTO>();

            await _connection.QueryAsync<PostResponseDTO, string, PostResponseDTO>(sql,
                (post, postTag) =>
                {
                    if (!lookup.TryGetValue(post.Slug, out var dto))
                    {
                        dto = new PostResponseDTO
                        {
                            AuthorName = post.AuthorName,
                            CategoryName = post.CategoryName,
                            CreateDate = post.CreateDate,
                            LastUpdateDate = post.LastUpdateDate,
                            Title = post.Title,
                            Summary = post.Summary,
                            Body = post.Body,
                            Slug = post.Slug,
                            TagName = post.TagName
                        };
                        lookup.Add(post.Slug, dto);
                    }
                    dto.TagName.Add(postTag);

                    return post;
                },
                splitOn: "TagName"    
            );

            var posts = lookup.Values.ToList();
            return posts;
        }

        public async Task<PostResponseDTO?> GetPostBySlugAsync(string slug)
        {
            var sql = @"SELECT 
                            u.[Name] AS AuthorName,
                            c.[Name] AS CategoryName,
                            p.CreateDate, p.LastUpdateDate, p.Title, p.Summary, p.Body, p.Slug,
                            t.[Name] AS TagName
                      FROM Post p
                      JOIN Category c
                      ON c.Id = p.CategoryId
                      JOIN [User] u
                      ON u.Id = p.AuthorId
                      JOIN PostTag pt
                      ON p.Id = pt.PostId
                      JOIN Tag t
                      ON t.Id = pt.TagId
                      WHERE p.Slug = @Slug";

            var lookup = new Dictionary<string, PostResponseDTO>();

            await _connection.QueryAsync<PostResponseDTO, string, PostResponseDTO>(sql,
                (post, postTag) =>
                {
                    if (!lookup.TryGetValue(post.Slug, out var dto))
                    {
                        dto = new PostResponseDTO
                        {
                            AuthorName = post.AuthorName,
                            CategoryName = post.CategoryName,
                            CreateDate = post.CreateDate,
                            LastUpdateDate = post.LastUpdateDate,
                            Title = post.Title,
                            Summary = post.Summary,
                            Body = post.Body,
                            Slug = post.Slug,
                            TagName = post.TagName
                        };
                        lookup.Add(post.Slug, dto);
                    }
                    dto.TagName.Add(postTag);

                    return post;
                },
                new {Slug = slug},
                splitOn: "TagName"
            );

            var post = lookup.Values.FirstOrDefault();
            return post;
        }

        public async Task<PostUpdateDTO?> GetPostUpdateDTOBySlugAsync(string slug)
        {
            var sql = "SELECT Title, Summary, Body FROM Post WHERE Slug = @Slug";
            return await _connection.QueryFirstOrDefaultAsync<PostUpdateDTO>(sql, new { Slug = slug });
        }

        public async Task<int> SearchCategoryIdBySlugAsync(string slug)
        {
            var sql = "SELECT Id FROM Category WHERE Slug = @Slug";
            return await _connection.QueryFirstOrDefaultAsync<int>(sql, new {Slug = slug});
        }

        public async Task<int> SearchPostIdBySlugAsync(string slug)
        {
            var sql = "SELECT Id FROM Post WHERE Slug = @Slug";
            return await _connection.QueryFirstOrDefaultAsync<int>(sql, new { Slug = slug });
        }

        public async Task<List<int>> SearchTagsIdsBySlugAsync(List<string> slug)
        {
            var sql = "SELECT Id FROM Tag WHERE Slug IN @Slug";
            return (await _connection.QueryAsync<int>(sql, new {Slug = slug})).ToList();
        }

        public async Task<int> SearchUserIdBySlugAsync(string slug)
        {
            var sql = "SELECT Id FROM [User] WHERE Slug = @Slug";
            return await _connection.QueryFirstOrDefaultAsync<int>(sql, new { Slug = slug });
        }

        public async Task UpdatePostAsync(int postId, PostUpdateDTO postUpdateDTO, string newSlug)
        {
            var sql = @"UPDATE Post SET
                            Title = @Title,
                            Summary = @Summary,
                            Body = @Body
                       WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new
            {
                Title = postUpdateDTO.Title,
                Summary = postUpdateDTO.Summary,
                Body = postUpdateDTO.Body,
                Id = postId
            });
        }
    }
}
