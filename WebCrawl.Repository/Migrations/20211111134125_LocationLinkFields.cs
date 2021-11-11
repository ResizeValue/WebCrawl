using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawl.Repository.Migrations
{
    public partial class LocationLinkFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCrawlingLink",
                table: "ParsedHtmlDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSitemapLink",
                table: "ParsedHtmlDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCrawlingLink",
                table: "ParsedHtmlDocuments");

            migrationBuilder.DropColumn(
                name: "IsSitemapLink",
                table: "ParsedHtmlDocuments");
        }
    }
}
