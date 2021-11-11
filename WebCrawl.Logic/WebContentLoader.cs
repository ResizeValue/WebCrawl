using System;
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

            try
            {
                downloadedString = webClient.DownloadString(url);
            }
            catch (WebException)
            {
                throw new WebException("File does not found by path: " + url);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

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
