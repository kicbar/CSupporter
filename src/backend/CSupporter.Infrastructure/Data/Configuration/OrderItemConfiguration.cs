using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CSupporter.Infrastructure.Data.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(nameof(OrderItem));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductType)
               .HasMaxLength(32)
               .HasConversion(new EnumToStringConverter<ProductType>());

        builder.Property(x => x.Colour)
               .HasMaxLength(32);

        builder.Property(x => x.AdditionalInfo)
               .HasMaxLength(256);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId)
            .IsRequired();
    }
}
