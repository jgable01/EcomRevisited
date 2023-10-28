using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcomRevisited.Migrations
{
    /// <inheritdoc />
    public partial class addeditemnumbertoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("10ee2d15-6874-4f03-9cb8-c7d2c7fa7d28"));

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("815a93dd-6cf6-48a1-a77b-a59a73550320"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("56945e0a-9e23-4f17-ab18-a4258595dc82"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("5db1e40c-4d46-4a49-9812-51ac152f9b49"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7c9b37ca-a53a-4436-a970-5d8dc8a03ab7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("527d18ab-692b-4ff7-9992-899e388bb9ec"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("670f85a3-c926-48a9-83a6-9acb5301f566"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("86229d29-f6b3-4662-b79a-92efde4cb9db"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bd4d69f3-dfc3-484d-ac28-afde337f405c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f3517d3b-824d-4e21-844b-cebe8e6c9033"));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfItems",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Carts",
                column: "Id",
                values: new object[]
                {
                    new Guid("53ad08b2-357d-4790-ab7f-369e74f565ba"),
                    new Guid("fb10968d-ea5d-48f9-b90b-e88604149cb3")
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ConversionRate", "Name", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("9f577222-d7a3-4fdf-b68e-70707930bdee"), 1.3, "United Kingdom", 0.10000000000000001 },
                    { new Guid("c067ea9c-1b36-41a8-9d95-9ddb58b95a86"), 1.0, "Canada", 0.070000000000000007 },
                    { new Guid("cd085ef6-c04e-4516-9016-8b76f8b8bc40"), 1.1000000000000001, "United States", 0.050000000000000003 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("345e220e-60c0-45de-b65d-d1b1d61d31d8"), 10, "High performance laptop", "https://www.lifeofpix.com/wp-content/uploads/2018/05/p-244-ae-mint-005-1600x1169.jpg", "Laptop", 1000.0 },
                    { new Guid("448a36f8-906a-470e-99d4-d1832a9b8741"), 15, "With fitness tracking", "https://images.pexels.com/photos/393047/pexels-photo-393047.jpeg?cs=srgb&dl=pexels-alexandr-borecky-393047.jpg&fm=jpg", "Smartwatch", 250.0 },
                    { new Guid("a63eed52-2429-4221-8a2e-d37208df1e63"), 5, "Digital SLR", "https://images.pexels.com/photos/274973/pexels-photo-274973.jpeg?cs=srgb&dl=pexels-pixabay-274973.jpg&fm=jpg", "Camera", 1200.0 },
                    { new Guid("b5ec9bea-29d0-43bd-9c2d-c50935b4e003"), 30, "Wireless", "https://images.pexels.com/photos/3945667/pexels-photo-3945667.jpeg?cs=srgb&dl=pexels-cottonbro-studio-3945667.jpg&fm=jpg", "Headphones", 150.0 },
                    { new Guid("d06677ba-2450-497c-9a50-cd12e100f530"), 20, "Latest model", "https://media.istockphoto.com/id/1377877660/vector/realistic-mobile-phone-mockup-cellphone-app-template-isolated-stock-vector.jpg?s=612x612&w=0&k=20&c=Xw2padf6w33h9eQFFz83PL0reGEMdu1FtFsuI5G5Nf0=", "Smartphone", 800.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("53ad08b2-357d-4790-ab7f-369e74f565ba"));

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("fb10968d-ea5d-48f9-b90b-e88604149cb3"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9f577222-d7a3-4fdf-b68e-70707930bdee"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c067ea9c-1b36-41a8-9d95-9ddb58b95a86"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("cd085ef6-c04e-4516-9016-8b76f8b8bc40"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("345e220e-60c0-45de-b65d-d1b1d61d31d8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("448a36f8-906a-470e-99d4-d1832a9b8741"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a63eed52-2429-4221-8a2e-d37208df1e63"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b5ec9bea-29d0-43bd-9c2d-c50935b4e003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d06677ba-2450-497c-9a50-cd12e100f530"));

            migrationBuilder.DropColumn(
                name: "NumberOfItems",
                table: "Orders");

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
        }
    }
}
