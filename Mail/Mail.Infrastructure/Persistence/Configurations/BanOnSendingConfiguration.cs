using Mail.Domain.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mail.Infrastructure.Persistence.Configurations;

public class BanOnSendingConfiguration : IEntityTypeConfiguration<BanOnSending>
{
    public void Configure(EntityTypeBuilder<BanOnSending> banOnSending)
    {
        banOnSending.HasKey(bos => new
        {
            bos.TemplateName,
            bos.UserId
        });

        banOnSending.HasOne(bos => bos.Template)
            .WithMany(t => t.BansOnSending)
            .HasForeignKey(bos => bos.TemplateName)
            .IsRequired();

        banOnSending.HasOne(bos => bos.User)
            .WithMany(u => u.BansOnSending)
            .HasForeignKey(bos => bos.UserId)
            .IsRequired();
    }
}