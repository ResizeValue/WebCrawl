using System;

namespace WebCrawl.Entity.Models
{
    public class ParsedHtmlDocument
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public TimeSpan ResponseTime { get; set; }
    }
}
