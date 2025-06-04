using Surveys.Infrastructure.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPersistence();

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(Assembly.Load("Surveys.Application")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.ApplyMigrations();

app.UseAuthorization();

app.MapControllers();

app.Run();
