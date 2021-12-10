using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwB.Migrations
{
    public partial class EditOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Order_FK_ITEMID",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_FK_ITEMID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "FK_ITEMID",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "Order",
                newName: "FK_ITEMID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_FK_ITEMID",
                table: "Order",
                column: "FK_ITEMID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Item_FK_ITEMID",
                table: "Order",
                column: "FK_ITEMID",
                principalTable: "Item",
                principalColumn: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Item_FK_ITEMID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_FK_ITEMID",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "FK_ITEMID",
                table: "Order",
                newName: "MenuId");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FK_ITEMID",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_FK_ITEMID",
                table: "Item",
                column: "FK_ITEMID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Order_FK_ITEMID",
                table: "Item",
                column: "FK_ITEMID",
                principalTable: "Order",
                principalColumn: "OrderId");
        }
    }
}
