using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebCrawl.Logic.Services;
using WebCrawl.Repository.Configuration;

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
                .AddUserSecrets<Program>()
                .Build();

                services.AddEfRepository(configuration);
                services.AddScoped();

                services.AddScoped<ConsoleWrapper>();
                services.AddScoped<ConsoleWebCrawler>();

            }).ConfigureLogging(options => options.SetMinimumLevel(LogLevel.Error));
    }
}