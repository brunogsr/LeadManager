using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadManager.Migrations
{
    /// <inheritdoc />
    public partial class AddJobEntityAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Address", "ClientName", "DateRequested" },
                values: new object[,]
                {
                    { 1, "Yanderra 2574", "Bill Smith", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Woolooware 2230", "Craig Johnson", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "Carramar 6031", "Pete Sample", new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "Sydney 2000", "Maria Garcia", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "Quinns Rocks 6030", "Chris Sanderson", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 1,
                column: "JobId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 2,
                column: "JobId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 3,
                column: "JobId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 4,
                column: "JobId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 5,
                column: "JobId",
                value: 5);

            migrationBuilder.CreateIndex(
                name: "IX_Leads_JobId",
                table: "Leads",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Jobs_JobId",
                table: "Leads",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Jobs_JobId",
                table: "Leads");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Leads_JobId",
                table: "Leads");

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 1,
                column: "JobId",
                value: 5577421);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 2,
                column: "JobId",
                value: 5588872);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 3,
                column: "JobId",
                value: 5141895);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 4,
                column: "JobId",
                value: 5121931);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 5,
                column: "JobId",
                value: 5121931);
        }
    }
}
