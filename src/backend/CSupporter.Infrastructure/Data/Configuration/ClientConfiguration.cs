using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Xml.XPath;

namespace CSupporter.Infrastructure.Data.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable(nameof(Client));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
               .HasMaxLength(128);

        builder.Property(x => x.LastName)
               .HasMaxLength(128);

        builder.Property(x => x.PhoneNumber)
               .HasMaxLength(16);

        builder.Property(x => x.Address)
               .HasMaxLength(128);

        builder.Property(x => x.Address)
               .HasMaxLength(32);

        builder.Property(x => x.ClientType)
               .HasMaxLength(32)
               .HasConversion(new EnumToStringConverter<ClientType>());
    }
}
