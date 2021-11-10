using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawl.Repository.Migrations
{
    public partial class modelNameFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckedPages");

            migrationBuilder.CreateTable(
                name: "ParsedHtmlDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CrawlingResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParsedHtmlDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParsedHtmlDocuments_CrawlingResults_CrawlingResultId",
                        column: x => x.CrawlingResultId,
                        principalTable: "CrawlingResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParsedHtmlDocuments_CrawlingResultId",
                table: "ParsedHtmlDocuments",
                column: "CrawlingResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParsedHtmlDocuments");

            migrationBuilder.CreateTable(
                name: "CheckedPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrawlingResultId = table.Column<int>(type: "int", nullable: true),
                    ResponseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckedPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckedPages_CrawlingResults_CrawlingResultId",
                        column: x => x.CrawlingResultId,
                        principalTable: "CrawlingResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckedPages_CrawlingResultId",
                table: "CheckedPages",
                column: "CrawlingResultId");
        }
    }
}
