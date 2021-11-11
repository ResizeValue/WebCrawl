using System;
using System.Diagnostics;

namespace WebCrawl.Logic
{
    public class HtmlResponseTracker
    {
        private readonly WebContentLoader webLoader;

        public HtmlResponseTracker()
        {
            webLoader = new WebContentLoader();
        }

        public virtual TimeSpan CheckResponseTime(string url)
        {
            var timer = new Stopwatch();

            timer.Start();

            webLoader.DownloadContent(url);

            timer.Stop();

            return timer.Elapsed;
        }
    }
}
