using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fab.Migrations
{
    public partial class testDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextureTranslates_Textures_TextureId",
                table: "TextureTranslates");

            migrationBuilder.DropTable(
                name: "CategoryTexture");

            migrationBuilder.DropTable(
                name: "ProductTextures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextureTranslates",
                table: "TextureTranslates");

            migrationBuilder.DropColumn(
                name: "CategoriesIds",
                table: "Textures");

            migrationBuilder.RenameTable(
                name: "TextureTranslates",
                newName: "TextureTranslate");

            migrationBuilder.RenameIndex(
                name: "IX_TextureTranslates_TextureId",
                table: "TextureTranslate",
                newName: "IX_TextureTranslate_TextureId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Textures",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TextureValueId",
                table: "Textures",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextureTranslate",
                table: "TextureTranslate",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TextureValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TextureId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextureValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextureValueTranslate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    TextureValueId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextureValueTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextureValueTranslate_TextureValues_TextureValueId",
                        column: x => x.TextureValueId,
                        principalTable: "TextureValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Textures_CategoryId",
                table: "Textures",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TextureValueTranslate_TextureValueId",
                table: "TextureValueTranslate",
                column: "TextureValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Textures_Categories_CategoryId",
                table: "Textures",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextureTranslate_Textures_TextureId",
                table: "TextureTranslate",
                column: "TextureId",
                principalTable: "Textures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Textures_Categories_CategoryId",
                table: "Textures");

            migrationBuilder.DropForeignKey(
                name: "FK_TextureTranslate_Textures_TextureId",
                table: "TextureTranslate");

            migrationBuilder.DropTable(
                name: "TextureValueTranslate");

            migrationBuilder.DropTable(
                name: "TextureValues");

            migrationBuilder.DropIndex(
                name: "IX_Textures_CategoryId",
                table: "Textures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextureTranslate",
                table: "TextureTranslate");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Textures");

            migrationBuilder.DropColumn(
                name: "TextureValueId",
                table: "Textures");

            migrationBuilder.RenameTable(
                name: "TextureTranslate",
                newName: "TextureTranslates");

            migrationBuilder.RenameIndex(
                name: "IX_TextureTranslate_TextureId",
                table: "TextureTranslates",
                newName: "IX_TextureTranslates_TextureId");

            migrationBuilder.AddColumn<List<int>>(
                name: "CategoriesIds",
                table: "Textures",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextureTranslates",
                table: "TextureTranslates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CategoryTexture",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    TexturesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTexture", x => new { x.CategoriesId, x.TexturesId });
                    table.ForeignKey(
                        name: "FK_CategoryTexture_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTexture_Textures_TexturesId",
                        column: x => x.TexturesId,
                        principalTable: "Textures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTextures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    TextureId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTextures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTextures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTextures_Textures_TextureId",
                        column: x => x.TextureId,
                        principalTable: "Textures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTexture_TexturesId",
                table: "CategoryTexture",
                column: "TexturesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTextures_ProductId",
                table: "ProductTextures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTextures_TextureId",
                table: "ProductTextures",
                column: "TextureId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextureTranslates_Textures_TextureId",
                table: "TextureTranslates",
                column: "TextureId",
                principalTable: "Textures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
