using System;
using System.Collections.Generic;
using System.Net;

namespace WebCrawl.Sitemap
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
            try
            {
                var downloadedString = _webLoader.DownloadContent(url);

                return _xmlParser.ParseXmlString(downloadedString);
            }
            catch (WebException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
