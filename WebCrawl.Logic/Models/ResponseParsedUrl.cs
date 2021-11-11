using System;

namespace WebCrawl.Logic.Models
{
    public class ResponseParsedUrl
    {
        public string Url { get; set; }
        public TimeSpan ResponseTime { get; set; }
    }
}
