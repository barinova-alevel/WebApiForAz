using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SFMB.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgreSqlMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationTypes",
                columns: table => new
                {
                    OperationTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsIncome = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypes", x => x.OperationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    OperationTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.OperationId);
                    table.ForeignKey(
                        name: "FK_Operations_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "OperationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "OperationTypeId", "Description", "IsIncome", "Name" },
                values: new object[,]
                {
                    { 1, "Monthly job income", true, "Salary" },
                    { 2, "Freelance or side project payments", true, "Freelance" },
                    { 3, "Daily food and household shopping", false, "Groceries" },
                    { 4, "Electricity, water, gas, etc.", false, "Utilities" },
                    { 5, "Movies, games, subscriptions", false, "Entertainment" },
                    { 6, "Dividends or capital gains", true, "Investment Return" },
                    { 7, "Monthly house or apartment rent", false, "Rent" }
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "OperationId", "Amount", "Date", "Note", "OperationTypeId" },
                values: new object[,]
                {
                    { 1, 40000m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Monthly job income", 1 },
                    { 2, 220.50m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "food", 3 },
                    { 3, 554.70m, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), "electricity, water", 4 },
                    { 4, 358m, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), "netflix", 5 },
                    { 5, 16350m, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), "apartment rent", 7 },
                    { 6, 1020.80m, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), "market", 3 },
                    { 7, 4000m, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), "consulting", 2 },
                    { 8, 820m, new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), "", 5 },
                    { 9, 325m, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), "pizza", 3 },
                    { 10, 120m, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), "deposit percentage", 6 },
                    { 11, 780m, new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), "food delivery", 3 },
                    { 12, 535m, new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), "lunch", 3 },
                    { 13, 370m, new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "market", 3 },
                    { 14, 400m, new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "bike rent", 5 },
                    { 15, 520m, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "street food", 3 },
                    { 16, 250.72m, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "gas", 4 },
                    { 17, 10m, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "matches", 3 },
                    { 18, 100m, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "market", 3 },
                    { 19, 155m, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "youtube subscription", 5 },
                    { 20, 2500m, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bonus", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperationTypeId",
                table: "Operations",
                column: "OperationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "OperationTypes");
        }
    }
}
