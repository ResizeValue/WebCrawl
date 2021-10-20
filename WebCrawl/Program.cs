﻿using HtmlAgilityPack;
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
                    SaveResuts(sitemap_list, checked_list);
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

        static void SaveResuts(HashSet<string> sitemap_list, Dictionary<string, TimeSpan> checked_list)
        {
            try
            {
                // saving sitemap.xml parsing result
                if (sitemap_list.Count > 0) 
                {
                    if (!Directory.Exists(Environment.CurrentDirectory + "\\Result"))
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\Result");
                    var sitemap_res = new StreamWriter(Environment.CurrentDirectory + "\\Result\\sitemap_result.txt");
                    int i = 0;
                    foreach (var ref__ in sitemap_list)
                    {
                        sitemap_res.WriteLine(++i + ". " + ref__);
                    }
                    sitemap_res.Close();
                    Console.WriteLine("\n\nSITEMAP.XML parsing result has been saved in sitemap_result.txt");
                }

                // saving crawling result
                if (checked_list.Count > 0)
                {
                    var parse_res = new StreamWriter(Environment.CurrentDirectory + "\\Result\\parse_result.txt");
                    int index = 0;
                    foreach (var item in checked_list.OrderBy(key => key.Value)) // order by response time and write to file
                    {
                        parse_res.WriteLine(++index + ". " + item.Key + " | " + item.Value.TotalMilliseconds.ToString("0") + " ms");
                    }
                    // short info
                    parse_res.WriteLine("\nUrls found after crawling a website: " + checked_list.Count);
                    parse_res.WriteLine("\nUrls found in sitemap: " + sitemap_list.Count);
                    parse_res.Close();
                    Console.WriteLine("\n\nCrawling result has been saved in ParseResult.txt");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Except(HashSet<string> sitemap_list, Dictionary<string, TimeSpan> checked_list)
        {
            if (sitemap_list.Count < 1 && checked_list.Count < 1) return;
            try
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\Result"))
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\Result");
                HashSet<string> except = new HashSet<string>(checked_list.Keys); // using HashSet for ExceptWith method
                except.ExceptWith(sitemap_list);
                Console.WriteLine("\n\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml: \n\n");
                var writer = new StreamWriter(Environment.CurrentDirectory + "\\Result\\Crawling except Sitemap.txt");
                foreach (var item in except)
                {
                    Console.WriteLine(item);
                    writer.WriteLine(item);
                }
                writer.Close();
                Console.WriteLine("\nResult has been saved in Crawling except Sitemap.txt");


                writer = new StreamWriter(Environment.CurrentDirectory + "\\Result\\Sitemap except Crawling.txt");
                except = new HashSet<string>(sitemap_list);
                except.ExceptWith(checked_list.Keys);
                Console.WriteLine("\n\nUrls FOUNDED IN SITEMAP.XML but not founded after crawling a web site:\n\n");
                foreach (var item in except)
                {
                    Console.WriteLine(item);
                    writer.WriteLine(item);
                }
                writer.Close();
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
            int i = 0;
            foreach (var item in checked_list)
            {
                Console.WriteLine(++i + ". " + item.Key + " | " + item.Value.TotalMilliseconds.ToString("0") + "ms");
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
    }
}
