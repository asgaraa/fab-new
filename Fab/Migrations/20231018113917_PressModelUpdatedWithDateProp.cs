using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class PressModelUpdatedWithDateProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "PressTranslates",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "PressTranslates");
        }
    }
}
