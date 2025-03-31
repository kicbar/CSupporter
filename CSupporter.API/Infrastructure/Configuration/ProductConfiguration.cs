using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(x => x.ProductCode)
            .HasMaxLength(16)
            .HasColumnOrder(2);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128)
            .HasColumnOrder(3);

        builder.Property(x => x.ProductType)
            .HasMaxLength(32)
            .HasColumnOrder(4);

        builder.Property(x => x.Description)
            .HasMaxLength(256)
            .HasColumnOrder(5);
    }
}
