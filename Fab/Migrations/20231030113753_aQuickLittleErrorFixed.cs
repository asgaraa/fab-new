using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class aQuickLittleErrorFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategroryId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "File",
                table: "Subcategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategroryId",
                table: "Subcategories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Subcategories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
