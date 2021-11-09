using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCrawl.Entity.Models;

namespace WebCrawl.Entity.Configuration
{
    public class CheckedPageConfiguration : IEntityTypeConfiguration<CheckedPage>
    {
        public void Configure(EntityTypeBuilder<CheckedPage> builder)
        {
            builder.Property(x => x.Url)
                .IsRequired();

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
