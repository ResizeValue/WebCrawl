using Microsoft.Extensions.DependencyInjection;
using WebCrawl.Logic.Crawler;
using WebCrawl.Logic.Sitemap;

namespace WebCrawl.Logic
{
    public static class HostConfigurationServices
    {
        public static IServiceCollection AddScoped(this IServiceCollection services)
        {
            services.AddScoped<HtmlCrawler>();
            services.AddScoped<HtmlPageParser>();
            services.AddScoped<ReferenceValidation>();
            services.AddScoped<SitemapParser>();
            services.AddScoped<XmlParser>();
            services.AddScoped<RepositoryService>();
            services.AddScoped<HtmlResponseTracker>();
            services.AddScoped<WebContentLoader>();
            services.AddScoped<WebCrawler>();

            return services;
        }
    }
}
