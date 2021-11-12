using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawl.WebApplication.Models
{
    public class CrawlingResultInfo
    {
        public int Id { get; set; }

        public string BasePage { get; set; }

        public DateTime Date { get; set; }

        public int PagesCount { get; set; }
    }
}
