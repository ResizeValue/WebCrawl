using System;
using System.Collections.Generic;
using System.Text;
using WebCrawl.Logic.Crawler;

namespace WebCrawl.Logic.Services
{
    public class ValidationInputUrlService : ReferenceValidation
    {
        public string ErrorMessage { get; private set; }

        public ValidationInputUrlService(WebContentLoader loader) : base(loader)
        {

        }

        public override bool IsValidInputUrl(string url)
        {
            if (!base.IsCorrectUrl(url))
            {
                ErrorMessage = "Incorrect string format";
                return false;
            }

            if (!base.IsValidInputUrl(url))
            {
                ErrorMessage = "Can't find a site";
                return false;
            }

            return true;
        }

    }
}
