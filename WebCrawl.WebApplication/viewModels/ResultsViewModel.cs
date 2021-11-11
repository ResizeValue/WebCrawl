using System.Collections.Generic;
using WebCrawl.Entity.Models;

namespace WebCrawl.WebApplication.viewModels
{
    public class ResultsViewModel
    {
        public ICollection<CrawlingResult> Results { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int MaxPages { get; set; }
        public string InputUrl { get; set; }
    }
}
