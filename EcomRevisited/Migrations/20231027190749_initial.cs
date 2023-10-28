using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcomRevisited.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversionRate = table.Column<double>(type: "float", nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carts",
                column: "Id",
                values: new object[]
                {
                    new Guid("10ee2d15-6874-4f03-9cb8-c7d2c7fa7d28"),
                    new Guid("815a93dd-6cf6-48a1-a77b-a59a73550320")
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ConversionRate", "Name", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("56945e0a-9e23-4f17-ab18-a4258595dc82"), 1.0, "Canada", 0.070000000000000007 },
                    { new Guid("5db1e40c-4d46-4a49-9812-51ac152f9b49"), 1.3, "United Kingdom", 0.10000000000000001 },
                    { new Guid("7c9b37ca-a53a-4436-a970-5d8dc8a03ab7"), 1.1000000000000001, "United States", 0.050000000000000003 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("527d18ab-692b-4ff7-9992-899e388bb9ec"), 5, "Digital SLR", "https://images.pexels.com/photos/274973/pexels-photo-274973.jpeg?cs=srgb&dl=pexels-pixabay-274973.jpg&fm=jpg", "Camera", 1200.0 },
                    { new Guid("670f85a3-c926-48a9-83a6-9acb5301f566"), 20, "Latest model", "https://media.istockphoto.com/id/1377877660/vector/realistic-mobile-phone-mockup-cellphone-app-template-isolated-stock-vector.jpg?s=612x612&w=0&k=20&c=Xw2padf6w33h9eQFFz83PL0reGEMdu1FtFsuI5G5Nf0=", "Smartphone", 800.0 },
                    { new Guid("86229d29-f6b3-4662-b79a-92efde4cb9db"), 30, "Wireless", "https://images.pexels.com/photos/3945667/pexels-photo-3945667.jpeg?cs=srgb&dl=pexels-cottonbro-studio-3945667.jpg&fm=jpg", "Headphones", 150.0 },
                    { new Guid("bd4d69f3-dfc3-484d-ac28-afde337f405c"), 10, "High performance laptop", "https://www.lifeofpix.com/wp-content/uploads/2018/05/p-244-ae-mint-005-1600x1169.jpg", "Laptop", 1000.0 },
                    { new Guid("f3517d3b-824d-4e21-844b-cebe8e6c9033"), 15, "With fitness tracking", "https://images.pexels.com/photos/393047/pexels-photo-393047.jpeg?cs=srgb&dl=pexels-alexandr-borecky-393047.jpg&fm=jpg", "Smartwatch", 250.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
