using System;

namespace WebCrawl.WebApi.Models
{
    public class ParsedPageInfo
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsSitemapLink { get; set; }

        public bool IsCrawlingLink { get; set; }

        public TimeSpan ResponseTime { get; set; }
    }
}
