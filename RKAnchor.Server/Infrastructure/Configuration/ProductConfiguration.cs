using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ProductCode)
               .HasMaxLength(16);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(x => x.ProductType)
               .HasMaxLength(32);

        builder.Property(x => x.Description)
            .HasMaxLength(256);

        builder.Property(x => x.InsertDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.InsertUser)
            .HasMaxLength(32)
            .HasDefaultValue("sys_user");
             
        builder.Property(x => x.UpdateDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdateUser)
            .HasMaxLength(32)
            .HasDefaultValue("sys_user");
    }
}
