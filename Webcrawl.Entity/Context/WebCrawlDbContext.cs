using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawl.Entity.Context
{
    public class WebCrawlDbContext: DbContext
    {
        public WebCrawlDbContext(DbContextOptions<WebCrawlDbContext> options):
            base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WebCrawlerDb;Trusted_Connection=True;");
        }
    }
}
