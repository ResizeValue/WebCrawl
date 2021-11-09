using Microsoft.EntityFrameworkCore;
using System.Data;
using WebCrawl.Entity.Models;

namespace WebCrawl.Repository
{
    public class WebCrawlDbContext : DbContext, IEfRepositoryDbContext
    {
        public DbSet<CrawlingResult> CrawlingResults { get; set; }
        public DbSet<CheckedPage> CheckedPages { get; set; }

        public WebCrawlDbContext(DbContextOptions<WebCrawlDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-LRDGVT1;Database=WebCrawlerDb;User Id=sa;Password=q1w2e3r4;");
        }
    }
}
