using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LawyerService.API.Migrations
{
    public partial class addTransactions_Orders_Specializations : Migration
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

            migrationBuilder.CreateTable(
                name: "File",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FileExtension = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true),
                    FileLength = table.Column<long>(type: "bigint", nullable: false),
                    DateLoad = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRus = table.Column<string>(type: "text", nullable: true),
                    NameKaz = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameKaz = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionReasons",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRus = table.Column<string>(type: "text", nullable: true),
                    DescriptionRus = table.Column<string>(type: "text", nullable: true),
                    NameKaz = table.Column<string>(type: "text", nullable: true),
                    DescriptionKaz = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStatuses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRus = table.Column<string>(type: "text", nullable: true),
                    DescriptionRus = table.Column<string>(type: "text", nullable: true),
                    NameKaz = table.Column<string>(type: "text", nullable: true),
                    DescriptionKaz = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserBalances",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ProcentIn = table.Column<byte>(type: "smallint", nullable: false),
                    ProcentOut = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBalances_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    NameClient = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    EndDueDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LawyerId = table.Column<long>(type: "bigint", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FinishDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Procent = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalSchema: "dbo",
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryTransactions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsInService = table.Column<bool>(type: "boolean", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryTransactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryTransactions_TransactionStatuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "TransactionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryUserTransactions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionReasonId = table.Column<long>(type: "bigint", nullable: false),
                    UserBalanceId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PreviousBalanceAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentBalanceAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryUserTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryUserTransactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryUserTransactions_TransactionReasons_TrId",
                        column: x => x.TransactionReasonId,
                        principalSchema: "dbo",
                        principalTable: "TransactionReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryUserTransactions_UserBalances_UserBalanceId",
                        column: x => x.UserBalanceId,
                        principalSchema: "dbo",
                        principalTable: "UserBalances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderSpecialization",
                schema: "dbo",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    SpecializationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSpecialization", x => new { x.OrderId, x.SpecializationId });
                    table.ForeignKey(
                        name: "FK_OrderSpecialization_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSpecialization_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalSchema: "dbo",
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Lawyers_AddressId",
            //    schema: "dbo",
            //    table: "Lawyers",
            //    column: "AddressId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Lawyers_FileCopyId",
            //    schema: "dbo",
            //    table: "Lawyers",
            //    column: "FileCopyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Lawyers_UserId",
            //    schema: "dbo",
            //    table: "Lawyers",
            //    column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTransactions_StatusId",
                schema: "dbo",
                table: "HistoryTransactions",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTransactions_UserId",
                schema: "dbo",
                table: "HistoryTransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUserTransactions_OrderId",
                schema: "dbo",
                table: "HistoryUserTransactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUserTransactions_TransactionReasonId",
                schema: "dbo",
                table: "HistoryUserTransactions",
                column: "TransactionReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUserTransactions_UserBalanceId",
                schema: "dbo",
                table: "HistoryUserTransactions",
                column: "UserBalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LawyerId",
                schema: "dbo",
                table: "Orders",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                schema: "dbo",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "dbo",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSpecialization_SpecializationId",
                schema: "dbo",
                table: "OrderSpecialization",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBalances_UserId",
                schema: "dbo",
                table: "UserBalances",
                column: "UserId",
                unique: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Lawyers_Addresses_AddressId",
            //    schema: "dbo",
            //    table: "Lawyers",
            //    column: "AddressId",
            //    principalSchema: "dbo",
            //    principalTable: "Addresses",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Lawyers_AspNetUsers_UserId",
            //    schema: "dbo",
            //    table: "Lawyers",
            //    column: "UserId",
            //    principalSchema: "dbo",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawyers_File_FileCopyId",
                schema: "dbo",
                table: "Lawyers",
                column: "FileCopyId",
                principalSchema: "dbo",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "HistoryTransactions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HistoryUserTransactions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrderSpecialization",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TransactionStatuses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TransactionReasons",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserBalances",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Specializations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrderStatuses",
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
        }
    }
}
