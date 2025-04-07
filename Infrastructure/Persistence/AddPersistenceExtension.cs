using Application.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class AddPersistenceExtension
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.AddDbContext<CalmTrackDbContext>();

            services.AddNpgsql<CalmTrackDbContext>(
                configurationManager.GetConnectionString("CalmTrackDb"));

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
