using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using WebCrawl.Logic.Crawler;

namespace WebCrawl.Logic.Tests.Crawler
{
    public class HtmlPageParserTests
    {
        private readonly Mock<WebContentLoader> _mockWebLoader;
        private readonly HtmlPageParser _htmlParser;

        public HtmlPageParserTests()
        {
            _mockWebLoader = new Mock<WebContentLoader>();

            _htmlParser = new HtmlPageParser(_mockWebLoader.Object, new ReferenceValidation(new WebContentLoader()), new UrlConverter());
        }

        [Test, Timeout(1000)]
        public void ParsePageForUrls_HtmlContent_ShouldReturnAllCorrectUrls()
        {
            var fakeUrl = "https://example.com/";

            var fakeHTML = @"<a href=""https://example.com/Contacts/"">Link 1</a>
                             <a href =""About/"">Link 2</a>";

            _mockWebLoader.Setup(mock => mock.DownloadContent(It.IsAny<string>()))
                .Returns("");
            _mockWebLoader.Setup(mock => mock.DownloadContent(fakeUrl))
                .Returns(fakeHTML);

            var expectedList = new List<string>
            {
                "https://example.com/Contacts/",
                "https://example.com/About/"
            };

            var result = _htmlParser.ParsePageForUrls(fakeUrl, fakeUrl);

            Assert.AreEqual(expectedList, result);
        }

        [Test, Timeout(1000)]
        public void ParsePageForUrls_HtmlContent_ShouldIgnoreEmptyUrls()
        {
            var fakeUrl = "https://example.com/";

            var fakeHTML = @"<a href=""https://example.com/"">Link 1</a>
                             <a href ="""">Link 2</a>
                             <a href =""https://example.com/Home/"">Link 3</a>
                             <a href ="""">Link 4</a>";

            _mockWebLoader.Setup(mock => mock.DownloadContent(It.IsAny<string>()))
                .Returns("");
            _mockWebLoader.Setup(mock => mock.DownloadContent(fakeUrl))
                .Returns(fakeHTML);

            var expectedList = new List<string>
            {
                "https://example.com/",
                "https://example.com/Home/"
            };

            var result = _htmlParser.ParsePageForUrls(fakeUrl, fakeUrl);

            Assert.AreEqual(expectedList, result);
        }

        [Test, Timeout(1000)]
        public void ParsePageForUrls_HtmlContentWithoutRefs_ShouldReturnNull()
        {
            var fakeUrl = "https://example.com/";

            _mockWebLoader.Setup(mock => mock.DownloadContent(It.IsAny<string>()))
                .Returns("");

            var result = _htmlParser.ParsePageForUrls(fakeUrl, fakeUrl);

            Assert.IsNull(result);
        }

        [Test, Timeout(1000)]
        public void ParsePageForUrls_UrlContentIsNull_ShouldReturnEmptyCollection()
        {
            var fakeUrl = "https://example.com/";

            _mockWebLoader.Setup(mock => mock.DownloadContent(fakeUrl))
                .Throws<WebException>();

            var result = Assert.Throws<WebException>(() => { _htmlParser.ParsePageForUrls(fakeUrl, fakeUrl); });

            Assert.AreEqual(new WebException().GetType(), result.GetType());
        }
    }
}
