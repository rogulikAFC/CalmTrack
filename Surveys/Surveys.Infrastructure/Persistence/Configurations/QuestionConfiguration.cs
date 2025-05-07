using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Survey;

namespace Surveys.Infrastructure.Persistence.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> question)
        {
            question.HasKey(question => question.Id);
            
            question.HasOne(question => question.Survey)
                .WithMany(survey => survey.Questions)
                .HasForeignKey(question => question.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            question.HasIndex(question => new
                {
                    question.SurveyId,
                    question.SerialNumber,
                })
                .IsUnique();

            question.HasMany(question => question.Answers)
                .WithOne(answer => answer.Question)
                .HasForeignKey(answer => answer.QuestionId);
        }
    }
}
