using System;
using System.Collections.Generic;
using System.Net;

namespace WebCrawl.Logic.Crawler
{
    public class HtmlCrawler
    {
        private readonly HtmlPageParser _htmlparser;

        public HtmlCrawler(HtmlPageParser htmlparser)
        {
            _htmlparser = htmlparser;
        }

        public virtual List<string> ParseHtmlDocuments(string url)
        {
            string baseAddress = url;

            Queue<string> references = new Queue<string>();

            references.Enqueue(url);

            List<string> parsedUrls = new List<string>();

            while (references.Count > 0)
            {
                var nextUrl = references.Dequeue();

                if (!nextUrl.StartsWith(baseAddress) || parsedUrls.Contains(nextUrl))
                {
                    continue;
                }

                try
                {
                    var foundRefs = _htmlparser.ParsePageForUrls(nextUrl, baseAddress);

                    parsedUrls.Add(nextUrl);

                    if (foundRefs != null)
                    {
                        foreach (var reference in foundRefs)
                        {
                            references.Enqueue(reference);
                        }
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
