using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawl.Logic;
using WebCrawl.Logic.Services;
using WebCrawl.WebApplication.viewModels;

namespace WebCrawl.WebApplication.Controllers
{
    public class DetailsController : Controller
    {
        private readonly RepositoryService _service;
        public DetailsController(RepositoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult Index(int id, int curPage = 1, int pageSize = 10, int maxPages = 7)
        {
            var result = _service.GetResultById(id);

            var detailModel = new DetailsViewModel
            {
                Id = id,
                BaseUrl = result.BasePage,
                Pages = result.Pages.Skip((curPage-1) * pageSize).Take(pageSize).ToArray(),
                CurrentPage = curPage,
                MaxPages = maxPages,
                TotalPages = (int)Math.Ceiling((double)result.Pages.Count / pageSize)
            };

            return View(detailModel);
        }
    }
}
