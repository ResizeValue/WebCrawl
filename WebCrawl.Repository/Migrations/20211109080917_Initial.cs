using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawl.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrawlingResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasePage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrawlingResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckedPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CrawlingResultId = table.Column<int>(type: "int", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckedPages");

            migrationBuilder.DropTable(
                name: "CrawlingResults");
        }
    }
}
