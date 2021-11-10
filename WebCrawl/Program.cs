using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebCrawl.Logic;
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
                IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

                services.AddEfRepository<WebCrawlDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CrawlerDb")));

                services.AddScoped();

                services.AddScoped<ConsoleWrapper>();
                services.AddScoped<ConsoleWebCrawler>();

            }).ConfigureLogging(options => options.SetMinimumLevel(LogLevel.Error));
    }
}