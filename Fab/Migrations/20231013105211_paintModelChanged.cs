using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class paintModelChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "PaintTranslates");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "PaintTranslates",
                newName: "WeightOrVolume");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Paints",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Paints",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Paints");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Paints");

            migrationBuilder.RenameColumn(
                name: "WeightOrVolume",
                table: "PaintTranslates",
                newName: "Weight");

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "PaintTranslates",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
