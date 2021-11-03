using System;
using System.IO;

namespace WebCrawl.ConsoleApplication
{
    public class ConsoleWebCrawler
    {
        private readonly WebCrawler.WebCrawler webCrawler;
        private readonly ConsoleWrapper wrapper;

        public ConsoleWebCrawler(WebCrawler.WebCrawler crawler, ConsoleWrapper _wrapper)
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

                try
                {
                    webCrawler.ParseSitemap(url + "sitemap.xml");
                }
                catch (FileNotFoundException notFoundException)
                {
                    wrapper.ShowMessage(notFoundException.Message);
                }

                try
                {
                    webCrawler.ParseUrl(url);

                    //wrapper.ShowMessage(string.Join("\n", webCrawler.GetRefsWhichExistOnlyInCrawlList()));

                    //wrapper.ShowMessage(string.Join("\n", webCrawler.GetRefsWhichExistOnlyInSitemap()));
                }
                catch (Exception e)
                {
                    wrapper.ShowMessage("\n\nException!\n" + e.Message + "\n\n\n\n");
                }
            }
        }
    }
}
