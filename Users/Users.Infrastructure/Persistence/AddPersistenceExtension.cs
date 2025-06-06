﻿using Application.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class AddPersistenceExtension
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services)
        {
            var postgresUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
            var postgresDb = Environment.GetEnvironmentVariable("POSTGRES_USERS_DB");
            var postgresPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

            var connectionString = $"Database = {postgresDb}; Username = {postgresUser}; Password = {postgresPassword}; Host = users_db; Port = 5001;";

            services.AddDbContext<CalmTrackDbContext>();

            services.AddNpgsql<CalmTrackDbContext>(connectionString);

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
