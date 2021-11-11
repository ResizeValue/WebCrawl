using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCrawl.Entity.Models;
using WebCrawl.Logic.Crawler;

namespace WebCrawl.Logic.Services
{
    public class CrawlerService
    {
        private readonly ReferenceValidation _validation;
        private readonly RepositoryService _service;
        private readonly WebCrawler _crawler;
        public CrawlerService(RepositoryService service, ReferenceValidation validation, WebCrawler crawler)
        {
            _validation = validation;
            _service = service;
            _crawler = crawler;
        }

        public  virtual IEnumerable<CrawlingResult> GetAllResult()
        {
            return _service.GetAllResults();
        }

        public async virtual Task ParseUrlAndSaveResultAsync(string url)
        {
            var response = _crawler.ParseUrl(url);

            var responseTime = _crawler.GetResponseTimeList(response);

            await _service.SaveResultAsync(url, responseTime);
        }
    }
}
