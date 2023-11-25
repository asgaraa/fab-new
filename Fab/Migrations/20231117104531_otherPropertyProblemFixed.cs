using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class otherPropertyProblemFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicsTranslates_ApplicationFields_ApplicationFiel~",
                table: "CharacteristicsTranslates");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicsTranslates_ApplicationFieldId",
                table: "CharacteristicsTranslates");

            migrationBuilder.DropColumn(
                name: "ApplicationFieldId",
                table: "CharacteristicsTranslates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationFieldId",
                table: "CharacteristicsTranslates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicsTranslates_ApplicationFieldId",
                table: "CharacteristicsTranslates",
                column: "ApplicationFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicsTranslates_ApplicationFields_ApplicationFiel~",
                table: "CharacteristicsTranslates",
                column: "ApplicationFieldId",
                principalTable: "ApplicationFields",
                principalColumn: "Id");
        }
    }
}
