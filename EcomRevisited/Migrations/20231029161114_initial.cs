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
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    NumberOfItems = table.Column<int>(type: "int", nullable: false),
                    ConvertedPrice = table.Column<double>(type: "float", nullable: false),
                    FinalPrice = table.Column<double>(type: "float", nullable: false)
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
                    new Guid("016765b0-4079-48a5-a1e7-174c1019a9ed"),
                    new Guid("83e89c35-5d13-4f66-b742-6ccc20e7f4d9")
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ConversionRate", "Name", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("24857574-b64f-4bcb-96d1-715f535ea3bc"), 1.0, "Canada", 0.070000000000000007 },
                    { new Guid("85d1f152-94e7-483f-98c8-687dd799d038"), 1.1000000000000001, "United States", 0.050000000000000003 },
                    { new Guid("ae89423d-20a6-420a-8d5e-a841821f6a9d"), 1.3, "United Kingdom", 0.10000000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("042f5428-b44a-47b3-b6f8-abd8f7b0cea1"), 20, "Latest model", "https://media.istockphoto.com/id/1377877660/vector/realistic-mobile-phone-mockup-cellphone-app-template-isolated-stock-vector.jpg?s=612x612&w=0&k=20&c=Xw2padf6w33h9eQFFz83PL0reGEMdu1FtFsuI5G5Nf0=", "Smartphone", 800.0 },
                    { new Guid("328a220d-834e-4862-9608-120ceec44c26"), 10, "High performance laptop", "https://www.lifeofpix.com/wp-content/uploads/2018/05/p-244-ae-mint-005-1600x1169.jpg", "Laptop", 1000.0 },
                    { new Guid("9699e61b-0da0-4fac-bf34-90b521d0d6fa"), 15, "With fitness tracking", "https://images.pexels.com/photos/393047/pexels-photo-393047.jpeg?cs=srgb&dl=pexels-alexandr-borecky-393047.jpg&fm=jpg", "Smartwatch", 250.0 },
                    { new Guid("99cc5249-920f-401b-aacb-aa4836a375a1"), 5, "Digital SLR", "https://images.pexels.com/photos/274973/pexels-photo-274973.jpeg?cs=srgb&dl=pexels-pixabay-274973.jpg&fm=jpg", "Camera", 1200.0 },
                    { new Guid("f900f369-4dc0-43dd-b5e8-f718cb8116ab"), 30, "Wireless", "https://images.pexels.com/photos/3945667/pexels-photo-3945667.jpeg?cs=srgb&dl=pexels-cottonbro-studio-3945667.jpg&fm=jpg", "Headphones", 150.0 }
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
