using System.Collections.Generic;

namespace WebCrawl.Logic.Sitemap
{
    public class SitemapParser
    {
        private readonly XmlParser _xmlParser;
        private readonly UrlConverter _converter;
        private readonly WebContentLoader _webLoader;

        public SitemapParser(XmlParser xmlParser, WebContentLoader webLoader, UrlConverter converter)
        {
            _xmlParser = xmlParser;
            _webLoader = webLoader;
            _converter = converter;
        }

        public virtual List<string> ParseSitemap(string url)
        {
            var sitemapUrl = url + "sitemap.xml";

            var downloadedString = _webLoader.DownloadContent(sitemapUrl);

            var urlList = _xmlParser.ParseXmlString(downloadedString);

            for (int i = 0; i < urlList.Count; i++)
            {
                urlList[i] = _converter.CreateAbsoluteUrl(urlList[i], url);
            }

            return urlList;
        }
    }
}
