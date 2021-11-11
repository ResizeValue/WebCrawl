using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawl.Entity.Models;
using WebCrawl.WebApplication.Models;

namespace WebCrawl.WebApplication.viewModels
{
    public class ResultsViewModel
    {
        public ICollection<CrawlingResult> Results { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int MaxPages { get; set; }
        public Input Input { get; set; }
    }
}
