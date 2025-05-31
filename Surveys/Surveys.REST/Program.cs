using Surveys.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPersistence();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.ApplyMigrations();

app.UseAuthorization();

app.MapControllers();

app.Run();
