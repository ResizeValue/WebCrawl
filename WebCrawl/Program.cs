using HtmlAgilityPack;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;

namespace WebCrawl
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var sitemap_list = new HashSet<string>();
                var checked_list = new Dictionary<string, TimeSpan>();

                Console.Write("Enter the URL: ");

                string url = Console.ReadLine();
                if (!url.EndsWith('/')) url = url + '/';

                try
                {
                    LoadSitemap(url + "sitemap.xml", sitemap_list); //trying to get a sitemap.xml
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File sitemap.xml does not exist!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    ReadURL(url, checked_list);
                    Except(sitemap_list, checked_list);
                    Timing(checked_list, sitemap_list);
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\nException!\n" + e.Message);
                }
                Console.WriteLine("\n\n\n\n");
            }
        }

        static void LoadSitemap(string url, HashSet<string> sitemap_list)
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            XmlDocument urldoc = new XmlDocument();
            string str = wc.DownloadString(url);
            if (str == null) throw new FileNotFoundException();
            Console.WriteLine("sitemap.xml founded!");
            try
            {
                Console.WriteLine("Trying to load xml...");
                urldoc.LoadXml(str);

                XmlNodeList xmlSitemapList = urldoc.GetElementsByTagName("url");
                if (xmlSitemapList.Count < 1) xmlSitemapList = urldoc.GetElementsByTagName("sitemap"); // check for another sitemap files

                Console.WriteLine("\nParsing " + url + "...");
                foreach (XmlNode node in xmlSitemapList)
                {
                    if (node["loc"] != null)
                    {
                        var ref__ = node["loc"].InnerText;
                        if (ref__.Contains("sitemap"))
                        {
                            try
                            {
                                Console.WriteLine("New sitemap " + ref__);
                                LoadSitemap(ref__, sitemap_list); // recursively check of another sitemap
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            sitemap_list.Add(ref__);
                        }
                    }

                }

                Console.WriteLine(url + " has been parsed!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
        static void Except(HashSet<string> sitemap_list, Dictionary<string, TimeSpan> checked_list)
        {
            if (sitemap_list.Count < 1 && checked_list.Count < 1) return;
            try
            {
                HashSet<string> except = new HashSet<string>(checked_list.Keys); // using HashSet for ExceptWith method
                except.ExceptWith(sitemap_list);
                Console.WriteLine("\n\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml: \n\n");
                var writer = new StreamWriter(Environment.CurrentDirectory + "\\Result\\Crawling except Sitemap.txt");
                foreach (var item in except)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("\nResult has been saved in Crawling except Sitemap.txt");


                except = new HashSet<string>(sitemap_list);
                except.ExceptWith(checked_list.Keys);
                Console.WriteLine("\n\nUrls FOUNDED IN SITEMAP.XML but not founded after crawling a web site:\n\n");
                foreach (var item in except)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("\nResult has been saved in Sitemap except Crawling.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void ReadURL(string url, Dictionary<string, TimeSpan> checked_list)
        {
            ParseUrl(url, url, checked_list);
            Console.WriteLine("\n\nCrawling result:");
            if (checked_list.Count < 1)
            {
                Console.WriteLine("None.");
                return;
            }
        }


        static void ParseUrl(string url, string base_url, Dictionary<string, TimeSpan> checked_list)
        {
            try
            {
                Console.WriteLine("Parsing: " + url);
                var web = new HtmlWeb(); // using HtmlAgilityPack for work with html
                System.Diagnostics.Stopwatch timer = new Stopwatch(); // timer for count response time
                timer.Start();

                var doc = web.Load(url); // load html document

                timer.Stop();
                TimeSpan responseTime = timer.Elapsed;

                checked_list.Add(url, responseTime); // mark url as "checked"

                var refs = doc.DocumentNode.SelectNodes("//a"); // select all <a> tags
                if (refs == null) return;
                Console.WriteLine("Refs count: " + checked_list.Count);
                foreach (var item in refs)
                {
                    if (item.Attributes["href"] == null) continue;
                    var ref_ = item.Attributes["href"].Value; // get a reference from the <a> tag

                    if (ref_ == null) { Console.WriteLine("Ref is null!"); continue; }
                    if (ref_.Length < 1) { continue; }
                    if (ref_.Contains("#") || ref_.Contains("@") || ref_.Contains("?") || ref_.Contains("javascript:")) continue;
                    if (ref_[0] == '/') ref_ = ref_.Remove(0, 1);
                    if (!ref_.Contains("http")) ref_ = base_url + ref_;
                    if (IsFile(ref_)) continue;
                    if (ref_.Split(':').Length > 2) continue;
                    if (!checked_list.ContainsKey(ref_))
                    {
                        if (ref_.StartsWith(base_url)) // does the ref belong to the site
                        {
                            try
                            {
                                ParseUrl(ref_, base_url, checked_list); // recursively parse another page
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("\nException!\n" + ex.Message + "\n");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Exception!\n" + e.Message + "\n");
            }
        }

        static void Timing(Dictionary<string, TimeSpan> checked_list, HashSet<string> sitemap)
        {
            Console.WriteLine("Timing");
            int i = 0;
            foreach (var item in checked_list)
            {
                Console.WriteLine(++i + ". " + item.Key + " | " + item.Value.TotalMilliseconds.ToString("0") + "ms");
            }
            Console.WriteLine("\nUrls(html documents) found after crawling a website: " + checked_list.Count);
            Console.WriteLine("Urls found in sitemap:: " + sitemap.Count);
        }

        static bool IsFile(string url)
        {
            url = url.ToLower();
            if (url.EndsWith(".doc") || url.EndsWith(".docx") || url.EndsWith(".pdf") || url.EndsWith(".xls")
                || url.EndsWith(".xlsx") || url.EndsWith(".txt") || url.EndsWith(".png") || url.EndsWith(".jgp")
                || url.EndsWith(".jpeg") || url.EndsWith(".webp") || url.EndsWith(".gif") || url.EndsWith(".xml")
                || url.EndsWith(".aif") || url.EndsWith(".mp3") || url.EndsWith(".ogg") || url.EndsWith(".wav")
                || url.EndsWith(".pkg") || url.EndsWith(".rar") || url.EndsWith(".zip") || url.EndsWith(".ico"))
                return true;

            return false;
        }
    }
}
