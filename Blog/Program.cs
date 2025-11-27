using Blog.API.Data;
using Blog.API.Repositories;
using Blog.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// garante somente uma instância da classe ConnectionDB na aplicação
builder.Services.AddSingleton<ConnectionDB>();
builder.Services.AddSingleton<CategoryRepository>();
builder.Services.AddSingleton<CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
