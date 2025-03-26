using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(user => user.RoleId)
                .IsRequired();

            builder.Property(user => user.FirstName)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(user => user.LastName)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(320);

            builder.Property(user => user.Password)
                .IsRequired();
        }
    }
}
