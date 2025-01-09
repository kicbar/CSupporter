using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RKAnchor.Server.Migrations
{
    /// <inheritdoc />
    public partial class productExtend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "Products",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                defaultValue: "sys_user");

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Products",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Products",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "Products",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                defaultValue: "sys_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "Products");
        }
    }
}
