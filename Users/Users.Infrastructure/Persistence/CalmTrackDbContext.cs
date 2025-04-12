using Domain.User;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CalmTrackDbContext : DbContext
    {
        public CalmTrackDbContext(DbContextOptions<CalmTrackDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
