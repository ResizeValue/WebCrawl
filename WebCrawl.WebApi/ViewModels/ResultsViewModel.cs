using System.Collections.Generic;
using WebCrawl.WebApi.Models;

namespace WebCrawl.WebApi.ViewModels
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
