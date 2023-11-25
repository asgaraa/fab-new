using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fab.Migrations
{
    public partial class producttexturesRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExPropTranslates");

            migrationBuilder.DropTable(
                name: "ExPropValueTranslates");

            migrationBuilder.DropTable(
                name: "ProductTextures");

            migrationBuilder.DropTable(
                name: "TextureTranslate");

            migrationBuilder.DropTable(
                name: "TextureValueTranslate");

            migrationBuilder.DropTable(
                name: "ExPropValues");

            migrationBuilder.DropTable(
                name: "Textures");

            migrationBuilder.DropTable(
                name: "TextureValues");

            migrationBuilder.DropTable(
                name: "ExProps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExProps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExProps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExProps_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Textures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    TextureValueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Textures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Textures_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextureValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    TextureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextureValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExPropTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExPropId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExPropTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExPropTranslates_ExProps_ExPropId",
                        column: x => x.ExPropId,
                        principalTable: "ExProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExPropValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExPropId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExPropValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExPropValues_ExProps_ExPropId",
                        column: x => x.ExPropId,
                        principalTable: "ExProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextureTranslate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TextureId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextureTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextureTranslate_Textures_TextureId",
                        column: x => x.TextureId,
                        principalTable: "Textures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextureValueTranslate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TextureValueId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ExPropValueTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExPropValueId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExPropValueTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExPropValueTranslates_ExPropValues_ExPropValueId",
                        column: x => x.ExPropValueId,
                        principalTable: "ExPropValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTextures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExPropId = table.Column<int>(type: "integer", nullable: false),
                    ExPropValueId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTextures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTextures_ExProps_ExPropId",
                        column: x => x.ExPropId,
                        principalTable: "ExProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTextures_ExPropValues_ExPropValueId",
                        column: x => x.ExPropValueId,
                        principalTable: "ExPropValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTextures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExProps_CategoryId",
                table: "ExProps",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExPropTranslates_ExPropId",
                table: "ExPropTranslates",
                column: "ExPropId");

            migrationBuilder.CreateIndex(
                name: "IX_ExPropValues_ExPropId",
                table: "ExPropValues",
                column: "ExPropId");

            migrationBuilder.CreateIndex(
                name: "IX_ExPropValueTranslates_ExPropValueId",
                table: "ExPropValueTranslates",
                column: "ExPropValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTextures_ExPropId",
                table: "ProductTextures",
                column: "ExPropId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTextures_ExPropValueId",
                table: "ProductTextures",
                column: "ExPropValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTextures_ProductId",
                table: "ProductTextures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Textures_CategoryId",
                table: "Textures",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TextureTranslate_TextureId",
                table: "TextureTranslate",
                column: "TextureId");

            migrationBuilder.CreateIndex(
                name: "IX_TextureValueTranslate_TextureValueId",
                table: "TextureValueTranslate",
                column: "TextureValueId");
        }
    }
}
