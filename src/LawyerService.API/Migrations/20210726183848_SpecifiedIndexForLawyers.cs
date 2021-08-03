using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyerService.API.Migrations
{
    public partial class SpecifiedIndexForLawyers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Lawyers_LicenseNumber",
                schema: "dbo",
                table: "Lawyers",
                column: "LicenseNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lawyers_LicenseNumber",
                schema: "dbo",
                table: "Lawyers");
        }
    }
}
