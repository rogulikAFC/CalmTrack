using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Survey;

namespace Surveys.Infrastructure.Persistence.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> answer)
        {
            answer.HasKey(answer => answer.AnswerId);

            answer.HasOne(answer => answer.Question)
                .WithMany(question => question.Answers)
                .HasForeignKey(answer => answer.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            answer.Property(answer => answer.AnswerText)
                .IsRequired()
                .HasMaxLength(32);

            answer.Property(answer => answer.Value)
                .IsRequired();

            answer.HasIndex(answer => new
                {
                    answer.AnswerText,
                    answer.QuestionId,
                })
                .IsUnique();
        }
    }
}
