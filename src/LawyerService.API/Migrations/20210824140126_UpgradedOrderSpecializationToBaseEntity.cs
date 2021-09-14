using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LawyerService.API.Migrations
{
    public partial class UpgradedOrderSpecializationToBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "OrderSpecializations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "OrderSpecializations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "dbo",
                table: "OrderSpecializations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "OrderSpecializations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "OrderSpecializations");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "OrderSpecializations");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "OrderSpecializations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "OrderSpecializations");
        }
    }
}
