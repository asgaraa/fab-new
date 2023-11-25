using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class bannerFinalMigrationN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerTranslates_Banners_BannerId",
                table: "BannerTranslates");

            migrationBuilder.AlterColumn<int>(
                name: "BannerId",
                table: "BannerTranslates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BannerTranslates_Banners_BannerId",
                table: "BannerTranslates",
                column: "BannerId",
                principalTable: "Banners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerTranslates_Banners_BannerId",
                table: "BannerTranslates");

            migrationBuilder.AlterColumn<int>(
                name: "BannerId",
                table: "BannerTranslates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerTranslates_Banners_BannerId",
                table: "BannerTranslates",
                column: "BannerId",
                principalTable: "Banners",
                principalColumn: "Id");
        }
    }
}
