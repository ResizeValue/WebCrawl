using System.Collections.Generic;
using WebCrawl.WebApplication.Models;

namespace WebCrawl.WebApplication.ViewModels
{
    public class ResultsViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int MaxPages { get; set; }
        public string InputUrl { get; set; }
        public IEnumerable<CrawlingResultInfo> Results { get; set; }
    }
}
