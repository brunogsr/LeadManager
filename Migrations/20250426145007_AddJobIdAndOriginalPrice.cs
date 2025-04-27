using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadManager.Migrations
{
    /// <inheritdoc />
    public partial class AddJobIdAndOriginalPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Leads",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalPrice",
                table: "Leads",
                type: "decimal(18, 2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "JobId", "OriginalPrice" },
                values: new object[] { 5577421, 62.00m });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "JobId", "OriginalPrice" },
                values: new object[] { 5588872, 49.00m });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "JobId", "OriginalPrice" },
                values: new object[] { 5141895, 526.00m });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "JobId", "OriginalPrice" },
                values: new object[] { 5121931, 850.00m });

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "Category", "DateCreated", "Description", "Email", "FirstName", "JobId", "LastName", "OriginalPrice", "Phone", "Price", "Status", "Suburb" },
                values: new object[] { 5, "Home Renovations", new DateTime(2023, 8, 30, 11, 14, 0, 0, DateTimeKind.Utc), "Two story building conversion", "another.fake@hipmail.com", "Chris", 5121931, "Sanderson", 1532.00m, "04987654321", 1532.00m, 1, "Quinns Rocks 6030" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "Leads");
        }
    }
}
