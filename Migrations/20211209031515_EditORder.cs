using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwB.Migrations
{
    public partial class EditORder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Discount_FK_ORDERID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_FK_ORDERID",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "FK_ORDERID",
                table: "Order",
                newName: "DiscountId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Discount",
                newName: "FK_DISCOUNTID");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_FK_DISCOUNTID",
                table: "Discount",
                column: "FK_DISCOUNTID");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Order_FK_DISCOUNTID",
                table: "Discount",
                column: "FK_DISCOUNTID",
                principalTable: "Order",
                principalColumn: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Order_FK_DISCOUNTID",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Discount_FK_DISCOUNTID",
                table: "Discount");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "Order",
                newName: "FK_ORDERID");

            migrationBuilder.RenameColumn(
                name: "FK_DISCOUNTID",
                table: "Discount",
                newName: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_FK_ORDERID",
                table: "Order",
                column: "FK_ORDERID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Discount_FK_ORDERID",
                table: "Order",
                column: "FK_ORDERID",
                principalTable: "Discount",
                principalColumn: "DiscountId");
        }
    }
}
