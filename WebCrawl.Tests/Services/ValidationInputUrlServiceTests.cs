using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebCrawl.Logic.Services;

namespace WebCrawl.Logic.Tests.Services
{
    public class ValidationInputUrlServiceTests
    {
        private readonly Mock<WebContentLoader> _mockContentLoader;
        private readonly ValidationInputUrlService _validationInputUrlService;

        public ValidationInputUrlServiceTests()
        {
            _mockContentLoader = new Mock<WebContentLoader>();

            _validationInputUrlService = new ValidationInputUrlService(_mockContentLoader.Object);
        }

        [Test, Timeout(1000)]
        public void IsValidInputUrl_InputInvalidUrl_ReturnErrorIncorrectStringFormat()
        {
            string fakeUrl = "https://example.com/#1";
            string errorMessage = "Incorrect string format";

            var actual = _validationInputUrlService.IsValidInputUrl(ref fakeUrl);

            Assert.IsFalse(actual);
            Assert.AreEqual(errorMessage, _validationInputUrlService.ErrorMessage);
        }

        [Test, Timeout(1000)]
        public void IsValidInputUrl_InputNotExistUrl_ReturnErrorSiteNotFound()
        {
            string fakeUrl = "https://notexample.com/";
            string errorMessage = "The site was not found";

            _mockContentLoader.Setup(x => x.TryDownloadContent(It.IsAny<string>()))
                .Returns(false);

            var actual = _validationInputUrlService.IsValidInputUrl(ref fakeUrl);

            Assert.IsFalse(actual);
            Assert.AreEqual(errorMessage, _validationInputUrlService.ErrorMessage);
        }

        [Test, Timeout(1000)]
        public void IsValidInputUrl_InputCorrectUrl_ReturnTrue()
        {
            string fakeUrl = "https://notexample.com/";

            _mockContentLoader.Setup(x => x.TryDownloadContent(It.IsAny<string>()))
                .Returns(true);

            var actual = _validationInputUrlService.IsValidInputUrl(ref fakeUrl);

            Assert.IsTrue(actual);
            Assert.Null(_validationInputUrlService.ErrorMessage);
        }
    }
}
