using System;
using Moq;
using NUnit.Framework;

namespace WebCrawl.ConsoleApplication.Tests
{
    public class ConsoleApplicationTests
    {
        private readonly Mock<ConsoleWebCrawler> mockConsoleCrawler;
        private readonly Mock<WebCrawler.WebCrawler> mockCrawler;
        private readonly Mock<ConsoleWrapper> mockWrapper;
        public ConsoleApplicationTests()
        {
            mockCrawler = new Mock<WebCrawler.WebCrawler>();
            mockWrapper = new Mock<ConsoleWrapper>();
            mockConsoleCrawler = new Mock<ConsoleWebCrawler>(mockCrawler.Object, mockWrapper.Object);
        }
        [Test, Timeout(1000)]
        public void Test1()
        {

        }
    }
}
