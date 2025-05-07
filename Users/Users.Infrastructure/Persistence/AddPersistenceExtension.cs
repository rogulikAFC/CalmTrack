using Application.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence
{
    public static class AddPersistenceExtension
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services)
        {
            DotNetEnv.Env.Load("../../Users.API/.env");

            var postgresUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
            var postgresDb = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var postgresPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

            var connectionString = $"Database = {postgresDb}; Username = {postgresUser}; Password = {postgresPassword}; Host = db; Port = 5432;";

            services.AddDbContext<CalmTrackDbContext>();

            services.AddNpgsql<CalmTrackDbContext>(connectionString);

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
