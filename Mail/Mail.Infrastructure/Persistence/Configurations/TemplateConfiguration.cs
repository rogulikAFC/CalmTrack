using Mail.Domain.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mail.Infrastructure.Persistence.Configurations;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> template)
    {
        template.HasKey(t => t.Name);
        
        template.Property(t => t.Name)
            .IsRequired();

        template.Property(t => t.Subject)
            .IsRequired();

        template.Property(t => t.TemplateFileName)
            .IsRequired();
    }
}