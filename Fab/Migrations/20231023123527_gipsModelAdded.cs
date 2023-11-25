using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fab.Migrations
{
    public partial class gipsModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gipses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCode = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    PropertiesFileName = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gipses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gipses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GipsImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    PaintId = table.Column<int>(type: "integer", nullable: false),
                    GipsId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GipsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GipsImages_Gipses_GipsId",
                        column: x => x.GipsId,
                        principalTable: "Gipses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GipsImages_Paints_PaintId",
                        column: x => x.PaintId,
                        principalTable: "Paints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GipsTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: false),
                    ProducerCountry = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    PaintId = table.Column<int>(type: "integer", nullable: false),
                    GipsId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GipsTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GipsTranslates_Gipses_GipsId",
                        column: x => x.GipsId,
                        principalTable: "Gipses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GipsTranslates_Paints_PaintId",
                        column: x => x.PaintId,
                        principalTable: "Paints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gipses_CategoryId",
                table: "Gipses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GipsImages_GipsId",
                table: "GipsImages",
                column: "GipsId");

            migrationBuilder.CreateIndex(
                name: "IX_GipsImages_PaintId",
                table: "GipsImages",
                column: "PaintId");

            migrationBuilder.CreateIndex(
                name: "IX_GipsTranslates_GipsId",
                table: "GipsTranslates",
                column: "GipsId");

            migrationBuilder.CreateIndex(
                name: "IX_GipsTranslates_PaintId",
                table: "GipsTranslates",
                column: "PaintId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GipsImages");

            migrationBuilder.DropTable(
                name: "GipsTranslates");

            migrationBuilder.DropTable(
                name: "Gipses");
        }
    }
}
