using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Survey;

namespace Surveys.Infrastructure.Persistence.Configurations
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> survey)
        {
            survey.HasKey(survey => survey.Id);

            survey.Property(survey => survey.Name)
                .IsRequired()
                .HasMaxLength(32);

            survey.Property(survey => survey.Description)
                .IsRequired()
                .HasMaxLength(512);

            survey.HasMany(survey => survey.Questions)
                .WithOne(question => question.Survey)
                .HasForeignKey(question => question.SurveyId);

            survey.HasMany(survey => survey.Scales)
                .WithOne(scale => scale.Survey)
                .HasForeignKey(scale => scale.SurveyId);
        }
    }
}
