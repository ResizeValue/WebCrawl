using System;
using System.Diagnostics;

namespace WebCrawl.Crawler
{
    public class HtmlResponseTracker
    {
        private readonly WebLoader webLoader;

        public HtmlResponseTracker()
        {
            webLoader = new WebLoader();
        }

        public TimeSpan CheckResponseTime(string url)
        {
            var timer = new Stopwatch();

            timer.Start();

            webLoader.DownloadString(url);

            timer.Stop();

            TimeSpan responseTime = timer.Elapsed;

            return responseTime;
        }
    }
}
