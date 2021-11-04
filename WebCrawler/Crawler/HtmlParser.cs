using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using WebCrawl.Crawler;

namespace WebCrawl
{
    public class HtmlParser
    {
        private List<string> parsedUrls;
        private readonly ReferenceValidation _validation;
        private readonly WebLoader _webLoader;
        public HtmlParser(ReferenceValidation validation, WebLoader htmlLoader)
        {
            parsedUrls = new List<string>();
            _validation = validation;
            _webLoader = htmlLoader;
        }
        public string BaseAddress { get; set; }

        public virtual List<string> ParseUrl(string url)
        {
            BaseAddress = url;

            ParseNextUrl(url);

            return parsedUrls;
        }

        private void ParseNextUrl(string url)
        {
            HtmlDocument htmlDocument = new HtmlDocument();

            try
            {
                htmlDocument.LoadHtml(_webLoader.DownloadString(url));
            }
            catch
            {
                return;
            }

            parsedUrls.Add(url);

            var findedReferences = htmlDocument.DocumentNode.SelectNodes("//a");

            if (findedReferences == null)
            {
                return;
            }

            foreach (var reference in findedReferences)
            {
                if (!_validation.IsValidReference(reference))
                {
                    continue;
                }

                var referenceValue = reference.Attributes["href"].Value;

                StringProcessing(ref referenceValue);

                CheckReference(referenceValue);
            }
        }

        private void CheckReference(string reference)
        {
            if (!parsedUrls.Contains(reference))
            {
                if (reference.StartsWith(BaseAddress)) // does the ref belong to the site
                {
                    ParseNextUrl(reference); // recursively parse another page
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
    }
}
