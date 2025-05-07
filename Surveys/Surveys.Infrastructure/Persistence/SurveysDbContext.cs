using Microsoft.EntityFrameworkCore;
using Surveys.Domain.Survey;
using Surveys.Domain.User;
using System.Reflection;

namespace Surveys.Infrastructure.Persistence
{
    public class SurveysDbContext(
        DbContextOptions<SurveysDbContext> options)
        : DbContext(options)
    {
        public DbSet<Survey> Surveys { get; set; } 

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }

        public DbSet<Scale> Scales { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<FormInstance> FormInstances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(SurveysDbContext).Assembly);
        }
    }
}
