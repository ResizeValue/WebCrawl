using System.Linq;
using WebCrawl.Logic;

namespace WebCrawl.ConsoleApplication
{
    public class ConsoleWebCrawler
    {
        private readonly WebCrawler webCrawler;
        private readonly ConsoleWrapper wrapper;

        public ConsoleWebCrawler(WebCrawler crawler, ConsoleWrapper _wrapper)
        {
            webCrawler = crawler;
            wrapper = _wrapper;
        }

        public void Run()
        {
            while (true)
            {
                wrapper.ShowMessage("Enter the URL: ");

                string url = wrapper.ReadMessage();

                if (!url.EndsWith('/'))
                {
                    url = url + '/';
                }
                var result = webCrawler.ParseUrl(url);
                var responseTime = webCrawler.GetResponseTimeList(result);

                var sitemapResult = result.Where(x => !x.IsCrawlerUrl && x.IsSitemapUrl);
                wrapper.ShowMessage("\n\nUrls FOUNDED IN SITEMAP.XML but not founded after crawling a web site:\n" + string.Join("\n", sitemapResult.Select(x => x.Url)));

                var crawlerResult = result.Where(x => x.IsCrawlerUrl && !x.IsSitemapUrl);
                wrapper.ShowMessage("\n\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:\n" + string.Join("\n", crawlerResult.Select(x => x.Url)));

                wrapper.ShowMessage("\n\nTiming:\n");
                
                foreach (var link in responseTime)
                {
                    wrapper.ShowMessage(link.Url + " | " + link.ResponseTime.TotalMilliseconds.ToString("0") + " ms");
                }

                wrapper.ShowMessage("\n\nUrls(html documents) found after crawling a website: " + result.Where(x => x.IsCrawlerUrl).Count());
                wrapper.ShowMessage("Urls found in sitemap: " + result.Where(x => x.IsSitemapUrl).Count());
            }
        }
    }
}
