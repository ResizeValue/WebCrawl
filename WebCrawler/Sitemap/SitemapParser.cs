using System.Collections.Generic;

namespace WebCrawl.Logic.Sitemap
{
    public class SitemapParser
    {
        private readonly XmlParser _xmlParser;
        private readonly WebContentLoader _webLoader;

        public SitemapParser(XmlParser xmlParser, WebContentLoader webLoader)
        {
            _xmlParser = xmlParser;
            _webLoader = webLoader;
        }

        public virtual List<string> ParseSitemap(string url)
        {
            var downloadedString = _webLoader.DownloadContent(url);

            return _xmlParser.ParseXmlString(downloadedString);
        }
    }
}
