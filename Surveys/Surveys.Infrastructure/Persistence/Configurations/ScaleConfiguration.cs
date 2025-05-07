using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Survey;

namespace Surveys.Infrastructure.Persistence.Configurations
{
    public class ScaleConfiguration : IEntityTypeConfiguration<Scale>
    {
        public void Configure(EntityTypeBuilder<Scale> scale)
        {
            scale.HasKey(scale => new
            {
                scale.SurveyId,
                scale.Value,
            });

            scale.Property(scale => scale.From)
                .IsRequired();

            scale.Property(scale => scale.To)
                .IsRequired();

            scale.Property(scale => scale.Value)
                .IsRequired();
        }
    }
}
