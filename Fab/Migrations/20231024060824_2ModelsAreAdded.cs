using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fab.Migrations
{
    public partial class _2ModelsAreAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DryMixesTranslates");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "DryMixes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "DryMixes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "DryMixes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PropertiesFileName",
                table: "DryMixes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DryMixImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    DryMixId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DryMixImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DryMixImages_DryMixes_DryMixId",
                        column: x => x.DryMixId,
                        principalTable: "DryMixes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DryMixTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: false),
                    ProducerCountry = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    DryMixId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DryMixTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DryMixTranslates_DryMixes_DryMixId",
                        column: x => x.DryMixId,
                        principalTable: "DryMixes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Glues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCode = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    PropertiesFileName = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Glues_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GlueImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    GlueId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlueImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlueImages_Glues_GlueId",
                        column: x => x.GlueId,
                        principalTable: "Glues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlueTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: false),
                    ProducerCountry = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    GlueId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlueTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlueTranslates_Glues_GlueId",
                        column: x => x.GlueId,
                        principalTable: "Glues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DryMixes_CategoryId",
                table: "DryMixes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DryMixImages_DryMixId",
                table: "DryMixImages",
                column: "DryMixId");

            migrationBuilder.CreateIndex(
                name: "IX_DryMixTranslates_DryMixId",
                table: "DryMixTranslates",
                column: "DryMixId");

            migrationBuilder.CreateIndex(
                name: "IX_GlueImages_GlueId",
                table: "GlueImages",
                column: "GlueId");

            migrationBuilder.CreateIndex(
                name: "IX_Glues_CategoryId",
                table: "Glues",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GlueTranslates_GlueId",
                table: "GlueTranslates",
                column: "GlueId");

            migrationBuilder.AddForeignKey(
                name: "FK_DryMixes_Categories_CategoryId",
                table: "DryMixes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DryMixes_Categories_CategoryId",
                table: "DryMixes");

            migrationBuilder.DropTable(
                name: "DryMixImages");

            migrationBuilder.DropTable(
                name: "DryMixTranslates");

            migrationBuilder.DropTable(
                name: "GlueImages");

            migrationBuilder.DropTable(
                name: "GlueTranslates");

            migrationBuilder.DropTable(
                name: "Glues");

            migrationBuilder.DropIndex(
                name: "IX_DryMixes_CategoryId",
                table: "DryMixes");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "DryMixes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "DryMixes");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "DryMixes");

            migrationBuilder.DropColumn(
                name: "PropertiesFileName",
                table: "DryMixes");

            migrationBuilder.CreateTable(
                name: "DryMixesTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DryMixesTranslates", x => x.Id);
                });
        }
    }
}
