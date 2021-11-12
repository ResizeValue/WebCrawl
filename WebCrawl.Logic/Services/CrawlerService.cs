using System.Collections.Generic;
using System.Threading.Tasks;
using WebCrawl.Entity.Models;

namespace WebCrawl.Logic.Services
{
    public class CrawlerService
    {
        private readonly RepositoryService _service;
        private readonly WebCrawler _crawler;
        public CrawlerService(RepositoryService service, WebCrawler crawler)
        {
            _service = service;
            _crawler = crawler;
        }

        public virtual IEnumerable<CrawlingResult> GetAllResult()
        {
            return _service.GetAllResults();
        }

        public virtual CrawlingResult GetResultById(int id)
        {
            return _service.GetResultById(id);
        }

        public async virtual Task<(int TotalCount, IList<CrawlingResult> Results)> GetResultsPage(int curPage, int pageSize)
        {
            return await _service.GetResultsPageAsync(curPage, pageSize);
        }

        public async virtual Task ParseUrlAndSaveResultAsync(string url)
        {
            var response = _crawler.ParseUrl(url);

            var responseTimeList = _crawler.GetResponseTimeList(response);

            await _service.SaveResultAsync(url, responseTimeList);
        }
    }
}
