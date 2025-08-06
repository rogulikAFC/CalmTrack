using Mail.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mail.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> user)
    {
        user.HasKey(u => u.Id);

        user.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(32);

        user.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(32);

        user.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(320);
    }
}