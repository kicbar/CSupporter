using CSupporter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSupporter.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.FirstName)
            .HasMaxLength(64);

        builder.Property(x => x.LastName)
            .HasMaxLength(64);

        builder.HasOne(x => x.Role);
    }
}
