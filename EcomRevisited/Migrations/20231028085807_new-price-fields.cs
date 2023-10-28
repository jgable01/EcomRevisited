using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcomRevisited.Migrations
{
    /// <inheritdoc />
    public partial class newpricefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("1f6fe05c-ad70-4993-8108-5fff52aff96d"));

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("8d7dab5e-0bb4-42e2-b2d2-d99882e6405f"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("01c5ca0b-273e-452b-a76b-d0c3d92978f8"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("237cdf59-8e6d-4fe9-ba8d-608243362a44"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7cf68b5c-8ce2-467f-8c6e-0df72c037753"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1644824e-e3bc-4892-8041-00ea6cdb77d3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5d0f679f-1be1-4865-9b58-2dacdb4268d2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7c9e176f-496c-4151-a1d3-27aeeabbaccd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cffd22ff-b06f-43e8-8ccf-e4cbb3b23783"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f86511b0-5408-40a3-9b62-97764cdb31f4"));

            migrationBuilder.AddColumn<double>(
                name: "ConvertedPrice",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FinalPrice",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Carts",
                column: "Id",
                values: new object[]
                {
                    new Guid("94306a99-bcd0-4c34-8185-fae513e0457f"),
                    new Guid("c8840611-9ff4-4c16-9daf-631e4958680f")
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ConversionRate", "Name", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("12c7f898-f70b-46c8-8e88-d69ba6f7568a"), 1.3, "United Kingdom", 0.10000000000000001 },
                    { new Guid("7a3afe90-dc82-4e2d-bc23-12f9302b0d92"), 1.0, "Canada", 0.070000000000000007 },
                    { new Guid("9bfb4a05-3738-421c-a278-3ff94f3fb461"), 1.1000000000000001, "United States", 0.050000000000000003 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("12a1ee3a-0e08-4c42-8a8f-d24e7d78f743"), 15, "With fitness tracking", "https://images.pexels.com/photos/393047/pexels-photo-393047.jpeg?cs=srgb&dl=pexels-alexandr-borecky-393047.jpg&fm=jpg", "Smartwatch", 250.0 },
                    { new Guid("5c8f99f3-bc5b-4459-b897-0bbf93269ae0"), 10, "High performance laptop", "https://www.lifeofpix.com/wp-content/uploads/2018/05/p-244-ae-mint-005-1600x1169.jpg", "Laptop", 1000.0 },
                    { new Guid("a7b733e5-c8f4-4326-8fce-a692bd72414a"), 20, "Latest model", "https://media.istockphoto.com/id/1377877660/vector/realistic-mobile-phone-mockup-cellphone-app-template-isolated-stock-vector.jpg?s=612x612&w=0&k=20&c=Xw2padf6w33h9eQFFz83PL0reGEMdu1FtFsuI5G5Nf0=", "Smartphone", 800.0 },
                    { new Guid("d531e956-1460-4601-94ae-dd2d6a2f858a"), 30, "Wireless", "https://images.pexels.com/photos/3945667/pexels-photo-3945667.jpeg?cs=srgb&dl=pexels-cottonbro-studio-3945667.jpg&fm=jpg", "Headphones", 150.0 },
                    { new Guid("e538ce0e-273b-49b0-826d-431004edc63d"), 5, "Digital SLR", "https://images.pexels.com/photos/274973/pexels-photo-274973.jpeg?cs=srgb&dl=pexels-pixabay-274973.jpg&fm=jpg", "Camera", 1200.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("94306a99-bcd0-4c34-8185-fae513e0457f"));

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("c8840611-9ff4-4c16-9daf-631e4958680f"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("12c7f898-f70b-46c8-8e88-d69ba6f7568a"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7a3afe90-dc82-4e2d-bc23-12f9302b0d92"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9bfb4a05-3738-421c-a278-3ff94f3fb461"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("12a1ee3a-0e08-4c42-8a8f-d24e7d78f743"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5c8f99f3-bc5b-4459-b897-0bbf93269ae0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a7b733e5-c8f4-4326-8fce-a692bd72414a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d531e956-1460-4601-94ae-dd2d6a2f858a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e538ce0e-273b-49b0-826d-431004edc63d"));

            migrationBuilder.DropColumn(
                name: "ConvertedPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Carts",
                column: "Id",
                values: new object[]
                {
                    new Guid("1f6fe05c-ad70-4993-8108-5fff52aff96d"),
                    new Guid("8d7dab5e-0bb4-42e2-b2d2-d99882e6405f")
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ConversionRate", "Name", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("01c5ca0b-273e-452b-a76b-d0c3d92978f8"), 1.1000000000000001, "United States", 0.050000000000000003 },
                    { new Guid("237cdf59-8e6d-4fe9-ba8d-608243362a44"), 1.0, "Canada", 0.070000000000000007 },
                    { new Guid("7cf68b5c-8ce2-467f-8c6e-0df72c037753"), 1.3, "United Kingdom", 0.10000000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1644824e-e3bc-4892-8041-00ea6cdb77d3"), 20, "Latest model", "https://media.istockphoto.com/id/1377877660/vector/realistic-mobile-phone-mockup-cellphone-app-template-isolated-stock-vector.jpg?s=612x612&w=0&k=20&c=Xw2padf6w33h9eQFFz83PL0reGEMdu1FtFsuI5G5Nf0=", "Smartphone", 800.0 },
                    { new Guid("5d0f679f-1be1-4865-9b58-2dacdb4268d2"), 30, "Wireless", "https://images.pexels.com/photos/3945667/pexels-photo-3945667.jpeg?cs=srgb&dl=pexels-cottonbro-studio-3945667.jpg&fm=jpg", "Headphones", 150.0 },
                    { new Guid("7c9e176f-496c-4151-a1d3-27aeeabbaccd"), 10, "High performance laptop", "https://www.lifeofpix.com/wp-content/uploads/2018/05/p-244-ae-mint-005-1600x1169.jpg", "Laptop", 1000.0 },
                    { new Guid("cffd22ff-b06f-43e8-8ccf-e4cbb3b23783"), 5, "Digital SLR", "https://images.pexels.com/photos/274973/pexels-photo-274973.jpeg?cs=srgb&dl=pexels-pixabay-274973.jpg&fm=jpg", "Camera", 1200.0 },
                    { new Guid("f86511b0-5408-40a3-9b62-97764cdb31f4"), 15, "With fitness tracking", "https://images.pexels.com/photos/393047/pexels-photo-393047.jpeg?cs=srgb&dl=pexels-alexandr-borecky-393047.jpg&fm=jpg", "Smartwatch", 250.0 }
                });
        }
    }
}
