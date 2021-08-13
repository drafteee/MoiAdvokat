using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyerService.API.Migrations
{
    public partial class addAddressUserBalanceInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FunctionUser_Function_FunctionsId",
                schema: "dbo",
                table: "FunctionUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBalances_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserBalances");

            migrationBuilder.DropIndex(
                name: "IX_UserBalances_UserId",
                schema: "dbo",
                table: "UserBalances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Function",
                schema: "dbo",
                table: "Function");

            migrationBuilder.RenameTable(
                name: "Function",
                schema: "dbo",
                newName: "Functions",
                newSchema: "dbo");

            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                schema: "dbo",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BalanceId",
                schema: "dbo",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Functions",
                schema: "dbo",
                table: "Functions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                schema: "dbo",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BalanceId",
                schema: "dbo",
                table: "AspNetUsers",
                column: "BalanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                schema: "dbo",
                table: "AspNetUsers",
                column: "AddressId",
                principalSchema: "dbo",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserBalances_BalanceId",
                schema: "dbo",
                table: "AspNetUsers",
                column: "BalanceId",
                principalSchema: "dbo",
                principalTable: "UserBalances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionUser_Functions_FunctionsId",
                schema: "dbo",
                table: "FunctionUser",
                column: "FunctionsId",
                principalSchema: "dbo",
                principalTable: "Functions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserBalances_BalanceId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FunctionUser_Functions_FunctionsId",
                schema: "dbo",
                table: "FunctionUser");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BalanceId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Functions",
                schema: "dbo",
                table: "Functions");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BalanceId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Functions",
                schema: "dbo",
                newName: "Function",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Function",
                schema: "dbo",
                table: "Function",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserBalances_UserId",
                schema: "dbo",
                table: "UserBalances",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionUser_Function_FunctionsId",
                schema: "dbo",
                table: "FunctionUser",
                column: "FunctionsId",
                principalSchema: "dbo",
                principalTable: "Function",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBalances_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserBalances",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
