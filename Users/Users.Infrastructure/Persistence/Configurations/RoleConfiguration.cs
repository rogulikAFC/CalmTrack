using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(role => role.Id);

            builder.Property(role => role.Name)
                .IsRequired()
                .HasMaxLength(16);

            builder.HasMany(role => role.Users)
                .WithOne(user => user.Role)
                .HasForeignKey(user => user.RoleId);

            builder.HasData([
                new Role(new Guid("e466e24a-bd04-4970-8767-ee53699d5e89"), "admin"),
                new Role(new Guid("6ba308b6-5391-44fa-adf2-af38ba6744c4"), "user")
            ]);
        }
    }
}
