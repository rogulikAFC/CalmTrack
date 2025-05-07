using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Surveys.Infrastructure.Persistence
{
    public static class AddPersistenceExtension
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddNpgsql<SurveysDbContext>(
                configuration.GetConnectionString("SurveysDb"));

            return services;
        }
    }
}
