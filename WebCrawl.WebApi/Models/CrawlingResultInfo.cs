using System;

namespace WebCrawl.WebApi.Models
{
    public class CrawlingResultInfo
    {
        public int Id { get; set; }

        public string BasePage { get; set; }

        public DateTime Date { get; set; }

        public int PagesCount { get; set; }
    }
}
