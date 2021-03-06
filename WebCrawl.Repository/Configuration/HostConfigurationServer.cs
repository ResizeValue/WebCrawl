using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebCrawl.Repository.Configuration
{
    public static class HostConfigurationServer
    {
        public static IServiceCollection AddEfRepository(this IServiceCollection services, IConfiguration config)
        {
            services.AddEfRepository<WebCrawlDbContext>(options =>
                options.UseSqlServer(config["CrawlerDb:ConnectionString"]));

            return services;
        }
    }
}
