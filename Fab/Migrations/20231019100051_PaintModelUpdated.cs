using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class PaintModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintImage_Paints_PaintId",
                table: "PaintImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaintImage",
                table: "PaintImage");

            migrationBuilder.RenameTable(
                name: "PaintImage",
                newName: "PaintImages");

            migrationBuilder.RenameIndex(
                name: "IX_PaintImage_PaintId",
                table: "PaintImages",
                newName: "IX_PaintImages_PaintId");

            migrationBuilder.AddColumn<string>(
                name: "PropertiesFileName",
                table: "Paints",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaintImages",
                table: "PaintImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintImages_Paints_PaintId",
                table: "PaintImages",
                column: "PaintId",
                principalTable: "Paints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintImages_Paints_PaintId",
                table: "PaintImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaintImages",
                table: "PaintImages");

            migrationBuilder.DropColumn(
                name: "PropertiesFileName",
                table: "Paints");

            migrationBuilder.RenameTable(
                name: "PaintImages",
                newName: "PaintImage");

            migrationBuilder.RenameIndex(
                name: "IX_PaintImages_PaintId",
                table: "PaintImage",
                newName: "IX_PaintImage_PaintId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaintImage",
                table: "PaintImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintImage_Paints_PaintId",
                table: "PaintImage",
                column: "PaintId",
                principalTable: "Paints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
