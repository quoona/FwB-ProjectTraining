using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwB.Migrations
{
    public partial class CreateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ItemByIdOrders",
                columns: table => new
                {
                    ItemByIdOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdersOrderId = table.Column<int>(type: "int", nullable: true),
                    ItemsItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemByIdOrders", x => x.ItemByIdOrderID);
                    table.ForeignKey(
                        name: "FK_ItemByIdOrders_Item_ItemsItemId",
                        column: x => x.ItemsItemId,
                        principalTable: "Item",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_ItemByIdOrders_Order_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemByIdOrders_ItemsItemId",
                table: "ItemByIdOrders",
                column: "ItemsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemByIdOrders_OrdersOrderId",
                table: "ItemByIdOrders",
                column: "OrdersOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemByIdOrders");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Order");
        }
    }
}
