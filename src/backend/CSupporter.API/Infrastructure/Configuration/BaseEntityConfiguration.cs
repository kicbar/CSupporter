using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSupporter.API.Domain.Entities;

namespace CSupporter.API.Infrastructure.Configuration;

public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnOrder(1);

        builder.Property(x => x.InsertDate)
            .HasDefaultValueSql("GETDATE()")
            .HasColumnOrder(100);

        builder.Property(x => x.InsertUser)
            .HasMaxLength(32)
            .HasDefaultValue("sys_user")
            .HasColumnOrder(101);

        builder.Property(x => x.UpdateDate)
            .HasDefaultValueSql("GETDATE()")
            .HasColumnOrder(102);

        builder.Property(x => x.UpdateUser)
            .HasMaxLength(32)
            .HasDefaultValue("sys_user")
            .HasColumnOrder(103);
    }
}