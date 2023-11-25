using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class appearanceProblemFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicsTranslates_AppearanceFields_AppearanceFieldId",
                table: "CharacteristicsTranslates");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicsTranslates_AppearanceFieldId",
                table: "CharacteristicsTranslates");

            migrationBuilder.DropColumn(
                name: "AppearanceFieldId",
                table: "CharacteristicsTranslates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppearanceFieldId",
                table: "CharacteristicsTranslates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicsTranslates_AppearanceFieldId",
                table: "CharacteristicsTranslates",
                column: "AppearanceFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicsTranslates_AppearanceFields_AppearanceFieldId",
                table: "CharacteristicsTranslates",
                column: "AppearanceFieldId",
                principalTable: "AppearanceFields",
                principalColumn: "Id");
        }
    }
}
