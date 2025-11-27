using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.User;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public async Task CreateUser(User user)
        {
            var sql = @"INSERT INTO [User] ([Name], Email, PasswordHash, Bio, [Image], Slug) VALUES
                        (@Name, @Email, @PasswordHash, @Bio, @Image, @Slug);
                        SELECT SCOPE_IDENTITY();";

            var id = await _connection.ExecuteScalarAsync<int>(sql, new
            {
                Name = user.Name,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Bio = user.Bio,
                Image = user.Image,
                Slug = user.Slug
            });

            var userRoles = user.RolesId.Select(roleId => new
            {
                UserId = id,
                RoleId = roleId
            });

            sql = "INSERT INTO UserRole (UserId, RoleId) VALUES (@UserId, @RoleId)";

            await _connection.ExecuteAsync(sql, userRoles);
        }

        public async Task DeleteUserAsync(int id)
        {
            var sql = "DELETE FROM UserRole WHERE UserId = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });

            sql = "DELETE FROM [User] WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var sql = @"SELECT 
                            u.Id,
                            u.Name,
                            u.Email,
                            u.Bio,
                            u.Image,
                            u.Slug,
                            r.Name AS RoleName
                        FROM [User] u
                        JOIN UserRole ur 
                        ON ur.UserId = u.Id
                        JOIN Role r 
                        ON r.Id = ur.RoleId
                        ";

            var lookup = new Dictionary<int, UserResponseDTO>();

            await _connection.QueryAsync<UserDTO, string, UserDTO>(
                sql,
                (user, roleName) =>
                {
                    if (!lookup.TryGetValue(user.Id, out var dto))
                    {
                        dto = new UserResponseDTO
                        {
                            Name = user.Name,
                            Email = user.Email,
                            Bio = user.Bio,
                            Image = user.Image,
                            Slug = user.Slug,
                            RolesName = new List<string>()
                        };
                        lookup.Add(user.Id, dto);
                    }
                    dto.RolesName.Add(roleName);

                    return user;
                },
                splitOn: "RoleName"
            );

            var users = lookup.Values.ToList();
            return users;
        }

        public async Task<UserResponseDTO> GetUserByEmailAsync(string slug)
        {
            var sql = @"SELECT 
                            u.Id,
                            u.Name,
                            u.Email,
                            u.Bio,
                            u.Image,
                            u.Slug,
                            r.Name AS RoleName
                        FROM [User] u
                        JOIN UserRole ur 
                        ON ur.UserId = u.Id
                        JOIN Role r 
                        ON r.Id = ur.RoleId
                        WHERE u.Slug = @Slug
                        ";

            var lookup = new Dictionary<int, UserResponseDTO>();

            await _connection.QueryAsync<UserDTO, string, UserDTO>(
                sql,
                (user, roleName) =>
                {
                    if (!lookup.TryGetValue(user.Id, out var dto))
                    {
                        dto = new UserResponseDTO
                        {
                            Name = user.Name,
                            Email = user.Email,
                            Bio = user.Bio,
                            Image = user.Image,
                            Slug = user.Slug,
                            RolesName = new List<string>()
                        };
                        lookup.Add(user.Id, dto);
                    }
                    dto.RolesName.Add(roleName);
                    return user;
                },
                new { Slug = slug },
                splitOn: "RoleName"
            );

            return lookup.Values.FirstOrDefault();
        }

        public async Task<int> GetUserIdAsync(string slug)
        {
            var sql = "SELECT Id FROM [User] WHERE Slug = @Slug";

            return await _connection.QueryFirstOrDefaultAsync<int>(sql, new { Slug = slug });
        }

        public async Task UpdateUserProfileAsync(UserProfileUpdateDTO updateDTO)
        {
            var sql = @"UPDATE [User] SET 
                            Name = @Name, Email = @Email, Bio = @Bio, Image = @Image, Slug = @Slug 
                       WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, updateDTO);
        }
    }
}
