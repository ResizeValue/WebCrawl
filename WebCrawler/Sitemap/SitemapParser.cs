using System.Collections.Generic;
using WebCrawl.Sitemap;

namespace WebCrawl
{
    public class SitemapParser
    {
        private readonly XmlParser _xmlParser;
        private readonly WebLoader _sitemaploader;

        public SitemapParser(XmlParser xmlParser, WebLoader sitemaploader)
        {
            _xmlParser = xmlParser;
            _sitemaploader = sitemaploader;
        }

        public virtual List<string> ParseSitemap(string url)
        {
            var downloadedString = _sitemaploader.DownloadString(url);

            return _xmlParser.ParseXml(downloadedString);
        }
    }
}
