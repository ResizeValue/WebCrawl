using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebCrawl.Entity.Models;
using WebCrawl.Logic.Models;

namespace WebCrawl.Logic
{
    public class RepositoryService
    {
        private readonly IRepository<CrawlingResult> _repository;

        public RepositoryService(IRepository<CrawlingResult> repository)
        {
            _repository = repository;
        }

        public async Task SaveResultAsync(string baseUrl, IEnumerable<ResponseParsedUrl> urls)
        {
            var result = new CrawlingResult
            {
                BasePage = baseUrl,
                Date = DateTime.Now,
                Pages = urls.Select(x => new CheckedPage { Url = x.Url, ResponseTime = x.ResponseTime }).ToArray() 
            };

            await _repository.AddAsync(result);

            await _repository.SaveChangesAsync();
        }
    }
}
