using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebCrawl.Entity
{
    public class WebCrawlDbContext : DbContext, IEfRepositoryDbContext
    {
        public WebCrawlDbContext(DbContextOptions<WebCrawlDbContext> options) : 
            base(options)
        {
            Database.Migrate();
        }
    }
}
