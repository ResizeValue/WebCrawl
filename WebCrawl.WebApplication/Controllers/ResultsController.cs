using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCrawl.Logic.Services;
using WebCrawl.WebApplication.Models;
using WebCrawl.WebApplication.Services;
using WebCrawl.WebApplication.ViewModels;

namespace WebCrawl.WebApplication.Controllers
{
    public class ResultsController : Controller
    {
        private readonly CrawlerService _service;
        private readonly ValidationInputUrlService _validation;
        private readonly ViewMapperService _viewMapper;

        private const int maxPagesCount = 7;
        private const int basePageSize = 5;

        public ResultsController(CrawlerService service,
            ViewMapperService viewMapper,
            ValidationInputUrlService validation)
        {
            _service = service;
            _validation = validation;
            _viewMapper = viewMapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int curPage = 1, int pageSize = basePageSize, int maxPages = maxPagesCount)
        {
            var page = _viewMapper.GetPageTemplate(pageSize, curPage, maxPages);

            var resultsForPage = await _service.GetResultsPage(curPage, pageSize);

            var resultsModel = _viewMapper.GetResultsViewModel(resultsForPage.Results, page, resultsForPage.TotalCount);

            return View(resultsModel);
        }

        [HttpPost]
        public async Task<IActionResult> ParseSiteByUrl(ResultsViewModel model)
        {
            if (_validation.IsValidInputUrl(model.InputUrl))
            {
                await _service.ParseUrlAndSaveResultAsync(model.InputUrl);
            }
            else
            {
                ModelState.AddModelError("IncorrectUrl", _validation.ErrorMessage);
            }

            var page = _viewMapper.GetPageTemplate(basePageSize, model.CurrentPage, maxPagesCount);

            var resultsForPage = await _service.GetResultsPage(model.CurrentPage, basePageSize);

            var resultModel = _viewMapper.GetResultsViewModel(resultsForPage.Results, page, resultsForPage.TotalCount, model.InputUrl);

            return View("Index", resultModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
