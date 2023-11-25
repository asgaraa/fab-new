using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class InteriorTranslatesModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InteriorTranslate_Interiors_InteriorId",
                table: "InteriorTranslate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteriorTranslate",
                table: "InteriorTranslate");

            migrationBuilder.RenameTable(
                name: "InteriorTranslate",
                newName: "InteriorTranslates");

            migrationBuilder.RenameIndex(
                name: "IX_InteriorTranslate_InteriorId",
                table: "InteriorTranslates",
                newName: "IX_InteriorTranslates_InteriorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteriorTranslates",
                table: "InteriorTranslates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InteriorTranslates_Interiors_InteriorId",
                table: "InteriorTranslates",
                column: "InteriorId",
                principalTable: "Interiors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InteriorTranslates_Interiors_InteriorId",
                table: "InteriorTranslates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteriorTranslates",
                table: "InteriorTranslates");

            migrationBuilder.RenameTable(
                name: "InteriorTranslates",
                newName: "InteriorTranslate");

            migrationBuilder.RenameIndex(
                name: "IX_InteriorTranslates_InteriorId",
                table: "InteriorTranslate",
                newName: "IX_InteriorTranslate_InteriorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteriorTranslate",
                table: "InteriorTranslate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InteriorTranslate_Interiors_InteriorId",
                table: "InteriorTranslate",
                column: "InteriorId",
                principalTable: "Interiors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
