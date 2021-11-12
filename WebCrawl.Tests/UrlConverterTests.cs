using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawl.Logic.Tests
{
    public class UrlConverterTests
    {
        private readonly UrlConverter _urlConverter;

        public UrlConverterTests()
        {
            _urlConverter = new UrlConverter();
        }

        [Test, Timeout(1000)]
        [TestCase("Home/", "https://example.com/")]
        [TestCase("/Home/", "https://example.com/")]
        [TestCase("https://example.com/Home/", "https://example.com/")]
        public void CreateAbsoluteUrl_InputUrlAndBaseAddress_ShouldReturnAbsoluteUrl(string url, string baseUrl)
        {
            var expected = "https://example.com/Home/";

            var result = _urlConverter.CreateAbsoluteUrl(url, baseUrl);

            Assert.AreEqual(expected, result);
        }
    }
}
