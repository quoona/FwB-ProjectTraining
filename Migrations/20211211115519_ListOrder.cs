using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwB.Migrations
{
    public partial class ListOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListOrder",
                columns: table => new
                {
                    ListOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdersOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOrder", x => x.ListOrderId);
                    table.ForeignKey(
                        name: "FK_ListOrder_Order_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListOrder_OrdersOrderId",
                table: "ListOrder",
                column: "OrdersOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListOrder");
        }
    }
}
