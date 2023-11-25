using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fab.Migrations
{
    public partial class nullableparametrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AppearanceFields_AppearanceFieldId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicationFields_ApplicationFieldId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Characteristics_CharacteristicId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductTranslates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProducerCountry",
                table: "ProductTranslates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductTranslates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Length",
                table: "ProductTranslates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "ProductTranslates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "CharacteristicId",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationFieldId",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AppearanceFieldId",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AppearanceFields_AppearanceFieldId",
                table: "Products",
                column: "AppearanceFieldId",
                principalTable: "AppearanceFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicationFields_ApplicationFieldId",
                table: "Products",
                column: "ApplicationFieldId",
                principalTable: "ApplicationFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Characteristics_CharacteristicId",
                table: "Products",
                column: "CharacteristicId",
                principalTable: "Characteristics",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AppearanceFields_AppearanceFieldId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicationFields_ApplicationFieldId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Characteristics_CharacteristicId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductTranslates",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProducerCountry",
                table: "ProductTranslates",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductTranslates",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Length",
                table: "ProductTranslates",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "ProductTranslates",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CharacteristicId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationFieldId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppearanceFieldId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AppearanceFields_AppearanceFieldId",
                table: "Products",
                column: "AppearanceFieldId",
                principalTable: "AppearanceFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicationFields_ApplicationFieldId",
                table: "Products",
                column: "ApplicationFieldId",
                principalTable: "ApplicationFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Characteristics_CharacteristicId",
                table: "Products",
                column: "CharacteristicId",
                principalTable: "Characteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
