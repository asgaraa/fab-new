using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class gipsModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GipsImages_Gipses_GipsId",
                table: "GipsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_GipsImages_Paints_PaintId",
                table: "GipsImages");

            migrationBuilder.DropIndex(
                name: "IX_GipsImages_PaintId",
                table: "GipsImages");

            migrationBuilder.DropColumn(
                name: "PaintId",
                table: "GipsImages");

            migrationBuilder.AlterColumn<int>(
                name: "GipsId",
                table: "GipsImages",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GipsImages_Gipses_GipsId",
                table: "GipsImages",
                column: "GipsId",
                principalTable: "Gipses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GipsImages_Gipses_GipsId",
                table: "GipsImages");

            migrationBuilder.AlterColumn<int>(
                name: "GipsId",
                table: "GipsImages",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PaintId",
                table: "GipsImages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GipsImages_PaintId",
                table: "GipsImages",
                column: "PaintId");

            migrationBuilder.AddForeignKey(
                name: "FK_GipsImages_Gipses_GipsId",
                table: "GipsImages",
                column: "GipsId",
                principalTable: "Gipses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GipsImages_Paints_PaintId",
                table: "GipsImages",
                column: "PaintId",
                principalTable: "Paints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
