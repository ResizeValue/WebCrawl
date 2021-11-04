using System.Collections.Generic;
using System.Linq;
using WebCrawl;
using WebCrawl.Crawler;
using WebCrawl.Models;

namespace WebCrawler
{
    public class WebCrawler
    {
        private readonly HtmlParser _htmlParser;
        private readonly SitemapParser _sitemapParser;
        private readonly HtmlResponseTracker htmlResposeTracker;

        public WebCrawler(HtmlParser urlParser, SitemapParser sitemapParser)
        {
            htmlResposeTracker = new HtmlResponseTracker();
            _htmlParser = urlParser;
            _sitemapParser = sitemapParser;
        }

        public virtual List<ParsedUrl> ParseUrl(string url)
        {
            var crawlUrlsList = _htmlParser.ParseUrl(url);
            var sitemapUrlsList = _sitemapParser.ParseSitemap(url + "sitemap.xml");

            var allUrls = crawlUrlsList.Union(sitemapUrlsList).Distinct();

            return (from stringUrl in allUrls
                    select new ParsedUrl
                    {
                        Url = stringUrl,
                        IsSitemapUrl = sitemapUrlsList.Contains(stringUrl),
                        IsCrawlerUrl = crawlUrlsList.Contains(stringUrl)
                    }).ToList();
        }

        public List<ResponseParsedUrl> GetResponseTimeList(List<ParsedUrl> parsedUrls)
        {
            return (from url in parsedUrls
                    where url.IsCrawlerUrl == true
                    select new ResponseParsedUrl
                    {
                        Url = url.Url,
                        ResponseTime = htmlResposeTracker.CheckResponseTime(url.Url)
                    }).ToList();
        }
    }
}
