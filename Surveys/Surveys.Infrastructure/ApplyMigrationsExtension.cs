using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Surveys.Infrastructure.Persistence
{
    public static class ApplyMigrationsExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<SurveysDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
