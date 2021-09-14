using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyerService.API.Migrations
{
    public partial class UniquePairOrderResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderRespenses_OrderId_LawyerId",
                schema: "dbo",
                table: "OrderRespenses",
                columns: new[] { "OrderId", "LawyerId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderRespenses_OrderId_LawyerId",
                schema: "dbo",
                table: "OrderRespenses");
        }
    }
}
