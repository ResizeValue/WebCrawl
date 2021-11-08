using System.Threading.Tasks;
using WebCrawl.Logic;
using WebCrawl.Logic.Crawler;
using WebCrawl.Logic.Sitemap;

namespace WebCrawl.ConsoleApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WebCrawler crawlerClass = new WebCrawler(
                new HtmlCrawler(new HtmlPageParser(new WebContentLoader(), new ReferenceValidation())),
                new SitemapParser(new XmlParser(), new WebContentLoader()));

            ConsoleWebCrawler webCrawler = new ConsoleWebCrawler(crawlerClass, new ConsoleWrapper());

            webCrawler.Run();
        }
    }
}
