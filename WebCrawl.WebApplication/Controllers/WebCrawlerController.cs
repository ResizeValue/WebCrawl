using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCrawl.Logic;
using WebCrawl.Logic.Services;
using WebCrawl.WebApplication.Models;
using WebCrawl.WebApplication.viewModels;

namespace WebCrawl.WebApplication.Controllers
{
    public class WebCrawlerController : Controller
    {
        private readonly ILogger<WebCrawlerController> _logger;
        private readonly CrawlerService _service;
        private readonly ValidationInputUrlService _validation;

        public WebCrawlerController(ILogger<WebCrawlerController> logger, CrawlerService service, ValidationInputUrlService validation)
        {
            _logger = logger;
            _service = service;
            _validation = validation;
        }

        [HttpGet]
        public IActionResult Index(int curPage = 1, int pageSize = 5, int maxPages = 7)
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
        public async Task<IActionResult> ParseSiteByUrl(Input input)
        {
            if (_validation.IsValidInputUrl(input.Url))
            {
                await _service.ParseUrlAndSaveResultAsync(input.Url);
            }
            else
            {
                ModelState.AddModelError("IncorrectUrl", _validation.ErrorMessage);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
