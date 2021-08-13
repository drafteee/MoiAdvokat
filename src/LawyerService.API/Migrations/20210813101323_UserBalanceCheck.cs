using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyerService.API.Migrations
{
    public partial class UserBalanceCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "UserBalances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "UserBalances",
                type: "text",
                nullable: true);
        }
    }
}
