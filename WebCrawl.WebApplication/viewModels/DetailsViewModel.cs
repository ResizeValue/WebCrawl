using System.Collections.Generic;
using WebCrawl.WebApplication.Models;

namespace WebCrawl.WebApplication.ViewModels
{
    public class DetailsViewModel
    {
        public string BaseUrl { get; set; }
        public IEnumerable<ParsedPageInfo> Pages { get; set; }
    }
}
