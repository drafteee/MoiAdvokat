using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyerService.API.Migrations
{
    public partial class changeAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                schema: "dbo",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CountryId",
                schema: "dbo",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "dbo",
                table: "Addresses");

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                schema: "dbo",
                table: "AdministrativeTerritories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeTerritories_CountryId",
                schema: "dbo",
                table: "AdministrativeTerritories",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdministrativeTerritories_Countries_CountryId",
                schema: "dbo",
                table: "AdministrativeTerritories",
                column: "CountryId",
                principalSchema: "dbo",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdministrativeTerritories_Countries_CountryId",
                schema: "dbo",
                table: "AdministrativeTerritories");

            migrationBuilder.DropIndex(
                name: "IX_AdministrativeTerritories_CountryId",
                schema: "dbo",
                table: "AdministrativeTerritories");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "dbo",
                table: "AdministrativeTerritories");

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                schema: "dbo",
                table: "Addresses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                schema: "dbo",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                schema: "dbo",
                table: "Addresses",
                column: "CountryId",
                principalSchema: "dbo",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
