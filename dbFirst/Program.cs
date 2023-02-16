using Microsoft.EntityFrameworkCore;
using dbFirst.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DemoContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=demo;Username=postgres;Password=123"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
