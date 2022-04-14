using System.Collections.Generic;
using WebCrawl.WebApi.Models;

namespace WebCrawl.WebApi.ViewModels
{
    public class DetailsViewModel
    {
        public string BaseUrl { get; set; }
        public IEnumerable<ParsedPageInfo> Pages { get; set; }
    }
}
