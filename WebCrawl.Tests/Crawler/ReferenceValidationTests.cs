using NUnit.Framework;
using WebCrawl.Logic.Crawler;

namespace WebCrawl.Logic.Tests.Crawler
{
    public class ReferenceValidationTests
    {
        private readonly ReferenceValidation validationTests;

        public ReferenceValidationTests()
        {
            validationTests = new ReferenceValidation(new WebContentLoader());
        }

        [Test, Timeout(1000)]
        public void IsCorrectUrl_InputEmptyString_ReturnFalse()
        {
            var fakeUrl = "";

            var result = validationTests.IsCorrectUrl(fakeUrl);

            Assert.IsFalse(result);
        }

        [Test, Timeout(1000)]
        public void IsCorrectUrl_InputNull_ReturnFalse()
        {
            var result = validationTests.IsCorrectUrl(null);

            Assert.IsFalse(result);
        }
        
        [Test, Timeout(1000)]
        [TestCase("user@gmail.com")]
        [TestCase("https://example.com/#id")]
        [TestCase("https://example.com/id?=1")]
        [TestCase("https://example.com/document.xml")]
        [TestCase("https://example.com/javascript:setStyle('style')")]
        public void IsCorrectUrl_InputIncorrectString_ReturnFalse(string url)
        {
            var result = validationTests.IsCorrectUrl(url);

            Assert.IsFalse(result);
        }
    }
}
