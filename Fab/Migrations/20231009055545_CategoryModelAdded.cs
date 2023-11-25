using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class CategoryModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "CategoryTranslates");

            migrationBuilder.RenameColumn(
                name: "Header",
                table: "CategoryTranslates",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CategoryTranslates",
                newName: "Header");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "CategoryTranslates",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
