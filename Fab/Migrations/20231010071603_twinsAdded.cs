using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fab.Migrations
{
    public partial class twinsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintImage_Paints_PaintId",
                table: "PaintImage");

            migrationBuilder.AlterColumn<int>(
                name: "PaintId",
                table: "PaintImage",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Twins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Twins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TwinImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WideImage = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    TwinId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwinImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TwinImages_Twins_TwinId",
                        column: x => x.TwinId,
                        principalTable: "Twins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TwinTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WideDesc = table.Column<string>(type: "text", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    TwinId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwinTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TwinTranslates_Twins_TwinId",
                        column: x => x.TwinId,
                        principalTable: "Twins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TwinImages_TwinId",
                table: "TwinImages",
                column: "TwinId");

            migrationBuilder.CreateIndex(
                name: "IX_TwinTranslates_TwinId",
                table: "TwinTranslates",
                column: "TwinId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintImage_Paints_PaintId",
                table: "PaintImage",
                column: "PaintId",
                principalTable: "Paints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintImage_Paints_PaintId",
                table: "PaintImage");

            migrationBuilder.DropTable(
                name: "TwinImages");

            migrationBuilder.DropTable(
                name: "TwinTranslates");

            migrationBuilder.DropTable(
                name: "Twins");

            migrationBuilder.AlterColumn<int>(
                name: "PaintId",
                table: "PaintImage",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintImage_Paints_PaintId",
                table: "PaintImage",
                column: "PaintId",
                principalTable: "Paints",
                principalColumn: "Id");
        }
    }
}
