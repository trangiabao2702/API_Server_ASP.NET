using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Server.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId",
                unique: true,
                filter: "[PaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8485), new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8501) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8730), new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8731) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8733), new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8733) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8735), new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8735) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8737), new DateTime(2023, 7, 30, 15, 23, 48, 19, DateTimeKind.Local).AddTicks(8738) });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsId",
                table: "OrderProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
