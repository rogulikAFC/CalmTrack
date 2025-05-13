using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Survey;

namespace Surveys.Infrastructure.Persistence.Configurations
{
    public class ScaleConfiguration : IEntityTypeConfiguration<Scale>
    {
        public void Configure(EntityTypeBuilder<Scale> scale)
        {
            scale.HasKey(scale => scale.Id);

            scale.HasIndex(scale => new
                {
                    scale.SurveyId,
                    scale.Value,
                })
                .IsUnique();

            scale.Property(scale => scale.From)
                .IsRequired();

            scale.Property(scale => scale.To)
                .IsRequired();

            scale.Property(scale => scale.Value)
                .IsRequired();

            scale.HasMany(scale => scale.FormInstances)
                .WithOne(formInstance => formInstance.Result)
                .HasForeignKey(formInstance => formInstance.ResultId);
        }
    }
}
