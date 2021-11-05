using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebCrawl.Crawler;
using WebCrawl.Models;
using WebCrawl.Sitemap;

namespace WebCrawl.Tests
{
    public class WebCrawlerTests
    {
        private readonly WebCrawler _mockWebCrawler;
        private readonly Mock<ReferenceValidation> _mockReferenceValidation;
        private readonly Mock<WebContentLoader> _mockWebLoader;
        private readonly Mock<HtmlCrawler> _mockHtmlCrawler;
        private readonly Mock<HtmlPageParser> _mockHtmlParser;
        private readonly Mock<XmlParser> _mockXmlParser;
        private readonly Mock<SitemapParser> _mockSitemapParser;

        public WebCrawlerTests()
        {
            _mockReferenceValidation = new Mock<ReferenceValidation>();
            _mockWebLoader = new Mock<WebContentLoader>();

            _mockHtmlParser = new Mock<HtmlPageParser>(_mockWebLoader.Object, _mockReferenceValidation.Object);

            _mockHtmlCrawler = new Mock<HtmlCrawler>(_mockHtmlParser.Object);

            _mockXmlParser = new Mock<XmlParser>();
            _mockSitemapParser = new Mock<SitemapParser>(_mockXmlParser.Object, _mockWebLoader.Object);

            _mockWebCrawler = new WebCrawler(_mockHtmlCrawler.Object, _mockSitemapParser.Object);

        }

        [Test, Timeout(1000)]
        public void ParseUrl_ShouldReturnListWithUnicUrls()
        {
            var fakeUrl = "https://example.com/";

            _mockHtmlCrawler.Setup(mock => mock.ParseHtmlDocuments(It.IsAny<string>()))
                .Returns(fakeCrawlCollection);

            _mockSitemapParser.Setup(mock => mock.ParseSitemap(It.IsAny<string>()))
                .Returns(fakeSitemapCollection);


            var resultUrls = _mockWebCrawler.ParseUrl(fakeUrl).Select(x => x.Url);

            CollectionAssert.AreEqual(ExpectedResult.Select(x => x.Url), resultUrls);
        }

        [Test, Timeout(1000)]
        public void ParseUrl_IsUrlsFromSitemap_ShouldReturnAllUrlsWithSitemapFlag()
        {
            var fakeUrl = "https://example.com/";

            _mockHtmlCrawler.Setup(mock => mock.ParseHtmlDocuments(It.IsAny<string>()))
                .Returns(fakeCrawlCollection);

            _mockSitemapParser.Setup(mock => mock.ParseSitemap(It.IsAny<string>()))
                .Returns(fakeSitemapCollection);


            var resultUrls = _mockWebCrawler.ParseUrl(fakeUrl);

            Assert.AreEqual(ExpectedResult.Select(x => x.IsCrawlerUrl), resultUrls.Select(x => x.IsCrawlerUrl));
        }

        [Test, Timeout(1000)]
        public void ParseUrl_IsUrlsFromCrawler_ShouldReturnAllUrlsWithCrawlerFlag()
        {
            var fakeUrl = "https://example.com/";

            _mockHtmlCrawler.Setup(mock => mock.ParseHtmlDocuments(It.IsAny<string>()))
                .Returns(fakeCrawlCollection);

            _mockSitemapParser.Setup(mock => mock.ParseSitemap(It.IsAny<string>()))
                .Returns(fakeSitemapCollection);


            var resultUrls = _mockWebCrawler.ParseUrl(fakeUrl);

            Assert.AreEqual(ExpectedResult.Select(x => x.IsCrawlerUrl), resultUrls.Select(x => x.IsCrawlerUrl));
        }

        private List<string> fakeCrawlCollection
        {
            get
            {
                return new List<string>
                {
                    "https://example.com/",
                    "https://example.com/Home/",
                    "https://example.com/About/",
                };
            }
        }
        private List<string> fakeSitemapCollection
        {
            get
            {
                return new List<string>
                {
                    "https://example.com/",
                    "https://example.com/Home/",
                    "https://example.com/Contacts/",
                };
            }
        }

        private List<ParsedUrl> ExpectedResult
        {
            get
            {
                return new List<ParsedUrl>
                {
                    new ParsedUrl
                    {
                        Url = "https://example.com/",
                        IsCrawlerUrl = true,
                        IsSitemapUrl = true
                    },
                    new ParsedUrl
                    {
                        Url = "https://example.com/Home/",
                        IsCrawlerUrl = true,
                        IsSitemapUrl = true
                    },
                    new ParsedUrl
                    {
                        Url = "https://example.com/About/",
                        IsCrawlerUrl = true,
                        IsSitemapUrl = false
                    },
                    new ParsedUrl
                    {
                        Url = "https://example.com/Contacts/",
                        IsCrawlerUrl = false,
                        IsSitemapUrl = true
                    }
                };
            }
        }
    }
}