using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyerService.API.Migrations
{
    public partial class ExtendedBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lawyers_File_FileCopyId",
                schema: "dbo",
                table: "Lawyers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_RefreshTokens_AspNetUsers_UserId1",
            //    schema: "dbo",
            //    table: "RefreshTokens");

            //migrationBuilder.DropIndex(
            //    name: "IX_RefreshTokens_UserId1",
            //    schema: "dbo",
            //    table: "RefreshTokens");

            //migrationBuilder.DropColumn(
            //    name: "UserId1",
            //    schema: "dbo",
            //    table: "RefreshTokens");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "UserBalances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "UserBalances",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "UserBalances",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "TransactionStatuses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "TransactionStatuses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "TransactionStatuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "TransactionReasons",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "TransactionReasons",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "TransactionReasons",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "Specializations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "Specializations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "Specializations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "RefreshTokens",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "RefreshTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "OrderStatuses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "OrderStatuses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "OrderStatuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "FileCopyId",
                schema: "dbo",
                table: "Lawyers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "Lawyers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "Lawyers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "Lawyers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "HistoryUserTransactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "HistoryUserTransactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "HistoryUserTransactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "HistoryTransactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "HistoryTransactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "HistoryTransactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "File",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "File",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "File",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "Countries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "Countries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "Countries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "AdministrativeTerritoryTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "AdministrativeTerritoryTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "AdministrativeTerritoryTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "AdministrativeTerritories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "AdministrativeTerritories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "AdministrativeTerritories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "Addresses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.CreateIndex(
            //    name: "IX_RefreshTokens_UserId",
            //    schema: "dbo",
            //    table: "RefreshTokens",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Lawyers_LicenseNumber",
            //    schema: "dbo",
            //    table: "Lawyers",
            //    column: "LicenseNumber",
            //    unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawyers_File_FileCopyId",
                schema: "dbo",
                table: "Lawyers",
                column: "FileCopyId",
                principalSchema: "dbo",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_RefreshTokens_AspNetUsers_UserId",
            //    schema: "dbo",
            //    table: "RefreshTokens",
            //    column: "UserId",
            //    principalSchema: "dbo",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lawyers_File_FileCopyId",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                schema: "dbo",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "dbo",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Lawyers_LicenseNumber",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "UserBalances");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "UserBalances");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "UserBalances");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "TransactionStatuses");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "TransactionStatuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "TransactionStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "TransactionReasons");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "TransactionReasons");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "TransactionReasons");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "HistoryUserTransactions");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "HistoryUserTransactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "HistoryUserTransactions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "HistoryTransactions");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "HistoryTransactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "HistoryTransactions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "File");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "File");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "File");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "AdministrativeTerritoryTypes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "AdministrativeTerritoryTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "AdministrativeTerritoryTypes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "AdministrativeTerritories");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "AdministrativeTerritories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "AdministrativeTerritories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "Addresses");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "dbo",
                table: "RefreshTokens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                schema: "dbo",
                table: "RefreshTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FileCopyId",
                schema: "dbo",
                table: "Lawyers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId1",
                schema: "dbo",
                table: "RefreshTokens",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Lawyers_File_FileCopyId",
                schema: "dbo",
                table: "Lawyers",
                column: "FileCopyId",
                principalSchema: "dbo",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId1",
                schema: "dbo",
                table: "RefreshTokens",
                column: "UserId1",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
