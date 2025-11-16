using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SFMB.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataAfterDateOnlyApply1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 1, 40000m, new DateOnly(2025, 5, 1), "Monthly job income", 1 },
                    { 2, 220.50m, new DateOnly(2025, 5, 1), "food", 3 },
                    { 3, 554.70m, new DateOnly(2025, 5, 2), "electricity, water", 4 },
                    { 4, 358m, new DateOnly(2025, 5, 2), "netflix", 5 },
                    { 5, 16350m, new DateOnly(2025, 5, 2), "apartment rent", 7 },
                    { 6, 1020.80m, new DateOnly(2025, 5, 3), "market", 3 },
                    { 7, 4000m, new DateOnly(2025, 5, 3), "consulting", 2 },
                    { 8, 820m, new DateOnly(2025, 5, 4), "", 5 },
                    { 9, 325m, new DateOnly(2025, 5, 5), "pizza", 3 },
                    { 10, 120m, new DateOnly(2025, 5, 5), "deposit percentage", 6 },
                    { 11, 780m, new DateOnly(2025, 5, 6), "food delivery", 3 },
                    { 12, 535m, new DateOnly(2025, 5, 6), "lunch", 3 },
                    { 13, 370m, new DateOnly(2025, 5, 8), "market", 3 },
                    { 14, 400m, new DateOnly(2025, 5, 8), "bike rent", 5 },
                    { 15, 520m, new DateOnly(2025, 5, 9), "street food", 3 },
                    { 16, 250.72m, new DateOnly(2025, 5, 10), "gas", 4 },
                    { 17, 10m, new DateOnly(2025, 5, 10), "matches", 3 },
                    { 18, 100m, new DateOnly(2025, 5, 10), "market", 3 },
                    { 19, 155m, new DateOnly(2025, 5, 10), "youtube subscription", 5 },
                    { 20, 2500m, new DateOnly(2025, 5, 10), "bonus", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "OperationTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "OperationTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "OperationTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "OperationTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "OperationTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "OperationTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "OperationTypeId",
                keyValue: 7);
        }
    }
}
