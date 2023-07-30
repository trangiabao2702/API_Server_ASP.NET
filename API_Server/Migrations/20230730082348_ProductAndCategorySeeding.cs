using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Server.Migrations
{
    /// <inheritdoc />
    public partial class ProductAndCategorySeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "Categories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "ModifiedAt", "Name" },
                values: new object[] { 1, new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8485), null, "Shoes's Decription", new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8501), "Shoes" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "DeletedAt", "Description", "Images", "ModifiedAt", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8730), null, "The iconic Nike Air Zoom Pegasus 36 offers more cooling and mesh that targets breathability across high-heat areas. A slimmer heel collar and tongue reduce bulk, while exposed cables give you a snug fit at higher speeds.", "['https://s3-us-west-2.amazonaws.com/s.cdpn.io/1315882/air-zoom-pegasus-36-mens-running-shoe-wide-D24Mcz-removebg-preview.png']", new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8731), "Nike Air Zoom Pegasus 36", 108.97m },
                    { 2, 1, new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8733), null, "The Nike Air Zoom Pegasus 36 Shield gets updated to conquer wet routes. A water-repellent upper combines with an outsole that helps create grip on wet surfaces, letting you run in confidence despite the weather.", "['https://s3-us-west-2.amazonaws.com/s.cdpn.io/1315882/air-zoom-pegasus-36-shield-mens-running-shoe-24FBGb__1_-removebg-preview.png']", new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8733), "Nike Air Zoom Pegasus 36 Shield", 89.97m },
                    { 3, 1, new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8735), null, "Designed for steady, easy-paced movement, the Nike CruzrOne keeps you going. Its rocker-shaped sole and plush, lightweight cushioning let you move naturally and comfortably. The padded collar is lined with soft wool, adding luxury to every step, while mesh details let your foot breathe. There’s no finish line—there’s only you, one step after the next.", "['https://s3-us-west-2.amazonaws.com/s.cdpn.io/1315882/cruzrone-unisex-shoe-T2rRwS-removebg-preview.png']", new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8735), "Nike CruzrOne", 100.97m },
                    { 4, 1, new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8737), null, "The Nike Epic React Flyknit 2 takes a step up from its predecessor with smooth, lightweight performance and a bold look. An updated Flyknit upper conforms to your foot with a minimal, supportive design. Underfoot, durable Nike React technology defies the odds by being both soft and responsive, for comfort that lasts as long as you can run.", "['https://s3-us-west-2.amazonaws.com/s.cdpn.io/1315882/epic-react-flyknit-2-mens-running-shoe-2S0Cn1-removebg-preview.png']", new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8738), "Nike Epic React Flyknit 2", 89.97m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
