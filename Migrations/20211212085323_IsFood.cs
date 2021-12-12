using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwB.Migrations
{
    public partial class IsFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isFood",
                table: "Item",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFood",
                table: "Item");
        }
    }
}
