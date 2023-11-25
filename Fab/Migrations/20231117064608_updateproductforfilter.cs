using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fab.Migrations
{
    public partial class updateproductforfilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppearanceFieldId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationFieldId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CharacteristicId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppearanceFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppearanceFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppearanceFields_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationFields_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characteristics_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Tel = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppearanceFIeldTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    AppearanceFieldId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppearanceFIeldTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppearanceFIeldTranslates_AppearanceFields_AppearanceFieldId",
                        column: x => x.AppearanceFieldId,
                        principalTable: "AppearanceFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationFieldTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    ApplicationFieldId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFieldTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationFieldTranslates_ApplicationFields_ApplicationFie~",
                        column: x => x.ApplicationFieldId,
                        principalTable: "ApplicationFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicsTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    CharacteristicsId = table.Column<int>(type: "integer", nullable: false),
                    AppearanceFieldId = table.Column<int>(type: "integer", nullable: true),
                    ApplicationFieldId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicsTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicsTranslates_AppearanceFields_AppearanceFieldId",
                        column: x => x.AppearanceFieldId,
                        principalTable: "AppearanceFields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacteristicsTranslates_ApplicationFields_ApplicationFiel~",
                        column: x => x.ApplicationFieldId,
                        principalTable: "ApplicationFields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacteristicsTranslates_Characteristics_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AppearanceFieldId",
                table: "Products",
                column: "AppearanceFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ApplicationFieldId",
                table: "Products",
                column: "ApplicationFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CharacteristicId",
                table: "Products",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceFields_CategoryId",
                table: "AppearanceFields",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceFIeldTranslates_AppearanceFieldId",
                table: "AppearanceFIeldTranslates",
                column: "AppearanceFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFields_CategoryId",
                table: "ApplicationFields",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFieldTranslates_ApplicationFieldId",
                table: "ApplicationFieldTranslates",
                column: "ApplicationFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CategoryId",
                table: "Characteristics",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicsTranslates_AppearanceFieldId",
                table: "CharacteristicsTranslates",
                column: "AppearanceFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicsTranslates_ApplicationFieldId",
                table: "CharacteristicsTranslates",
                column: "ApplicationFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicsTranslates_CharacteristicsId",
                table: "CharacteristicsTranslates",
                column: "CharacteristicsId");

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

            migrationBuilder.DropTable(
                name: "AppearanceFIeldTranslates");

            migrationBuilder.DropTable(
                name: "ApplicationFieldTranslates");

            migrationBuilder.DropTable(
                name: "CharacteristicsTranslates");

            migrationBuilder.DropTable(
                name: "CVs");

            migrationBuilder.DropTable(
                name: "AppearanceFields");

            migrationBuilder.DropTable(
                name: "ApplicationFields");

            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Products_AppearanceFieldId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ApplicationFieldId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CharacteristicId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AppearanceFieldId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApplicationFieldId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CharacteristicId",
                table: "Products");
        }
    }
}
