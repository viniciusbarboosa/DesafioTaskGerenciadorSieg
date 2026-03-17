using TaskManagerConsole.Api.Contexts;
using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Services;
using Scalar.AspNetCore;
using TaskManagerConsole.Api.Repository.Interfaces;
using TaskManagerConsole.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//CONEXOES COM BANCO
builder.Services.AddScoped<TaskDbContext>();

//REPOSITORIO
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();

//SERVICES
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
