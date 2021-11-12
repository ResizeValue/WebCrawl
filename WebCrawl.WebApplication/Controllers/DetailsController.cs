using Microsoft.AspNetCore.Mvc;
using WebCrawl.Logic.Services;
using WebCrawl.WebApplication.Services;

namespace WebCrawl.WebApplication.Controllers
{
    public class DetailsController : Controller
    {
        private readonly CrawlerService _service;
        private readonly ViewMapperService _viewMapper;

        public DetailsController(CrawlerService service, ViewMapperService viewMapper)
        {
            _service = service;
            _viewMapper = viewMapper;
        }

        [HttpGet]
        public ActionResult Index(int id)
        {
            var result = _service.GetResultById(id);

            var detailModel = _viewMapper.GetDetailsViewModel(result);

            return View(detailModel);
        }
    }
}
