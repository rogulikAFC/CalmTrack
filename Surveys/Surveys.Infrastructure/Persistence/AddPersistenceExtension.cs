using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Surveys.Application.UnitOfWork;

namespace Surveys.Infrastructure.Persistence
{
    public static class AddPersistenceExtension
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services)
        {
            var postgresUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
            var postgresDb = Environment.GetEnvironmentVariable("POSTGRES_SURVEYS_DB");
            var postgresPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

            var connectionString = $"Database = {postgresDb}; Username = {postgresUser}; Password = {postgresPassword}; Host = surveys_db; Port = 5433;";

            //services.AddDbContext<SurveysDbContext>();

            //services.AddNpgsql<SurveysDbContext>(connectionString);

            services.AddDbContext<SurveysDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
