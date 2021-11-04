using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace WebCrawl.Crawler
{
    public class ReferenceValidation
    {
        public bool IsValidReference(HtmlNode reference)
        {
            if (reference.Attributes["href"] == null)
            {
                return false;
            }

            var referenceValue = reference.Attributes["href"].Value;

            if (!IsCorrectUrl(referenceValue))
            {
                return false;
            }

            return true;
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

        private bool IsCorrectUrl(string url)
        {
            if (url == null)
            {
                return false;
            }

            bool isAnySymbol = url.Contains("#") || url.Contains("@") || url.Contains("?");

            if (url.Length < 1 || isAnySymbol ||
                IsFile(url) || url.Split(':').Length > 2)
            {
                return false;
            }

            return true;
        }
    }
}
