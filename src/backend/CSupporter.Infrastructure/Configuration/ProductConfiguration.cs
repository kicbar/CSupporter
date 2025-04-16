using CSupporter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSupporter.Infrastructure.Configuration;

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
            .HasMaxLength(64)
            .HasColumnOrder(4);

        builder.Property(x => x.Description)
            .HasMaxLength(256)
            .HasColumnOrder(5);
    }
}
