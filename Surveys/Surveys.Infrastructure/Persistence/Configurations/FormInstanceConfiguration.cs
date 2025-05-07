using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Survey;

namespace Surveys.Infrastructure.Persistence.Configurations
{
    public class FormInstanceConfiguration 
        : IEntityTypeConfiguration<FormInstance>
    {
        public void Configure(EntityTypeBuilder<FormInstance> formInstance)
        {
            formInstance.HasKey(formInstance => formInstance.Id);

            formInstance.HasOne(formInstance => formInstance.User)
                .WithMany(user => user.InstancesOfCompletedSurvays)
                .HasForeignKey(formInstance => formInstance.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            formInstance.Property(formInstance => formInstance.DateOnly)
                .IsRequired();

            formInstance.HasMany(formInstance => formInstance.UserAnswers)
                .WithOne(userAnswer => userAnswer.FormInstance)
                .HasForeignKey(userAnswer => userAnswer.FormInstanceId);
        }
    }
}
