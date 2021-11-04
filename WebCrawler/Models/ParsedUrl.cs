using System;

namespace WebCrawl
{
    public class ParsedUrl
    {
        public string Url { get; set; }
        public TimeSpan ResponseTime { get; set; }
        public bool IsSitemapUrl { get; set; }
        public bool IsCrawlerUrl { get; set; }
    }
}
