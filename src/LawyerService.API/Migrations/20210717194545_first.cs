using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LawyerService.API.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Lawyers",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropColumn(
                name: "LawyerId",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "dbo",
                table: "Lawyers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lawyers",
                schema: "dbo",
                table: "Lawyers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Lawyers",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.AddColumn<int>(
                name: "LawyerId",
                schema: "dbo",
                table: "Lawyers",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lawyers",
                schema: "dbo",
                table: "Lawyers",
                column: "LawyerId");
        }
    }
}
