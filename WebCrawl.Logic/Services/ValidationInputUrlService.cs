using WebCrawl.Logic.Crawler;

namespace WebCrawl.Logic.Services
{
    public class ValidationInputUrlService : ReferenceValidation
    {
        public string ErrorMessage { get; private set; }

        public ValidationInputUrlService(WebContentLoader loader) : base(loader) {}

        public override bool IsValidInputUrl(ref string url)
        {
            if (!base.IsCorrectUrl(url))
            {
                ErrorMessage = "Incorrect string format";

                return false;
            }

            if (!base.IsValidInputUrl(ref url))
            {
                ErrorMessage = "The site was not found";

                return false;
            }

            return true;
        }
    }
}
