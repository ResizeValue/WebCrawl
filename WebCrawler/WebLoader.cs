using System;
using System.IO;
using System.Net;

namespace WebCrawl
{
    public class WebLoader
    {
        private readonly WebClient webClient;
        public WebLoader()
        {
            webClient = new WebClient() { Encoding = System.Text.Encoding.UTF8 };
        }
        public string DownloadString(string url)
        {
            string downloadedString;

            try
            {
                downloadedString = webClient.DownloadString(url);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Sitemap does not found: " + url);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return downloadedString;
        }
    }
}
