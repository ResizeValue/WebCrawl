using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCrawl.Logic.Services;
using WebCrawl.WebApplication.Models;
using WebCrawl.WebApplication.Services;
using WebCrawl.WebApplication.viewModels;

namespace WebCrawl.WebApplication.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ILogger<ResultsController> _logger;
        private readonly CrawlerService _service;
        private readonly ValidationInputUrlService _validation;
        private readonly ViewMapperService _viewMapper;

        private const int maxPagesCount = 7;
        private const int basePageSize = 5;

        public ResultsController(ILogger<ResultsController> logger,
            CrawlerService service,
            ViewMapperService viewMapper,
            ValidationInputUrlService validation)
        {
            _logger = logger;
            _service = service;
            _validation = validation;
            _viewMapper = viewMapper;
        }

        [HttpGet]
        public IActionResult Index(int curPage = 1, int pageSize = basePageSize, int maxPages = maxPagesCount)
        {
            var results = _service.GetAllResult();

            var resultsModel = new ResultsViewModel
            {
                Results = results.Skip((curPage - 1) * pageSize).Take(pageSize).ToArray(),
                CurrentPage = curPage,
                MaxPages = maxPages,
                TotalPages = (int)Math.Ceiling((double)results.Count() / pageSize)
            };

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

            var page = new PageTemplate
            {
                CurrentPage = model.CurrentPage,
                MaxPages = maxPagesCount,
                PageSize = basePageSize
            };

            var resultModel = _viewMapper.GetResultsViewModel(_service.GetAllResult(), page, model.InputUrl);

            return View("Index", resultModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
