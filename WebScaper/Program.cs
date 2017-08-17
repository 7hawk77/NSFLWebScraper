using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HtmlAgilityPack;

namespace WebScaper
{
    class Program
    {

        static void Main(string[] args)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

                Scrape(web, ConfigurationManager.AppSettings["SJS1"].ToString(), ConfigurationManager.AppSettings["SJS2"].ToString(), ConfigurationManager.AppSettings["SJS3"].ToString(), "SJS", "San Jose Sabercats");
                Scrape(web, ConfigurationManager.AppSettings["BH1"].ToString(), ConfigurationManager.AppSettings["BH2"].ToString(), ConfigurationManager.AppSettings["BH3"].ToString(), "BH", "Baltimore Hawks");
                Scrape(web, ConfigurationManager.AppSettings["CY1"].ToString(), ConfigurationManager.AppSettings["CY2"].ToString(), ConfigurationManager.AppSettings["CY3"].ToString(), "CY", "Colorado Yetis");
                Scrape(web, ConfigurationManager.AppSettings["PL1"].ToString(), ConfigurationManager.AppSettings["PL2"].ToString(), ConfigurationManager.AppSettings["PL3"].ToString(), "PL", "Philadelphia Liberty");
                Scrape(web, ConfigurationManager.AppSettings["YW1"].ToString(), ConfigurationManager.AppSettings["YW2"].ToString(), ConfigurationManager.AppSettings["YW3"].ToString(), "YW", "Yellowknife Wraiths");
                Scrape(web, ConfigurationManager.AppSettings["AO1"].ToString(), ConfigurationManager.AppSettings["AO2"].ToString(), ConfigurationManager.AppSettings["AO3"].ToString(), "AO", "Arizona Outlaws");
                Scrape(web, ConfigurationManager.AppSettings["LVL1"].ToString(), ConfigurationManager.AppSettings["LVL2"].ToString(), ConfigurationManager.AppSettings["LVL3"].ToString(), "LVL", "Las Vegas Legion");
                Scrape(web, ConfigurationManager.AppSettings["OCO1"].ToString(), ConfigurationManager.AppSettings["OCO2"].ToString(), ConfigurationManager.AppSettings["OCO3"].ToString(), "OCO", "Orange County Otters");
        }

        private static void Scrape(HtmlWeb web,string d1, string d2, string d3, string teamAbrv, string teamName)
        {
            HtmlAgilityPack.HtmlDocument doc = web.Load(d1);
            HtmlAgilityPack.HtmlDocument doc2 = web.Load(d2);
            HtmlAgilityPack.HtmlDocument doc3 = null;
            int pagecount;
            HtmlNodeCollection pagination = doc.DocumentNode.SelectNodes(ConfigurationManager.AppSettings["PaginationPage"]);
            if (pagination == null)
            {
                pagecount = 1;
            }
            else
            {
                string page = pagination[0].InnerText.Split('(')[1];
                pagecount = (Convert.ToInt32(page.Split(')')[0]));
            }

            List<HtmlNode> PlayerNames = GetNodes(doc, ConfigurationManager.AppSettings["PlayerNodes"].ToString());
            PlayerNames.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

            if (pagecount == 3)
            {
                doc3 = web.Load(d3);
                PlayerNames.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());
            }

            PlayerNames.ForEach(x => x.InnerHtml = x.InnerHtml.Replace("&#39;", "'"));
            RemoveRepliesFromPlayerNames(PlayerNames);

            var PlayerTPE = GetNodes(doc, ConfigurationManager.AppSettings["TPENodes"].ToString());
            PlayerTPE.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

            if (pagecount == 3)
            {
                PlayerTPE.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());
            }

            foreach(var i in PlayerTPE)
            {
                if(i.InnerHtml.Length==0)
                {
                    i.InnerHtml = "TPE: 50";
                }
            }

            int switcher = 1;
            List<string> href1 = new List<string>();
            GetURLs(doc, switcher, href1);
            switcher = 1;
            GetURLs(doc2, switcher, href1);
            switcher = 1;
            if (pagecount == 3)
            {
                GetURLs(doc3, switcher, href1);
            }

            var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });
            var NameAndTPEAndURL = NameAndTPE.Zip(href1, (x, y) => new { NameAndTPE = x, href1 = y });

            System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + teamAbrv+"Players.txt");
            foreach (var nt in NameAndTPEAndURL)
            {
                file1.WriteLine(nt.NameAndTPE.PlayerNames.InnerText + "," + nt.NameAndTPE.PlayerTPE.InnerText + ", " + nt.href1.ToString() + ",");
            }

            CloseAndDispose(file1);

            string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + teamAbrv+"Players.txt");
            List<int> Total = new List<int>();
            foreach (string line in SJSTPETotal)
            {
                string[] splitwords = line.Split(',');
                string[] splitAgain = splitwords[1].Split(':');
                Total.Add(Convert.ToInt32(splitAgain[1]));
            }

            System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + teamAbrv+"Team.txt");

            file2.WriteLine(teamName+"' TPE: " + Total.Sum());
            CloseAndDispose(file2);
        }

        private static void CloseAndDispose(System.IO.StreamWriter file1)
        {
            file1.Close();
            file1.Dispose();
        }

        private static List<HtmlNode> GetNodes(HtmlDocument document, string config)
        {
            return document.DocumentNode
                                .SelectNodes(config.ToString()).ToList();
        }

        private static void GetURLs(HtmlDocument document, int switcher, List<string> href1)
        {
            foreach (HtmlNode node in document.DocumentNode.SelectNodes(ConfigurationManager.AppSettings["PlayerURL"]))
            {
                if (switcher == 1)
                {
                    String[] x = node.OuterHtml.Split('"');

                    href1.Add(x[1].ToString());
                    switcher *= -1;
                }
                else
                {
                    switcher *= -1;
                }
            }
        }

        private static void RemoveRepliesFromPlayerNames(List<HtmlAgilityPack.HtmlNode> PlayerNames)
        {
            int pos = 0;
            for (int i = 0; i < PlayerNames.Count; i += 2, pos++)
            {
                PlayerNames[pos] = PlayerNames[i];
            }
            PlayerNames.RemoveRange(pos, PlayerNames.Count - pos);
        }
    }
}
