using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CSupporter.Infrastructure.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.Property(x => x.FirstName)
               .HasMaxLength(128)
               .HasColumnOrder(2);

        builder.Property(x => x.LastName)
               .HasMaxLength(1280)
               .HasColumnOrder(3);

        builder.Property(x => x.ClientType)
               .HasMaxLength(32)
               .HasColumnOrder(4)
               .HasConversion(new EnumToStringConverter<ClientType>());
    }
}
