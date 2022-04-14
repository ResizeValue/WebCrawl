using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCrawl.Entity.Models;

namespace WebCrawl.Entity.Configuration
{
    public class CrawlingResultConfiguration : IEntityTypeConfiguration<CrawlingResult>
    {
        public void Configure(EntityTypeBuilder<CrawlingResult> builder)
        {
            builder.Property(x => x.BasePage)
                .IsRequired();

            builder.HasKey(x => x.Id);
        }
    }
}
