using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CSupporter.Infrastructure.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductCode)
            .HasMaxLength(16);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.ProductType)
            .HasMaxLength(32)
            .HasConversion(new EnumToStringConverter<ProductType>());

        builder.Property(x => x.Description)
            .HasMaxLength(256);
    }
}
