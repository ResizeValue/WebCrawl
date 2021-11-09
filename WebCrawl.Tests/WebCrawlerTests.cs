using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebCrawl.Logic.Crawler;
using WebCrawl.Logic.Models;
using WebCrawl.Logic.Sitemap;

namespace WebCrawl.Logic.Tests
{
    public class WebCrawlerTests
    {
        private readonly WebCrawler _mockWebCrawler;
        private readonly Mock<HtmlCrawler> _mockHtmlCrawler;
        private readonly Mock<SitemapParser> _mockSitemapParser;

        public WebCrawlerTests()
        {
            var htmlParser = new HtmlPageParser(new WebContentLoader(), new ReferenceValidation());

            _mockHtmlCrawler = new Mock<HtmlCrawler>(htmlParser);
            _mockSitemapParser = new Mock<SitemapParser>(new XmlParser(), new WebContentLoader());
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

        private List<string> fakeCrawlCollection =>
            new List<string>
            {
                "https://example.com/",
                "https://example.com/Home/",
                "https://example.com/About/",
            };

        private List<string> fakeSitemapCollection =>
             new List<string>
             {
                 "https://example.com/",
                 "https://example.com/Home/",
                 "https://example.com/Contacts/",
             };


        private List<ParsedUrl> ExpectedResult =>
                new List<ParsedUrl>
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