using System.Linq;

namespace WebCrawl.Logic
{
    public class UrlConverter
    {
        public virtual string CreateAbsoluteUrl(string url, string baseUrl)
        {
            if (url.First() == '/')
            {
                url = url.Remove(0, 1);
            }

            if (!url.Contains("http"))
            {
                url = baseUrl + url;
            }

            return url;
        }
    }
}
