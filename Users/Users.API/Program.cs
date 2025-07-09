using Infrastructure.Persistence;
using System.Reflection;
using Users.Infrastructure.Auth;
using Users.Infrastructure.Persistence;
using Users.Infrastructure.Kafka.UsersProducer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPersistence();

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddUsersProducer();

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(Assembly.Load("Users.Application")));

var app = builder.Build();

//app.UseHttpsRedirection();

app.ApplyMigrations();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
