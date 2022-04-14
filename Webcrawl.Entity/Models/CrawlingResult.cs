using System;
using System.Collections.Generic;

namespace WebCrawl.Entity.Models
{
    public class CrawlingResult
    {
        public int Id { get; set; }

        public string BasePage { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<ParsedHtmlDocument> Pages { get; set; }
    }
}
