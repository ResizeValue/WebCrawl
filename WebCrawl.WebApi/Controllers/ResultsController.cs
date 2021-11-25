using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCrawl.Logic.Services;
using WebCrawl.WebApplication.Services;

namespace WebCrawl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ViewModelsService _modelService;
        private readonly CrawlerService _crawlerService;
        private readonly ValidationInputUrlService _validation;

        public ResultsController(ViewModelsService modelService,
            CrawlerService crawlerService,
            ValidationInputUrlService validation)
        {
            _modelService = modelService;
            _crawlerService = crawlerService;
            _validation = validation;
        }

        [HttpGet]
        public async Task<IActionResult> Results(int curPage, int pageSize, int maxPages)
        {
            var resultsModel = await _modelService.GetResultsModel(curPage, pageSize, maxPages);

            return Ok(resultsModel);
        }

        [HttpPost]
        public async Task<IActionResult> ParseSiteByUrl(string inputUrl)
        {
            if (_validation.IsValidInputUrl(ref inputUrl))
            {
                await _crawlerService.ParseUrlAndSaveResultAsync(inputUrl);

                return Ok(ModelState);
            }
            else
            {
                ModelState.AddModelError("IncorrectUrl", _validation.ErrorMessage);

                return BadRequest(ModelState);
            }
        }
    }
}
