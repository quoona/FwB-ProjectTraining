using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwB.Migrations
{
    public partial class FKDISCOUNTITEM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemsItemId",
                table: "Discount",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discount_ItemsItemId",
                table: "Discount",
                column: "ItemsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Item_ItemsItemId",
                table: "Discount",
                column: "ItemsItemId",
                principalTable: "Item",
                principalColumn: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Item_ItemsItemId",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Discount_ItemsItemId",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "ItemsItemId",
                table: "Discount");
        }
    }
}
