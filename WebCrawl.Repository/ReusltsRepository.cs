using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebCrawl.Entity.Models;

namespace WebCrawl.Repository
{
    public class ReusltsRepository
    {
        private readonly IRepository<CrawlingResult> _repository;

        public ReusltsRepository(IRepository<CrawlingResult> repository)
        {
            _repository = repository;
        }

        public async Task<CrawlingResult> GetResultById(int id)
        {
            return _repository.Include(x => x.Pages)
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<CrawlingResult> GetResultByBaseUrl(string baseUrl)
        {
            return _repository.Include(x => x.Pages)
                .FirstOrDefault(x => x.BasePage == baseUrl);
        }

        public IEnumerable<CrawlingResult> GetAllResults()
        {
            return _repository.Include(x => x.Pages);
        }

        public async Task AddResultAsync(CrawlingResult result)
        {
            await _repository.AddAsync(result);
            await _repository.SaveChangesAsync();
        }

        public async Task AddRangeResult(IEnumerable<CrawlingResult> results)
        {
            _repository.AddRange(results);
            await _repository.SaveChangesAsync();
        }
    }
}
