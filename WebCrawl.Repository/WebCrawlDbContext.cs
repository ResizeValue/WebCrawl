using Microsoft.EntityFrameworkCore;
using System.Data;
using WebCrawl.Entity.Configuration;
using WebCrawl.Entity.Models;

namespace WebCrawl.Repository
{
    public class WebCrawlDbContext : DbContext, IEfRepositoryDbContext
    {
        public DbSet<CrawlingResult> CrawlingResults { get; set; }
        public DbSet<ParsedHtmlDocument> ParsedHtmlDocuments { get; set; }

        public WebCrawlDbContext()
        {
            Database.Migrate();
        }

        public WebCrawlDbContext(DbContextOptions<WebCrawlDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CrawlingResultConfiguration).Assembly);
        }
    }
}
