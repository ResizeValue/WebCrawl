using Microsoft.EntityFrameworkCore;
using System.Data;
using WebCrawl.Entity.Configuration;
using WebCrawl.Entity.Models;

namespace WebCrawl.Repository
{
    public class WebCrawlDbContext : DbContext, IEfRepositoryDbContext
    {
        public DbSet<CrawlingResult> CrawlingResults { get; set; }
        public DbSet<CheckedPage> CheckedPages { get; set; }

        public WebCrawlDbContext()
        {

        }

        public WebCrawlDbContext(DbContextOptions<WebCrawlDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new CheckedPageConfiguration().Configure(builder.Entity<CheckedPage>());
            new CrawlingResultConfiguration().Configure(builder.Entity<CrawlingResult>());
        }

    }
}
