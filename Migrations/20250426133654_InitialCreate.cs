using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Suburb = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "Category", "DateCreated", "Description", "Email", "FirstName", "LastName", "Phone", "Price", "Suburb" },
                values: new object[,]
                {
                    { 1, "Painters", new DateTime(2024, 1, 4, 14, 37, 0, 0, DateTimeKind.Utc), "Need to paint 2 aluminum windows and a sliding glass door", "bill@example.com", "Bill", "Smith", "0412345678", 62.00m, "Yanderra 2574" },
                    { 2, "Interior Painters", new DateTime(2024, 1, 4, 13, 15, 0, 0, DateTimeKind.Utc), "Internal walls 3 colours", "craig@sample.net", "Craig", "Johnson", "0498765432", 49.00m, "Woolooware 2230" },
                    { 3, "General Building Work", new DateTime(2024, 9, 5, 10, 36, 0, 0, DateTimeKind.Utc), "Plaster exposed brick walls", "pete@mailinator.com", "Pete", "Sample", "0412345678", 526.00m, "Carramar 6031" },
                    { 4, "Electrical", new DateTime(2024, 2, 15, 9, 20, 0, 0, DateTimeKind.Utc), "Install new lighting fixtures", "maria@example.com", "Maria", "Garcia", "0422334455", 850.00m, "Sydney 2000" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leads");
        }
    }
}
