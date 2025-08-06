using Mail.Domain.Template;
using Mail.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Mail.Infrastructure.Persistence;

public class MailDbContext(DbContextOptions<MailDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Template> Templates { get; set; }
    
    public DbSet<BanOnSending> BansOnSending { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MailDbContext).Assembly);

        modelBuilder.Entity<Template>().HasData(
            new Template
            {
                Name = "UserCreated",
                Subject = "CalmTrack account created",
                TemplateFileName = "UserCreatedMail.html",
            },
            new Template
            {
                Name = "UserDeleted",
                Subject = "CalmTrack account deleted",
                TemplateFileName = "UserDeletedMail.html",
            });
    }
}