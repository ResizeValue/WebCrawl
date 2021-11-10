using System;
using System.Linq;
using WebCrawl.Logic;

namespace WebCrawl.ConsoleApplication
{
    public class ConsoleWebCrawler
    {
        private readonly WebCrawler _webCrawler;
        private readonly ConsoleWrapper _wrapper;
        private readonly RepositoryService _repositoryService;

        public ConsoleWebCrawler(WebCrawler crawler, ConsoleWrapper wrapper, RepositoryService repositoryService)
        {
            _webCrawler = crawler;
            _wrapper = wrapper;
            _repositoryService = repositoryService;
        }

        public async void Run()
        {
            while (true)
            {
                _wrapper.ShowMessage("Enter the URL: ");

                string url = _wrapper.ReadMessage();

                if (!url.EndsWith('/'))
                {
                    url = url + '/';
                }

                var result = _webCrawler.ParseUrl(url);

                var responseTime = _webCrawler.GetResponseTimeList(result);


                var sitemapResult = result.Where(x => !x.IsCrawlerUrl && x.IsSitemapUrl);
                _wrapper.ShowMessage("\n\nUrls FOUNDED IN SITEMAP.XML but not founded after crawling a web site:\n" + string.Join("\n", sitemapResult.Select(x => x.Url)));

                var crawlerResult = result.Where(x => x.IsCrawlerUrl && !x.IsSitemapUrl);
                _wrapper.ShowMessage("\n\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:\n" + string.Join("\n", crawlerResult.Select(x => x.Url)));

                _wrapper.ShowMessage("\n\nTiming:\n");

                foreach (var link in responseTime)
                {
                    _wrapper.ShowMessage(link.Url + " | " + link.ResponseTime.TotalMilliseconds.ToString("0") + " ms");
                }

                _wrapper.ShowMessage("\n\nUrls(html documents) found after crawling a website: " + result.Where(x => x.IsCrawlerUrl).Count());
                _wrapper.ShowMessage("Urls found in sitemap: " + result.Where(x => x.IsSitemapUrl).Count());

                if (responseTime.Count() > 0)
                {
                    try
                    {
                        await _repositoryService.SaveResultAsync(url, responseTime);
                        _wrapper.ShowMessage("\nResult has been saved to database!");
                    }
                    catch (Exception exception)
                    {
                        _wrapper.ShowMessage(exception.Message);
                    }
                }
            }
        }
    }
}
