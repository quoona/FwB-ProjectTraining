using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwB.Migrations
{
    public partial class addColumnValueDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Order_FK_DISCOUNTID",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Discount_FK_DISCOUNTID",
                table: "Discount");

            migrationBuilder.RenameColumn(
                name: "FK_DISCOUNTID",
                table: "Discount",
                newName: "Value");

            migrationBuilder.AddColumn<int>(
                name: "FK_DISCOUNTID",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_FK_DISCOUNTID",
                table: "Order",
                column: "FK_DISCOUNTID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Discount_FK_DISCOUNTID",
                table: "Order",
                column: "FK_DISCOUNTID",
                principalTable: "Discount",
                principalColumn: "DiscountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Discount_FK_DISCOUNTID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_FK_DISCOUNTID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "FK_DISCOUNTID",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Value",
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
    }
}
