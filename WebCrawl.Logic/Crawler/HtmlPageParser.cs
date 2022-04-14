using HtmlAgilityPack;
using System.Collections.Generic;

namespace WebCrawl.Logic.Crawler
{
    public class HtmlPageParser
    {
        private readonly WebContentLoader _contentLoader;
        private readonly ReferenceValidation _validation;
        private readonly UrlConverter _converter;

        public HtmlPageParser(WebContentLoader contentLoader, ReferenceValidation validation, UrlConverter converter)
        {
            _contentLoader = contentLoader;
            _validation = validation;
            _converter = converter;
        }

        public virtual List<string> ParsePageForUrls(string url, string baseUrl)
        {
            HtmlDocument htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(_contentLoader.DownloadContent(url));

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

                referenceValue = _converter.CreateAbsoluteUrl(referenceValue, baseUrl);

                findedRefsList.Add(referenceValue);
            }

            return findedRefsList;
        }

    }
}
