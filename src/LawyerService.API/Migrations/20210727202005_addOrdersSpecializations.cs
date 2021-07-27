using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyerService.API.Migrations
{
    public partial class addOrdersSpecializations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderSpecialization_Orders_OrderId",
                schema: "dbo",
                table: "OrderSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderSpecialization_Specializations_SpecializationId",
                schema: "dbo",
                table: "OrderSpecialization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSpecialization",
                schema: "dbo",
                table: "OrderSpecialization");

            migrationBuilder.RenameTable(
                name: "OrderSpecialization",
                schema: "dbo",
                newName: "OrderSpecializations",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_OrderSpecialization_SpecializationId",
                schema: "dbo",
                table: "OrderSpecializations",
                newName: "IX_OrderSpecializations_SpecializationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSpecializations",
                schema: "dbo",
                table: "OrderSpecializations",
                columns: new[] { "OrderId", "SpecializationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSpecializations_Orders_OrderId",
                schema: "dbo",
                table: "OrderSpecializations",
                column: "OrderId",
                principalSchema: "dbo",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSpecializations_Specializations_SpecializationId",
                schema: "dbo",
                table: "OrderSpecializations",
                column: "SpecializationId",
                principalSchema: "dbo",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderSpecializations_Orders_OrderId",
                schema: "dbo",
                table: "OrderSpecializations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderSpecializations_Specializations_SpecializationId",
                schema: "dbo",
                table: "OrderSpecializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSpecializations",
                schema: "dbo",
                table: "OrderSpecializations");

            migrationBuilder.RenameTable(
                name: "OrderSpecializations",
                schema: "dbo",
                newName: "OrderSpecialization",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_OrderSpecializations_SpecializationId",
                schema: "dbo",
                table: "OrderSpecialization",
                newName: "IX_OrderSpecialization_SpecializationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSpecialization",
                schema: "dbo",
                table: "OrderSpecialization",
                columns: new[] { "OrderId", "SpecializationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSpecialization_Orders_OrderId",
                schema: "dbo",
                table: "OrderSpecialization",
                column: "OrderId",
                principalSchema: "dbo",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSpecialization_Specializations_SpecializationId",
                schema: "dbo",
                table: "OrderSpecialization",
                column: "SpecializationId",
                principalSchema: "dbo",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
