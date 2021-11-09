﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCrawl.Repository;

namespace WebCrawl.Repository.Migrations
{
    [DbContext(typeof(WebCrawlDbContext))]
    partial class WebCrawlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebCrawl.Entity.Models.CheckedPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CrawlingResultId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("ResponseTime")
                        .HasColumnType("time");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CrawlingResultId");

                    b.ToTable("CheckedPages");
                });

            modelBuilder.Entity("WebCrawl.Entity.Models.CrawlingResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BasePage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CrawlingResults");
                });

            modelBuilder.Entity("WebCrawl.Entity.Models.CheckedPage", b =>
                {
                    b.HasOne("WebCrawl.Entity.Models.CrawlingResult", null)
                        .WithMany("Pages")
                        .HasForeignKey("CrawlingResultId");
                });

            modelBuilder.Entity("WebCrawl.Entity.Models.CrawlingResult", b =>
                {
                    b.Navigation("Pages");
                });
#pragma warning restore 612, 618
        }
    }
}
