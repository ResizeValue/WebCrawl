using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebCrawl.Repository.Migrations;

namespace WebCrawl.Repository.Configuration
{
    public static class HostConfigurationServer
    {
        public static IServiceCollection AddEfRepository(this IServiceCollection services, IConfiguration config)
        {
            services.AddEfRepository<WebCrawlDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("CrawlerDb")));

            return services;
        }
    }
}
