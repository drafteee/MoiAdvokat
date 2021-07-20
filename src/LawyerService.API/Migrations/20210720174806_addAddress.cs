using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LawyerService.API.Migrations
{
    public partial class addAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdministrativeTerritoryTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRus = table.Column<string>(type: "text", nullable: true),
                    NameRusShort = table.Column<string>(type: "text", nullable: true),
                    NameKaz = table.Column<string>(type: "text", nullable: true),
                    NameShortKaz = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeTerritoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRus = table.Column<string>(type: "text", nullable: true),
                    NameRusShort = table.Column<string>(type: "text", nullable: true),
                    NameKaz = table.Column<string>(type: "text", nullable: true),
                    NameShortKaz = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdministrativeTerritories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    AdministrativeTerritoryTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeTerritories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministrativeTerritories_AdministrativeTerritoryTypes_Admi~",
                        column: x => x.AdministrativeTerritoryTypeId,
                        principalSchema: "dbo",
                        principalTable: "AdministrativeTerritoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Postcode = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    AdministrativeTerritoryId = table.Column<long>(type: "bigint", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: true),
                    House = table.Column<string>(type: "text", nullable: true),
                    Office = table.Column<string>(type: "text", nullable: true),
                    Notice = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AdministrativeTerritories_AdministrativeTerritory~",
                        column: x => x.AdministrativeTerritoryId,
                        principalSchema: "dbo",
                        principalTable: "AdministrativeTerritories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AdministrativeTerritoryId",
                schema: "dbo",
                table: "Addresses",
                column: "AdministrativeTerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                schema: "dbo",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeTerritories_AdministrativeTerritoryTypeId",
                schema: "dbo",
                table: "AdministrativeTerritories",
                column: "AdministrativeTerritoryTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AdministrativeTerritories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AdministrativeTerritoryTypes",
                schema: "dbo");
        }
    }
}
