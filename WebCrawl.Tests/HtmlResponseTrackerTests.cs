using Moq;
using NUnit.Framework;
using System;

namespace WebCrawl.Logic.Tests
{
    class HtmlResponseTrackerTests
    {
        private readonly Mock<WebContentLoader> _mockWebLoader;
        private readonly HtmlResponseTracker _mockHtmlResponseTracker;

        public HtmlResponseTrackerTests()
        {
            _mockWebLoader = new Mock<WebContentLoader>();
            _mockHtmlResponseTracker = new HtmlResponseTracker();
        }

        [Test, Timeout(1000)]
        public void CheckResponseTime_InputUrl_ShouldReturnResponseTimes()
        {
            var fakeUrl = "https://example.com/";

            _mockWebLoader.Setup(mock => mock.DownloadContent(It.IsAny<string>()))
                .Returns("ResultString");

            var resultTime = _mockHtmlResponseTracker.CheckResponseTime(fakeUrl);

            Assert.Less(resultTime, TimeSpan.FromSeconds(1));
        }
    }
}
