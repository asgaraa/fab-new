using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fab.Migrations
{
    public partial class productAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "ProductTranslates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductTranslates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Properties",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExPropValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExPropId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "CrossTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExPropId = table.Column<int>(type: "integer", nullable: false),
                    ExPropValueId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrossTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrossTable_ExProps_ExPropId",
                        column: x => x.ExPropId,
                        principalTable: "ExProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrossTable_ExPropValues_ExPropValueId",
                        column: x => x.ExPropValueId,
                        principalTable: "ExPropValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrossTable_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExPropValueTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    ExPropValueId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CrossTable_ExPropId",
                table: "CrossTable",
                column: "ExPropId");

            migrationBuilder.CreateIndex(
                name: "IX_CrossTable_ExPropValueId",
                table: "CrossTable",
                column: "ExPropValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CrossTable_ProductId",
                table: "CrossTable",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExPropValues_ExPropId",
                table: "ExPropValues",
                column: "ExPropId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExPropValueTranslates_ExPropValueId",
                table: "ExPropValueTranslates",
                column: "ExPropValueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrossTable");

            migrationBuilder.DropTable(
                name: "ExPropValueTranslates");

            migrationBuilder.DropTable(
                name: "ExPropValues");

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "ProductTranslates");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductTranslates");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Properties",
                table: "Products");
        }
    }
}
