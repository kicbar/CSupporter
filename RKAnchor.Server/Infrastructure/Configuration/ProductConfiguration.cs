using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(p => p.Description)
            .HasMaxLength(256);
    }
}
