using System;
using System.Collections.Generic;
using System.Linq;
using WebCrawl.Entity.Models;
using WebCrawl.WebApi.Models;
using WebCrawl.WebApi.ViewModels;

namespace WebCrawl.WebApi.Services
{
    public class ViewMapperService
    {
        public ResultsViewModel GetResultsViewModel(IEnumerable<CrawlingResult> results, PageTemplate page, int totalCount, string inputUrl = "")
        {
            return new ResultsViewModel
            {
                CurrentPage = page.CurrentPage,
                MaxPages = page.MaxPages,
                InputUrl = inputUrl,
                TotalPages = (int)Math.Ceiling((double)totalCount / page.PageSize),
                Results = results.Select(x => new CrawlingResultInfo
                {
                    Id = x.Id,
                    BasePage = x.BasePage,
                    Date = x.Date,
                    PagesCount = x.Pages.Count
                })
            };
        }

        public DetailsViewModel GetDetailsViewModel(CrawlingResult result)
        {
            return new DetailsViewModel
            {
                Pages = result.Pages.Select(x => new ParsedPageInfo
                {
                    Id = x.Id,
                    Url = x.Url,
                    IsSitemapLink = x.IsSitemapLink,
                    IsCrawlingLink = x.IsCrawlingLink,
                    ResponseTime = x.ResponseTime
                }),
                BaseUrl = result.BasePage
            };
        }

        public PageTemplate GetPageTemplate(int pageSize, int curPage, int maxPages)
        {
            return new PageTemplate
            {
                PageSize = pageSize,
                CurrentPage = curPage,
                MaxPages = maxPages
            };
        }

        public CrawlingResultInfo MapToCrawlingResultInfo(CrawlingResult crawlingResult)
        {
            return new CrawlingResultInfo
            {
                Id = crawlingResult.Id,
                BasePage = crawlingResult.BasePage,
                Date = crawlingResult.Date,
                PagesCount = crawlingResult.Pages.Count
            };
        }
    }
}
