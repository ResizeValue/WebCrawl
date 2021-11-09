using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawl.Entity.Models;
using WebCrawl.Logic.Models;
using WebCrawl.Repository;

namespace WebCrawl.Logic
{
    public class DatabaesManager
    {
        private readonly ReusltsRepository _repository;

        public DatabaesManager(ReusltsRepository repository)
        {
            _repository = repository;
        }

        public async Task<CrawlingResult> GetResultById(int id)
        {
            return await _repository.GetResultById(id);
        }

        public async Task<CrawlingResult> GetResultByBaseUrl(string baseUrl)
        {
            return await _repository.GetResultByBaseUrl(baseUrl);
        }

        public async Task SaveResultAsync(string baseUrl, IEnumerable<ResponseParsedUrl> urls)
        {
            var result = new CrawlingResult
            {
                BasePage = baseUrl,
                Date = DateTime.Now,
                Pages = urls.Select(x => new CheckedPage { Url = x.Url, ResponseTime = x.ResponseTime }).ToArray()
            };

            await _repository.AddResultAsync(result);
        }
    }
}
