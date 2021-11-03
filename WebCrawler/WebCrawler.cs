using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace WebCrawler
{
    public class WebCrawler
    {
        private Dictionary<string, TimeSpan> checkedRefs;
        private List<string> sitemapRefs;

        public WebCrawler()
        {
            checkedRefs = new Dictionary<string, TimeSpan>();
            sitemapRefs = new List<string>();
        }

        public WebCrawler(Dictionary<string, TimeSpan> _checkedRefs, List<string> _sitemapRefs)
        {
            checkedRefs = _checkedRefs;
            sitemapRefs = _sitemapRefs;
        }

        public string BaseAddress { get; set; }
        public bool Sitemap { get; set; }
        public List<string> SitemapRefs
        {
            get
            {
                return sitemapRefs;
            }
        }
        public Dictionary<string, TimeSpan> CheckedRefs
        {
            get
            {
                return checkedRefs;
            }
        }

        public void ParseUrl(string url)
        {
            BaseAddress = url;

            TryParseUrl(url);
        }

        private void TryParseUrl(string url)
        {
            try
            {
                var htmlDocument = LoadHtmlPage(url);

                var findedReferences = htmlDocument.DocumentNode.SelectNodes("//a");

                if (findedReferences == null)
                {
                    return;
                }

                foreach (var reference in findedReferences)
                {
                    if (reference.Attributes["href"] == null)
                    {
                        continue;
                    }

                    var referenceValue = reference.Attributes["href"].Value;

                    if (!IsCorrectUrl(referenceValue))
                    {
                        continue;
                    }

                    StringProcessing(ref referenceValue);

                    CheckReference(referenceValue);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("\n Exception!\n" + exception.Message + "\n");
            }
        }

        public HashSet<string> GetRefsWhichExistOnlyInSitemap()
        {
            HashSet<string> sitemapOnlyRefs = new HashSet<string>(sitemapRefs);

            sitemapOnlyRefs.ExceptWith(checkedRefs.Keys);

            return sitemapOnlyRefs;
        }

        public HashSet<string> GetRefsWhichExistOnlyInCrawlList()
        {
            HashSet<string> CrawlOnlyRefs = new HashSet<string>(checkedRefs.Keys);

            CrawlOnlyRefs.ExceptWith(sitemapRefs);

            return CrawlOnlyRefs;
        }

        public void ParseSitemap(string url)
        {
            var downloadedString = LoadSitemap(url);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();

                xmlDocument.LoadXml(downloadedString);

                XmlNodeList xmlSitemapList = xmlDocument.GetElementsByTagName("url");

                TryParseSitemap(xmlSitemapList);

                Sitemap = true;
            }
            catch
            {
                throw new FileLoadException("Can't load xml document");
            }
        }

        public string LoadSitemap(string url)
        {
            WebClient webClient = new WebClient()
            {
                Encoding = System.Text.Encoding.UTF8
            };
            string downloadedString = string.Empty;
            try
            {
                downloadedString = webClient.DownloadString(url);
            }
            catch
            {
                throw new FileNotFoundException("Sitemap does not found: " + url);
            }
            return downloadedString;
        }

        private void TryParseSitemap(XmlNodeList xmlSitemapList)
        {
            foreach (XmlNode node in xmlSitemapList)
            {
                if (node["loc"] == null)
                {
                    continue;
                }

                var sitemapReference = node["loc"].InnerText;

                sitemapRefs.Add(sitemapReference);
            }
        }

        private bool IsFile(string url)
        {
            url = url.ToLower();
            if (url.EndsWith(".doc") || url.EndsWith(".docx") || url.EndsWith(".pdf") || url.EndsWith(".xls")
                || url.EndsWith(".xlsx") || url.EndsWith(".txt") || url.EndsWith(".png") || url.EndsWith(".jgp")
                || url.EndsWith(".jpeg") || url.EndsWith(".webp") || url.EndsWith(".gif") || url.EndsWith(".xml")
                || url.EndsWith(".aif") || url.EndsWith(".mp3") || url.EndsWith(".ogg") || url.EndsWith(".wav")
                || url.EndsWith(".pkg") || url.EndsWith(".rar") || url.EndsWith(".zip") || url.EndsWith(".ico"))
                return true;

            return false;
        }

        private bool IsCorrectUrl(string url)
        {
            if (url == null)
            {
                return false;
            }

            if (url.Length < 1 || url.Contains("#") ||
                url.Contains("@") || url.Contains("?") ||
                IsFile(url) || url.Split(':').Length > 2)
            {
                return false;
            }

            return true;
        }

        private void CheckReference(string reference)
        {
            if (!checkedRefs.ContainsKey(reference))
            {
                if (reference.StartsWith(BaseAddress)) // does the ref belong to the site
                {
                    try
                    {
                        TryParseUrl(reference); // recursively parse another page
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nException!\n" + ex.Message + "\n");
                    }
                }
            }
        }

        private string StringProcessing(ref string url)
        {
            if (url.First() == '/')
            {
                url = url.Remove(0, 1);
            }

            if (!url.Contains("http"))
            {
                url = BaseAddress + url;
            }

            return url;
        }

        private HtmlDocument LoadHtmlPage(string url)
        {
            var web = new HtmlWeb();

            var timer = new Stopwatch();

            timer.Start();

            var htmlDocument = web.Load(url);

            timer.Stop();

            TimeSpan responseTime = timer.Elapsed;

            checkedRefs.Add(url, responseTime);

            return htmlDocument;
        }
    }
}
