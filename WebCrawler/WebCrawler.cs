using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebCrawl.Logic.Crawler;
using WebCrawl.Logic.Models;
using WebCrawl.Logic.Sitemap;

namespace WebCrawl.Logic
{
    public class WebCrawler
    {
        private readonly HtmlCrawler _htmlParser;
        private readonly SitemapParser _sitemapParser;
        private readonly HtmlResponseTracker _htmlResposeTracker;

        public WebCrawler(HtmlCrawler urlParser, SitemapParser sitemapParser)
        {
            _htmlResposeTracker = new HtmlResponseTracker();
            _htmlParser = urlParser;
            _sitemapParser = sitemapParser;
        }

        public virtual List<ParsedUrl> ParseUrl(string url)
        {
            var crawlUrlsList = _htmlParser.ParseHtmlDocuments(url);

            List<string> sitemapUrlsList = new List<string>();
            try
            {
                sitemapUrlsList = _sitemapParser.ParseSitemap(url + "sitemap.xml");
            }
            catch (WebException exception)
            {
                System.Console.WriteLine(exception.Message);
            }

            var allUrls = crawlUrlsList.Union(sitemapUrlsList).Distinct();

            return (from stringUrl in allUrls
                    select new ParsedUrl
                    {
                        Url = stringUrl,
                        IsSitemapUrl = sitemapUrlsList.Contains(stringUrl),
                        IsCrawlerUrl = crawlUrlsList.Contains(stringUrl)
                    }).ToList();
        }

        public IEnumerable<ResponseParsedUrl> GetResponseTimeList(IEnumerable<ParsedUrl> parsedUrls)
        {
            return (from url in parsedUrls
                    where url.IsCrawlerUrl == true
                    select new ResponseParsedUrl
                    {
                        Url = url.Url,
                        ResponseTime = _htmlResposeTracker.CheckResponseTime(url.Url)
                    }).ToList();
        }
    }
}
