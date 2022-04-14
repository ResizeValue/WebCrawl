using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCrawl.Entity.Models;

namespace WebCrawl.Entity.Configuration
{
    public class ParsedHtmlDocumentConfiguration : IEntityTypeConfiguration<ParsedHtmlDocument>
    {
        public void Configure(EntityTypeBuilder<ParsedHtmlDocument> builder)
        {
            builder.Property(x => x.Url)
                .IsRequired();

            builder.HasKey(x => x.Id);
        }
    }
}
