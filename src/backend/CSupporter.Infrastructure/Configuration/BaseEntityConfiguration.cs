using CSupporter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSupporter.Infrastructure.Configuration;

public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnOrder(1);

        builder.Property(x => x.InsertDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnOrder(100);

        builder.Property(x => x.InsertUser)
            .HasMaxLength(32)
            .HasDefaultValue("sys_user")
            .HasColumnOrder(101);

        builder.Property(x => x.UpdateDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnOrder(102);

        builder.Property(x => x.UpdateUser)
            .HasMaxLength(32)
            .HasDefaultValue("sys_user")
            .HasColumnOrder(103);
    }
}