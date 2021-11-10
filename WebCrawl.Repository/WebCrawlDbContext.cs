using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
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
