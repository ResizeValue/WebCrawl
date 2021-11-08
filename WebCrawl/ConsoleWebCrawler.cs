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
                wrapper.ShowMessage("\nResult:\n" + string.Join("\n", result.Where(x => x.IsCrawlerUrl && !x.IsSitemapUrl).Select(x => x.Url)));

            }
        }
    }
}
