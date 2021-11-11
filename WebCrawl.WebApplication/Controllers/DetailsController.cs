using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebCrawl.Logic.Services;
using WebCrawl.WebApplication.viewModels;

namespace WebCrawl.WebApplication.Controllers
{
    public class DetailsController : Controller
    {
        private readonly CrawlerService _service;

        public DetailsController(CrawlerService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index(int id)
        {
            var result = _service.GetResultById(id);

            var detailModel = new DetailsViewModel
            {
                Id = id,
                BaseUrl = result.BasePage,
                Pages = result.Pages.ToArray()
            };

            return View(detailModel);
        }
    }
}
