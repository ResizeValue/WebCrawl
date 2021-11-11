using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace WebCrawl.Logic.Crawler
{
    public class HtmlPageParser
    {
        private readonly WebContentLoader _contentLoader;
        private readonly ReferenceValidation _validation;

        public HtmlPageParser(WebContentLoader contentLoader, ReferenceValidation validation)
        {
            _contentLoader = contentLoader;
            _validation = validation;
        }

        public virtual List<string> ParsePageForUrls(string url, string baseUrl)
        {
            string baseAddress = baseUrl;

            HtmlDocument htmlDocument = new HtmlDocument();

            try
            {
                htmlDocument.LoadHtml(_contentLoader.DownloadContent(url));
            }
            catch (WebException exception)
            {
                throw new WebException(exception.Message);
            }

            var findedReferences = htmlDocument.DocumentNode.SelectNodes("//a");

            if (findedReferences == null)
            {
                return null;
            }

            List<string> findedRefsList = new List<string>();

            foreach (var reference in findedReferences)
            {
                if (reference.Attributes["href"] == null)
                {
                    continue;
                }

                if (!_validation.IsCorrectUrl(reference.Attributes["href"].Value))
                {
                    continue;
                }

                var referenceValue = reference.Attributes["href"].Value;

                referenceValue = StringProcessing(referenceValue, baseAddress);

                findedRefsList.Add(referenceValue);
            }

            return findedRefsList;
        }

        private string StringProcessing(string url, string baseAddress)
        {
            if (url.First() == '/')
            {
                url = url.Remove(0, 1);
            }

            if (!url.Contains("http"))
            {
                url = baseAddress + url;
            }

            return url;
        }
    }
}
