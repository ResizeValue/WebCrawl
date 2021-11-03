using System;
using System.Collections.Generic;
using WebCrawl.ConsoleApplication;

namespace WebCrawl
{
    class Program
    {
        static void Main(string[] args)
        {
            WebCrawler.WebCrawler crawlerClass = new WebCrawler.WebCrawler(new Dictionary<string, TimeSpan>(), new List<string>());

            ConsoleWebCrawler webCrawler = new ConsoleWebCrawler(crawlerClass, new ConsoleWrapper());

            webCrawler.Run();
        }
    }
}
