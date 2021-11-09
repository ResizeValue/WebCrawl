using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WebCrawl.Logic.Crawler;

namespace WebCrawl.Logic.Tests.Crawler
{
    public class HtmlCrawlerTests
    {
        private readonly Mock<HtmlPageParser> _mockHtmlParser;
        private readonly HtmlCrawler _htmlCrawler;

        public HtmlCrawlerTests()
        {
            _mockHtmlParser = new Mock<HtmlPageParser>(new Mock<WebContentLoader>().Object,
                new ReferenceValidation());

            _htmlCrawler = new HtmlCrawler(_mockHtmlParser.Object);
        }

        [Test, Timeout(1000)]
        public void ParseUrl_MultipleUrls_ShouldIgnoreDuplicates()
        {
            var fakeUrl = "https://example.com/";

            var fakeUrlsList = new List<string>
            {
                "https://example.com/Home/",
                "https://example.com/Home/",
                "https://example.com/About/"
            };

            var excpectedResult = new List<string>
            {
                "https://example.com/",
                "https://example.com/Home/",
                "https://example.com/About/"
            };

            _mockHtmlParser.Setup(mock => mock.ParsePageForUrls(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string>());
            _mockHtmlParser.Setup(mock => mock.ParsePageForUrls(fakeUrl, fakeUrl))
                .Returns(fakeUrlsList);

            var result = _htmlCrawler.ParseHtmlDocuments(fakeUrl);

            Assert.AreEqual(excpectedResult, result);
        }

        [Test, Timeout(1000)]
        public void ParsePageForUrls_HtmlContent_ShouldIgnoreUrlsToOtherSites()
        {
            var fakeUrl = "https://example.com/";

            var fakeUrlsList = new List<string>
            {
                "https://example.com/Home/",
                "https://anotherSite.com/",
                "https://example.com/About/",
                "https://anotherSite.com/Contacts"
            };

            var excpectedResult = new List<string>
            {
                "https://example.com/",
                "https://example.com/Home/",
                "https://example.com/About/"
            };

            _mockHtmlParser.Setup(mock => mock.ParsePageForUrls(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string>());
            _mockHtmlParser.Setup(mock => mock.ParsePageForUrls(fakeUrl, fakeUrl))
                .Returns(fakeUrlsList);

            var result = _htmlCrawler.ParseHtmlDocuments(fakeUrl);

            Assert.AreEqual(excpectedResult, result);
        }
    }
}
