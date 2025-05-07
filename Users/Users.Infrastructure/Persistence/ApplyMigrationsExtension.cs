using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure.Persistence
{
    public static class ApplyMigrationsExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<CalmTrackDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
