using System.Collections.Generic;
using System.Linq;

namespace WebCrawl.Logic.Crawler
{
    public class ReferenceValidation
    {
        private readonly WebContentLoader _contentLoader;
        public ReferenceValidation(WebContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }
        private bool IsFile(string url)
        {
            var extentions = new List<string> {
                ".doc", ".docx", ".pdf", ".xls",
                ".xlsx",".txt",".png",".jgp",
                ".jpeg",".webp",".gif",".xml",
                ".aif",".mp3",".ogg",".wav",
                ".pkg",".rar",".zip",".ico"
            };

            if (extentions.FirstOrDefault(x => url.Contains(x)) != null)
            {
                return true;
            }

            return false;
        }

        public virtual bool IsCorrectUrl(string url)
        {
            if (url == null)
            {
                return false;
            }

            bool isAnySymbol = url.Contains("#") || url.Contains("@") || url.Contains("?");

            if (url.Length < 1 || isAnySymbol ||
                IsFile(url) || url.Count(x => x == ':') > 1)
            {
                return false;
            }

            return true;
        }

        public virtual bool IsValidInputUrl(string url)
        {
            if (!url.StartsWith("http"))
            {
                url = "http://" + url;
            }

            if (_contentLoader.TryDownloadContent(url))
            {
                return true;
            }

            return false;
        }
    }
}
