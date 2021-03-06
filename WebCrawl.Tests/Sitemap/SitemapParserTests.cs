using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WebCrawl.Logic.Sitemap;

namespace WebCrawl.Logic.Tests.Sitemap
{
    public class SitemapParserTests
    {
        private readonly Mock<XmlParser> _mockXmlParser;
        private readonly Mock<WebContentLoader> _mockWebLoader;
        private readonly SitemapParser sitemapParser;

        public SitemapParserTests()
        {
            _mockXmlParser = new Mock<XmlParser>();
            _mockWebLoader = new Mock<WebContentLoader>();

            sitemapParser = new SitemapParser(_mockXmlParser.Object, _mockWebLoader.Object, new UrlConverter());
        }

        [Test, Timeout(1000)]
        public void ParseSitemap_ShouldCallDownloadContentAndParseXmlStringFuncsOnce()
        {
            var fakeUrl = "http://www.example.com/";

            var fakeUrlsList = new List<string>
            {
                "http://www.example.com/",
                "http://www.example.com/Home/",
                "http://www.example.com/About/"
            };

            _mockWebLoader.Setup(mock => mock.DownloadContent(It.IsAny<string>()))
                .Returns("xml");
            _mockXmlParser.Setup(mock => mock.ParseXmlString(It.IsAny<string>()))
                .Returns(fakeUrlsList);

            var result = sitemapParser.ParseSitemap(fakeUrl);

            _mockWebLoader.Verify(x => x.DownloadContent(It.IsAny<string>()), Times.Once);
            _mockXmlParser.Verify(x => x.ParseXmlString(It.IsAny<string>()), Times.Once);
        }
    }
}
