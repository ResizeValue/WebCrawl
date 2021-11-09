using System;
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
        private readonly HtmlResponseTracker _htmlResponseTracker;

        public WebCrawler(HtmlCrawler urlParser, SitemapParser sitemapParser)
        {
            _htmlResponseTracker = new HtmlResponseTracker();
            _htmlParser = urlParser;
            _sitemapParser = sitemapParser;
        }

        public virtual IEnumerable<ParsedUrl> ParseUrl(string url)
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
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.Message);
            }

            var allUrls = crawlUrlsList.Union(sitemapUrlsList).Distinct();

            var parsedUrlCollection = allUrls.Select(stringUrl => new ParsedUrl
            {
                Url = stringUrl,
                IsSitemapUrl = sitemapUrlsList.Contains(stringUrl),
                IsCrawlerUrl = crawlUrlsList.Contains(stringUrl)
            });

            return parsedUrlCollection;
        }

        public IEnumerable<ResponseParsedUrl> GetResponseTimeList(IEnumerable<ParsedUrl> parsedUrls)
        {
            var responseTimeCollection = parsedUrls.Where(x => x.IsCrawlerUrl)
                .Select(stringUrl => new ResponseParsedUrl
                {
                    Url = stringUrl.Url,
                    ResponseTime = _htmlResponseTracker.CheckResponseTime(stringUrl.Url)
                }).ToArray();

            return responseTimeCollection;
        }
    }
}
