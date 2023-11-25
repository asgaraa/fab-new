using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class gipsModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GipsTranslates_Gipses_GipsId",
                table: "GipsTranslates");

            migrationBuilder.DropForeignKey(
                name: "FK_GipsTranslates_Paints_PaintId",
                table: "GipsTranslates");

            migrationBuilder.DropIndex(
                name: "IX_GipsTranslates_PaintId",
                table: "GipsTranslates");

            migrationBuilder.DropColumn(
                name: "PaintId",
                table: "GipsTranslates");

            migrationBuilder.AlterColumn<int>(
                name: "GipsId",
                table: "GipsTranslates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GipsTranslates_Gipses_GipsId",
                table: "GipsTranslates",
                column: "GipsId",
                principalTable: "Gipses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GipsTranslates_Gipses_GipsId",
                table: "GipsTranslates");

            migrationBuilder.AlterColumn<int>(
                name: "GipsId",
                table: "GipsTranslates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PaintId",
                table: "GipsTranslates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GipsTranslates_PaintId",
                table: "GipsTranslates",
                column: "PaintId");

            migrationBuilder.AddForeignKey(
                name: "FK_GipsTranslates_Gipses_GipsId",
                table: "GipsTranslates",
                column: "GipsId",
                principalTable: "Gipses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GipsTranslates_Paints_PaintId",
                table: "GipsTranslates",
                column: "PaintId",
                principalTable: "Paints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
