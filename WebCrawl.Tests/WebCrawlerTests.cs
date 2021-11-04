using NUnit.Framework;
using System.IO;
using Moq;
using WebCrawl.Crawler;
using WebCrawl.Sitemap;
using System.Collections.Generic;
using System.Linq;

namespace WebCrawl.Tests
{
    public class WebCrawlerTests
    {
        private readonly WebCrawler.WebCrawler _mockWebCrawler;
        private readonly Mock<ReferenceValidation> _mockReferenceValidation;
        private readonly Mock<WebLoader> _mockWebLoader;
        private readonly Mock<HtmlParser> _mockHtmlParser;
        private readonly Mock<XmlParser> _mockXmlParser;
        private readonly Mock<SitemapParser> _mockSitemapParser;

        public WebCrawlerTests()
        {
            _mockReferenceValidation = new Mock<ReferenceValidation>();
            _mockWebLoader = new Mock<WebLoader>();
            _mockHtmlParser = new Mock<HtmlParser>(_mockReferenceValidation.Object, _mockWebLoader.Object);

            _mockXmlParser = new Mock<XmlParser>();
            _mockSitemapParser = new Mock<SitemapParser>(_mockXmlParser.Object, _mockWebLoader.Object);

            _mockWebCrawler = new WebCrawler.WebCrawler(_mockHtmlParser.Object, _mockSitemapParser.Object);

        }


        [Test]
        public void ParseUrl_InputSingle_()
        {
            _mockHtmlParser.Setup(mock => mock.ParseUrl(It.IsAny<string>()))
                .Returns(new List<string>
                {
                    "Link1"
                });

            _mockSitemapParser.Setup(mock => mock.ParseSitemap(It.IsAny<string>()))
                .Returns(new List<string>());


            var result = _mockWebCrawler.ParseUrl("test").First();

            Assert.AreEqual("Link1", result.Url);
            Assert.AreEqual(false, result.IsSitemapUrl);
        }
    }
}