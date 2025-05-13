using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.User;

namespace Surveys.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasKey(user => user.Id);

            user.HasMany(user => user.InstancesOfCompletedSurveys)
                .WithOne(formInstance => formInstance.User)
                .HasForeignKey(formInstance => formInstance.UserId);
        }
    }
}
