using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(64)
            .HasColumnOrder(2);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(128)
            .HasColumnOrder(3);

        builder.Property(x => x.FirstName)
            .HasMaxLength(64)
            .HasColumnOrder(4);

        builder.Property(x => x.LastName)
            .HasMaxLength(64)
            .HasColumnOrder(5);

        builder.HasOne(x => x.Role);
    }
}
