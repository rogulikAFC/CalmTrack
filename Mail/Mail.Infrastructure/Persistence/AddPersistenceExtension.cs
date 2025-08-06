using Mail.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mail.Infrastructure.Persistence;

public static class AddPersistenceExtension
{
    public static void AddPersistence(
        this IServiceCollection services)
    {
        var postgresUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
        var postgresDb = Environment.GetEnvironmentVariable("POSTGRES_MAIL_DB");
        var postgresPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        
        var connectionString = $"Database = {postgresDb}; Username = {postgresUser}; Password = {postgresPassword}; Host = mail_db; Port = 5003;";
        
        services.AddDbContext<MailDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}