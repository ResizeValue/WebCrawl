using System.Threading.Tasks;
using WebCrawl.Logic.Services;
using WebCrawl.WebApi.ViewModels;

namespace WebCrawl.WebApi.Services
{
    public class ViewModelsService
    {
        private readonly CrawlerService _crawlerService;
        private readonly ViewMapperService _viewMapper;
        public ViewModelsService(CrawlerService crawlerService, ViewMapperService viewMapper)
        {
            _crawlerService = crawlerService;
            _viewMapper = viewMapper;
        }

        public async Task<ResultsViewModel> GetResultsModel(int curPage, int pageSize, int maxPages)
        {
            var page = _viewMapper.GetPageTemplate(pageSize, curPage, maxPages);
            var resultsForPage = await _crawlerService.GetResultsPage(curPage, pageSize);
            return _viewMapper.GetResultsViewModel(resultsForPage.Results, page, resultsForPage.TotalCount);
        }
    }
}
