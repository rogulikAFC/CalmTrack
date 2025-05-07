using Infrastructure.Persistence;
using System.Reflection;
using Users.gRPC.Services;
using Users.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistence();

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(Assembly.Load("Users.Application")));

builder.Services.AddGrpc();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.MapGrpcService<UsersService>();

app.Run();
