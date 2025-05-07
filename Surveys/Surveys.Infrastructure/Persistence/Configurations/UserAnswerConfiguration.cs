using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Survey;

namespace Surveys.Infrastructure.Persistence.Configurations
{
    public class UserAnswerConfiguration
        : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> userAnswer)
        {
            userAnswer.HasKey(userAnswer => new
            {
                userAnswer.AnswerId,
                userAnswer.UserId,
                userAnswer.FormInstanceId,
            });

            userAnswer.HasOne(userAnswer => userAnswer.Answer)
                .WithMany()
                .HasForeignKey(userAnswer => userAnswer.AnswerId);

            userAnswer.HasOne(userAnswer => userAnswer.User)
                .WithMany()
                .HasForeignKey(userAnswer => userAnswer.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            userAnswer.HasOne(userAnswer => userAnswer.FormInstance)
                .WithMany(formInstance => formInstance.UserAnswers)
                .HasForeignKey(userAnswer => userAnswer.FormInstanceId);
        }
    }
}
