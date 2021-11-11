using System;
using System.Collections.Generic;
using System.Linq;
using WebCrawl.Entity.Models;
using WebCrawl.WebApplication.Models;
using WebCrawl.WebApplication.viewModels;

namespace WebCrawl.WebApplication.Services
{
    public class ViewMapperService
    {
        public ResultsViewModel GetResultsViewModel(IEnumerable<CrawlingResult> results, PageTemplate page, string? inputUrl)
        {
            return new ResultsViewModel
            {
                CurrentPage = page.CurrentPage,
                MaxPages = page.MaxPages,
                InputUrl = inputUrl,
                TotalPages = (int)Math.Ceiling((double)results.Count() / page.PageSize),
                Results = results.Skip((page.CurrentPage - 1) * page.PageSize).Take(page.PageSize).ToArray()
            };
        }
    }
}
