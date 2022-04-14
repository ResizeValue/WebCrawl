using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebCrawl.Logic.Services;
using WebCrawl.WebApplication.Models;
using WebCrawl.WebApplication.Services;
using WebCrawl.WebApplication.ViewModels;

namespace WebCrawl.WebApplication.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ViewModelsService _modelService;
        private readonly CrawlerService _crawlerService;
        private readonly ValidationInputUrlService _validation;

        private const int MaxPagesCount = 7;
        private const int BasePageSize = 5;

        public ResultsController(ViewModelsService modelService,
            CrawlerService crawlerService,
            ValidationInputUrlService validation)
        {
            _modelService = modelService;
            _crawlerService = crawlerService;
            _validation = validation;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int curPage = 1, int pageSize = BasePageSize, int maxPages = MaxPagesCount)
        {
            var resultsModel = await _modelService.GetResultsModel(curPage, pageSize, maxPages);

            return View(resultsModel);
        }

        [HttpPost]
        public async Task<IActionResult> ParseSiteByUrl(ResultsViewModel model)
        {
            var inputUrl = model.InputUrl;

            if (_validation.IsValidInputUrl(ref inputUrl))
            {
                await _crawlerService.ParseUrlAndSaveResultAsync(model.InputUrl);
            }
            else
            {
                ModelState.AddModelError("IncorrectUrl", _validation.ErrorMessage);
            }

            var resultsModel = await _modelService.GetResultsModel(model.CurrentPage, BasePageSize, MaxPagesCount);

            return View("Index", resultsModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
