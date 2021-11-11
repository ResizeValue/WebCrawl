using System.Collections.Generic;
using WebCrawl.Entity.Models;

namespace WebCrawl.WebApplication.viewModels
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string BaseUrl { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int MaxPages { get; set; }
        public ICollection<ParsedHtmlDocument> Pages { get; set; }
    }
}
