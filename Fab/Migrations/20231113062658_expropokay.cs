using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fab.Migrations
{
    public partial class expropokay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrossTable");

            migrationBuilder.DropIndex(
                name: "IX_ExPropValues_ExPropId",
                table: "ExPropValues");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "Size");

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "IX_ExPropValues_ExPropId",
                table: "ExPropValues",
                column: "ExPropId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductTextures");

            migrationBuilder.DropIndex(
                name: "IX_ExPropValues_ExPropId",
                table: "ExPropValues");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Products",
                newName: "Image");

            migrationBuilder.CreateTable(
                name: "CrossTable",
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

            migrationBuilder.CreateIndex(
                name: "IX_ExPropValues_ExPropId",
                table: "ExPropValues",
                column: "ExPropId",
                unique: true);

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
        }
    }
}
