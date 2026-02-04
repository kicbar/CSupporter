using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CSupporter.Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderNo)
               .HasMaxLength(32);

        builder.Property(x => x.OrderDate)
               .HasMaxLength(32);

        builder.Property(x => x.ProducerType)
               .HasMaxLength(32)
               .HasConversion(new EnumToStringConverter<ProducerType>());

        builder.Property(x => x.AdditionalInfo)
               .HasMaxLength(512);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ClientId)
            .IsRequired();
    }
}
