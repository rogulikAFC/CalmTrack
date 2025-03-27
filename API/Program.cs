using Infrastructure.Auth;
using Infrastructure.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(Assembly.Load("Application")));

builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuth();

app.MapControllers();

app.Run();
