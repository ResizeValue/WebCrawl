using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebCrawl.Logic;
using WebCrawl.Logic.Crawler;
using WebCrawl.Logic.Sitemap;
using WebCrawl.Repository;

namespace WebCrawl.ConsoleApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var app = host.Services.GetService<ConsoleWebCrawler>();

            app.Run();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddEfRepository<WebCrawlDbContext>(options =>
                options.UseSqlServer(@"Server=DESKTOP-LRDGVT1;Database=WebCrawlerDb;User Id=sa;Password=q1w2e3r4;"));

                services.AddScoped<ConsoleWrapper>();
                services.AddScoped<ConsoleWebCrawler>();
                services.AddScoped<HtmlCrawler>();
                services.AddScoped<HtmlPageParser>();
                services.AddScoped<ReferenceValidation>();
                services.AddScoped<SitemapParser>();
                services.AddScoped<XmlParser>();
                services.AddScoped<RepositoryService>();
                services.AddScoped<HtmlResponseTracker>();
                services.AddScoped<WebContentLoader>();
                services.AddScoped<WebCrawler>();
            }).ConfigureLogging(options => options.SetMinimumLevel(LogLevel.Error));
    }
}