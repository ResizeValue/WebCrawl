using System;

namespace WebCrawl.Entity.Models
{
    public class ParsedHtmlDocument
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsSitemapLink { get; set; }

        public bool IsCrawlingLink { get; set; }

        public TimeSpan ResponseTime { get; set; }
    }
}
