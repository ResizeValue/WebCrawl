using NUnit.Framework;
using System.Collections.Generic;
using WebCrawl.Logic.Sitemap;

namespace WebCrawl.Logic.Tests.Sitemap
{
    public class XmlParserTests
    {
        private readonly XmlParser xmlParser;

        public XmlParserTests()
        {
            xmlParser = new XmlParser();
        }

        [Test, Timeout(1000)]
        public void ParseXmlDocument_InputNull_ShouldReturnEmptyCollection()
        {
            var result = xmlParser.ParseXmlString(null);

            Assert.AreEqual(new List<string>(), result);
        }

        [Test, Timeout(1000)]
        public void ParseXmlDocument_InputEmptyString_ShouldReturnEmptyCollection()
        {
            var result = xmlParser.ParseXmlString("");

            Assert.AreEqual(new List<string>(), result);
        }

        [Test, Timeout(1000)]
        public void ParseXmlDocument_InputXmlString_ShouldReturnUrlsCollection()
        {
            var inputXml = @"<urlset>
                                <url>
                                    <loc>https://example.com/</loc>
                                    <changefreq>monthly</changefreq>
                                    <priority >1.00</priority>
                                </url>
                                <url>
                                    <loc>https://example.com/Home/</loc>
                                    <changefreq>monthly</changefreq>
                                    <priority>0.80 </priority>
                                </url>
                                <url>
                                    <loc></loc>
                                </url>
                             </urlset>";

            var expectedList = new List<string>
            {
                "https://example.com/",
                "https://example.com/Home/"
            };

            var result = xmlParser.ParseXmlString(inputXml);

            Assert.AreEqual(expectedList, result);
        }
    }
}
