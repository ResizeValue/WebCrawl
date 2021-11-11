using System.Net;

namespace WebCrawl.Logic
{
    public class WebContentLoader
    {
        private readonly WebClient webClient;

        public WebContentLoader()
        {
            webClient = new WebClient() { Encoding = System.Text.Encoding.UTF8 };
        }

        public virtual string DownloadContent(string url)
        {
            string downloadedString;

            downloadedString = webClient.DownloadString(url);

            return downloadedString;
        }

        public bool TryDownloadContent(string url)
        {
            try
            {
                webClient.DownloadString(url);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
