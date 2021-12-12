using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwB.Migrations
{
    public partial class DeleteMenuId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Item_FK_MENUID",
                table: "Menu");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "Item",
                newName: "MenusMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_MenusMenuId",
                table: "Item",
                column: "MenusMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Menu_MenusMenuId",
                table: "Item",
                column: "MenusMenuId",
                principalTable: "Menu",
                principalColumn: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Menu_MenusMenuId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_MenusMenuId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "MenusMenuId",
                table: "Item",
                newName: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Item_FK_MENUID",
                table: "Menu",
                column: "FK_MENUID",
                principalTable: "Item",
                principalColumn: "ItemId");
        }
    }
}
