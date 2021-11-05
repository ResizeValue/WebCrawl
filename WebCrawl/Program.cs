using WebCrawl.ConsoleApplication;
using WebCrawl.Crawler;
using WebCrawl.Sitemap;

namespace WebCrawl
{
    class Program
    {
        static void Main(string[] args)
        {
            WebCrawler crawlerClass = new WebCrawler(
                new HtmlCrawler(new HtmlPageParser(new WebContentLoader(), new ReferenceValidation())),
                new SitemapParser(new XmlParser(), new WebContentLoader()));

            ConsoleWebCrawler webCrawler = new ConsoleWebCrawler(crawlerClass, new ConsoleWrapper());

            webCrawler.Run();
        }
    }
}
