using NUnit.Framework;
using Moq;
using System.IO;

namespace WebCrawl.Tests
{
    public class WebCrawlerTests
    {
        private readonly WebCrawler.WebCrawler webCrawler = new WebCrawler.WebCrawler();

        [SetUp]
        public void Setup()
        {
        }
        

       [Test]
        public void ParseSitemap_InputEmptyString_ReturnException()
        {
            var result = Assert.Throws<FileNotFoundException>(() => { webCrawler.ParseSitemap(string.Empty); });

            Assert.AreEqual(result.Message, "Sitemap does not found: ");
        }

        [Test]
        public void LoadSitemap_InputEmptyString_ReturnException()
        {
            var result = Assert.Throws<FileNotFoundException>(() => { webCrawler.ParseSitemap(string.Empty); });

            Assert.AreEqual(result.Message, "Sitemap does not found: ");
        }

        [Test]
        public void LoadSitemap_InputIncorrectUrl_ReturnException()
        {
            var result = Assert.Throws<FileNotFoundException>(() => { webCrawler.LoadSitemap("testUrl"); });

            Assert.AreEqual(result.Message, "Sitemap does not found: testUrl/sitemap.xml");
        }
    }
}