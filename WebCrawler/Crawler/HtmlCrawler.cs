using System;
using System.Collections.Generic;
using System.Net;

namespace WebCrawl.Crawler
{
    public class HtmlCrawler
    {
        private readonly HtmlPageParser _htmlparser;
        private string _baseAddress;

        public HtmlCrawler(HtmlPageParser htmlparser)
        {
            _htmlparser = htmlparser;
        }

        public virtual List<string> ParseHtmlDocuments(string url)
        {
            _baseAddress = url;

            Queue<string> references = new Queue<string>();

            references.Enqueue(url);

            List<string> parsedUrls = new List<string>();

            while (references.Count > 0)
            {
                var nextUrl = references.Dequeue();

                if (!nextUrl.StartsWith(_baseAddress) || parsedUrls.Contains(nextUrl))
                {
                    continue;
                }

                try
                {
                    var findedRefs = _htmlparser.ParsePageForUrls(nextUrl, _baseAddress);
                    parsedUrls.Add(nextUrl);
                    if (findedRefs != null)
                    {
                        findedRefs.ForEach(x => references.Enqueue(x));
                    }
                }
                catch (WebException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return parsedUrls;
        }
    }
}
