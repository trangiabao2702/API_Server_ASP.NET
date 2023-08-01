using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTableOrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailInfoDto_Orders_OrderId",
                table: "OrderDetailInfoDto");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailInfoDto_Products_ProductId",
                table: "OrderDetailInfoDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetailInfoDto",
                table: "OrderDetailInfoDto");

            migrationBuilder.RenameTable(
                name: "OrderDetailInfoDto",
                newName: "OrderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetailInfoDto_ProductId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetailInfoDto_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4632), new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4642) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4928), new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4929) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4931), new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4931) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4933), new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4933) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4936), new DateTime(2023, 8, 1, 10, 0, 58, 919, DateTimeKind.Local).AddTicks(4936) });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "OrderDetailInfoDto");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetailInfoDto",
                newName: "IX_OrderDetailInfoDto_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetailInfoDto",
                newName: "IX_OrderDetailInfoDto_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetailInfoDto",
                table: "OrderDetailInfoDto",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2130), new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2139) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2267), new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2267) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2269), new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2270) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2272), new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2272) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2274), new DateTime(2023, 8, 1, 9, 45, 27, 769, DateTimeKind.Local).AddTicks(2274) });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailInfoDto_Orders_OrderId",
                table: "OrderDetailInfoDto",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailInfoDto_Products_ProductId",
                table: "OrderDetailInfoDto",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
