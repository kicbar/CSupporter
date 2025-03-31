﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSupporter.API.Domain.Entities;

namespace CSupporter.API.Infrastructure.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.Property(x => x.RoleName)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnOrder(2);
    }
}
