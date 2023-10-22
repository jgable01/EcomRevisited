using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcomRevisited.Migrations
{
    /// <inheritdoc />
    public partial class initialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ConversionRate", "Name", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("2307fd24-788d-4446-92b6-3a7d78f793e1"), 1.3, "United Kingdom", 0.10000000000000001 },
                    { new Guid("686db6db-8243-4f35-9c07-030759ceb3d0"), 1.0, "Canada", 0.070000000000000007 },
                    { new Guid("6b25c11a-dbe0-459e-82c4-0be6c4409096"), 1.1000000000000001, "United States", 0.050000000000000003 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1bdf568a-3b1d-4acd-a162-10177dd8b912"), 30, "Wireless", "Headphones", 150.0 },
                    { new Guid("9c6c028f-862b-40b8-a135-9904d81db7e0"), 5, "Digital SLR", "Camera", 1200.0 },
                    { new Guid("9f417a57-ff91-4df1-abaf-58ab7f5a2cc3"), 15, "With fitness tracking", "Smartwatch", 250.0 },
                    { new Guid("b6321f82-56fe-4d08-9c75-c4fdc85ef7b8"), 10, "High performance laptop", "Laptop", 1000.0 },
                    { new Guid("fe1df346-8748-4a62-8f81-050a22a3dc64"), 20, "Latest model", "Smartphone", 800.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2307fd24-788d-4446-92b6-3a7d78f793e1"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("686db6db-8243-4f35-9c07-030759ceb3d0"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6b25c11a-dbe0-459e-82c4-0be6c4409096"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1bdf568a-3b1d-4acd-a162-10177dd8b912"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9c6c028f-862b-40b8-a135-9904d81db7e0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9f417a57-ff91-4df1-abaf-58ab7f5a2cc3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b6321f82-56fe-4d08-9c75-c4fdc85ef7b8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fe1df346-8748-4a62-8f81-050a22a3dc64"));
        }
    }
}
