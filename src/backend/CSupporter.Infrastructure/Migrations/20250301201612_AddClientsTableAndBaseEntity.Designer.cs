﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CSupporter.Infrastructure.Data;

#nullable disable

namespace CSupporter.API.Migrations
{
    [DbContext(typeof(CsupporterDbContext))]
    [Migration("20250301201612_AddClientsTableAndBaseEntity")]
    partial class AddClientsTableAndBaseEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RKAnchor.Server.Domain.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(4);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("InsertDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnOrder(100)
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasDefaultValue("sys_user")
                        .HasColumnOrder(101);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnOrder(102)
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("UpdateUser")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasDefaultValue("sys_user")
                        .HasColumnOrder(103);

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RKAnchor.Server.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("InsertDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnOrder(100)
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasDefaultValue("sys_user")
                        .HasColumnOrder(101);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnOrder(102)
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("UpdateUser")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasDefaultValue("sys_user")
                        .HasColumnOrder(103);

                    b.HasKey("Id");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
