using System.Collections.Generic;
using System.Xml;

namespace WebCrawl.Sitemap
{
    public class XmlParser
    {
        public List<string> ParseXml(string inputString)
        {
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(inputString);

            XmlNodeList xmlSitemapList = xmlDocument.GetElementsByTagName("url");

            List<string> parsedRefs = new List<string>();

            foreach (XmlNode node in xmlSitemapList)
            {
                if (node["loc"] == null)
                {
                    continue;
                }

                var sitemapReference = node["loc"].InnerText;

                parsedRefs.Add(sitemapReference);
            }

            return parsedRefs;
        }
    }
}
