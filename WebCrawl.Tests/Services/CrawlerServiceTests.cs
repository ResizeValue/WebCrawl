using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebCrawl.Entity.Models;
using WebCrawl.Logic.Crawler;
using WebCrawl.Logic.Models;
using WebCrawl.Logic.Services;
using WebCrawl.Logic.Sitemap;

namespace WebCrawl.Logic.Tests.Services
{
    public class CrawlerServiceTests
    {
        private readonly RepositoryService _repositoryService;
        private readonly Mock<IRepository<CrawlingResult>> _mockIRepositoryService;
        private readonly Mock<WebCrawler> _mockWebCrawler;

        private readonly CrawlerService _crawlerService;

        public CrawlerServiceTests()
        {
            _mockIRepositoryService = new Mock<IRepository<CrawlingResult>>();
            _mockWebCrawler = new Mock<WebCrawler>(
                new HtmlCrawler(new HtmlPageParser(new WebContentLoader(), new ReferenceValidation(new WebContentLoader()), new UrlConverter())),
                new SitemapParser(new XmlParser(), new WebContentLoader(), new UrlConverter()),
                new HtmlResponseTracker());

            _repositoryService = new RepositoryService(_mockIRepositoryService.Object);
            _crawlerService = new CrawlerService(_repositoryService, _mockWebCrawler.Object);
        }

        [Test, Timeout(1000)]
        public void ParseUrlAndSaveResultAsync_InputUrl_ShouldCallParseUrlAndGetResponseTimeListOnce()
        {
            var fakeUrlList = new List<ParsedUrl>
            {
                new ParsedUrl{Url = "http://www.example.com/"},
                new ParsedUrl{Url = "http://www.example.com/Home/"},
                new ParsedUrl{Url = "http://www.example.com/About/"}
            }.AsEnumerable();

            _mockWebCrawler.Setup(x => x.ParseUrl(It.IsAny<string>()))
                .Returns(fakeUrlList);

            _mockWebCrawler.Setup(x => x.GetResponseTimeList(It.IsAny<IEnumerable<ParsedUrl>>()))
                .Returns(new List<ResponseParsedUrl>().AsEnumerable());


            _crawlerService.ParseUrlAndSaveResultAsync("fake");

            _mockWebCrawler.Verify(x => x.ParseUrl(It.IsAny<string>()), Times.Once);
            _mockWebCrawler.Verify(x => x.GetResponseTimeList(It.IsAny<IEnumerable<ParsedUrl>>()), Times.Once);
        }

        [Test, Timeout(1000)]
        public void GetAllResult_ShouldReturnCollectionCrawlingResult()
        {
            var fakeCollection = new List<CrawlingResult>
            {
                new CrawlingResult {Id = 1, BasePage = "page1"},
                new CrawlingResult {Id = 2, BasePage = "page2"},
                new CrawlingResult {Id = 3, BasePage = "page3"},
            }.AsQueryable();

            _mockIRepositoryService.Setup(x => x.Include(x => x.Pages))
                .Returns(fakeCollection);

            var result = _crawlerService.GetAllResult();

            Assert.AreEqual(fakeCollection.Select(x => x.Id), result.Select(x => x.Id));
            Assert.AreEqual(fakeCollection.Select(x => x.BasePage), result.Select(x => x.BasePage));
        }

        [Test, Timeout(1000)]
        public void GetResultById_InputId_ShouldReturnCrawlingResult()
        {
            var fakeCollection = new List<CrawlingResult>
            {
                new CrawlingResult
                {
                    Id = 1, 
                    BasePage = "page1"
                },
                new CrawlingResult
                {
                    Id = 2,
                    BasePage = "page2"
                }
            }.AsQueryable();

            var id = 2;

            var expectedResult = new CrawlingResult { Id = 2, BasePage = "page2" };

            _mockIRepositoryService.Setup(x => x.Include(x => x.Pages))
                .Returns(fakeCollection);

            var result = _crawlerService.GetResultById(id);

            Assert.AreEqual(expectedResult.Id, result.Id);
            Assert.AreEqual(expectedResult.BasePage, result.BasePage);
        }

        [Test, Timeout(1000)]
        public void GetResultById_InputNotExistingId_ResultShouldBeNull()
        {
            var fakeCollection = new List<CrawlingResult>
            {
                new CrawlingResult
                {
                    Id = 1,
                    BasePage = "page1"
                },
                new CrawlingResult
                {
                    Id = 2,
                    BasePage = "page2"
                }
            }.AsQueryable();

            var id = 3;

            _mockIRepositoryService.Setup(x => x.Include(x => x.Pages))
                .Returns(fakeCollection);

            var result = _crawlerService.GetResultById(id);

            Assert.Null(result);
        }
    }
}
