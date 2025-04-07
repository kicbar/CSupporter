using CSupporter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSupporter.API.Infrastructure.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Products");

        builder.Property(x => x.FirstName)
               .HasMaxLength(128)
               .HasColumnOrder(2);

        builder.Property(x => x.LastName)
               .HasMaxLength(128)
               .HasColumnOrder(3);

        builder.Property(x => x.ClientType)
               .HasMaxLength(32)
               .HasColumnOrder(4);
    }
}
