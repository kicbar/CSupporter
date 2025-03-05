using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Infrastructure.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.Property(x => x.RoleName)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnOrder(2);
    }
}
