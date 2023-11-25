using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class ExPropProblemFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategroyId",
                table: "ExProps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategroyId",
                table: "ExProps",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
