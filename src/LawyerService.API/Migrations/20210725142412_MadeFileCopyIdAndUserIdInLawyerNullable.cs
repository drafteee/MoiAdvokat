using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LawyerService.API.Migrations
{
    public partial class MadeFileCopyIdAndUserIdInLawyerNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Lawyers",
                type: "text",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "FileCopyId",
                schema: "dbo",
                table: "Lawyers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            //migrationBuilder.CreateTable(
            //    name: "File",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        FileName = table.Column<string>(type: "text", nullable: true),
            //        FileExtension = table.Column<string>(type: "text", nullable: true),
            //        Content = table.Column<byte[]>(type: "bytea", nullable: true),
            //        FileLength = table.Column<long>(type: "bigint", nullable: false),
            //        DateLoad = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_File", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IP = table.Column<string>(type: "text", nullable: true),
                    UserAgent = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateTable(
            //    name: "UserBalances",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        UserId = table.Column<string>(type: "text", nullable: true),
            //        Amount = table.Column<decimal>(type: "numeric", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserBalances", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserBalances_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalSchema: "dbo",
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Lawyers_AddressId",
                schema: "dbo",
                table: "Lawyers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Lawyers_FileCopyId",
                schema: "dbo",
                table: "Lawyers",
                column: "FileCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lawyers_UserId",
                schema: "dbo",
                table: "Lawyers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "dbo",
                table: "RefreshTokens",
                column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserBalances_UserId",
            //    schema: "dbo",
            //    table: "UserBalances",
            //    column: "UserId",
            //    unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawyers_Addresses_AddressId",
                schema: "dbo",
                table: "Lawyers",
                column: "AddressId",
                principalSchema: "dbo",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawyers_AspNetUsers_UserId",
                schema: "dbo",
                table: "Lawyers",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Lawyers_File_FileCopyId",
            //    schema: "dbo",
            //    table: "Lawyers",
            //    column: "FileCopyId",
            //    principalSchema: "dbo",
            //    principalTable: "File",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lawyers_Addresses_AddressId",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawyers_AspNetUsers_UserId",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawyers_File_FileCopyId",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropTable(
                name: "File",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserBalances",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Lawyers_AddressId",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropIndex(
                name: "IX_Lawyers_FileCopyId",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.DropIndex(
                name: "IX_Lawyers_UserId",
                schema: "dbo",
                table: "Lawyers");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                schema: "dbo",
                table: "Lawyers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
        }
    }
}
