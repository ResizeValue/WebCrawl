using System.Collections.Generic;
using System.Xml;

namespace WebCrawl.Logic.Sitemap
{
    public class XmlParser
    {
        public virtual List<string> ParseXmlString(string xmlString)
        {
            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.LoadXml(xmlString);
            }
            catch
            {
                return new List<string>();
            }

            XmlNodeList xmlSitemapList = xmlDocument.GetElementsByTagName("url");

            List<string> parsedRefs = new List<string>();

            foreach (XmlNode node in xmlSitemapList)
            {
                if (node["loc"] == null)
                {
                    continue;
                }

                var sitemapReference = node["loc"].InnerText;

                if (sitemapReference != string.Empty)
                {
                    parsedRefs.Add(sitemapReference);
                }
            }

            return parsedRefs;
        }
    }
}
