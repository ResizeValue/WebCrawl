using WebCrawl.ConsoleApplication;
using WebCrawl.Crawler;
using WebCrawl.Sitemap;

namespace WebCrawl
{
    class Program
    {
        static void Main(string[] args)
        {
            WebCrawler.WebCrawler crawlerClass = new WebCrawler.WebCrawler(
                new HtmlParser(new ReferenceValidation(), new WebLoader()),
                new SitemapParser(new XmlParser(), new WebLoader()));

            ConsoleWebCrawler webCrawler = new ConsoleWebCrawler(crawlerClass, new ConsoleWrapper());

            webCrawler.Run();
        }
    }
}
