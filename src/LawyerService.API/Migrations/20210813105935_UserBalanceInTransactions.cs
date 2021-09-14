using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyerService.API.Migrations
{
    public partial class UserBalanceInTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryUserTransactions_UserBalances_UserBalanceId",
                schema: "dbo",
                table: "HistoryUserTransactions");

            migrationBuilder.DropIndex(
                name: "IX_HistoryUserTransactions_UserBalanceId",
                schema: "dbo",
                table: "HistoryUserTransactions");

            migrationBuilder.DropColumn(
                name: "UserBalanceId",
                schema: "dbo",
                table: "HistoryUserTransactions");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "HistoryUserTransactions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUserTransactions_UserId",
                schema: "dbo",
                table: "HistoryUserTransactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryUserTransactions_AspNetUsers_UserId",
                schema: "dbo",
                table: "HistoryUserTransactions",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryUserTransactions_AspNetUsers_UserId",
                schema: "dbo",
                table: "HistoryUserTransactions");

            migrationBuilder.DropIndex(
                name: "IX_HistoryUserTransactions_UserId",
                schema: "dbo",
                table: "HistoryUserTransactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "HistoryUserTransactions");

            migrationBuilder.AddColumn<long>(
                name: "UserBalanceId",
                schema: "dbo",
                table: "HistoryUserTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUserTransactions_UserBalanceId",
                schema: "dbo",
                table: "HistoryUserTransactions",
                column: "UserBalanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryUserTransactions_UserBalances_UserBalanceId",
                schema: "dbo",
                table: "HistoryUserTransactions",
                column: "UserBalanceId",
                principalSchema: "dbo",
                principalTable: "UserBalances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
