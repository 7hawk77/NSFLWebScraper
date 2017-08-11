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

            #region Sabercats
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["SJS1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["SJS2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = null;
                List<HtmlNode> PlayerNames = GetNodes(doc, ConfigurationManager.AppSettings["PlayerNodes"].ToString());
                PlayerNames.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                int pagecount = (Convert.ToInt32(ConfigurationManager.AppSettings["SJSPageCount"]));

                if (pagecount == 3)
                {
                    doc3 = web.Load(ConfigurationManager.AppSettings["SJS3"].ToString());
                    PlayerNames.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());
                }
                
                PlayerNames.ForEach(x => x.InnerHtml = x.InnerHtml.Replace("&#39;", "'"));

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = GetNodes(doc, ConfigurationManager.AppSettings["TPENodes"].ToString());

                PlayerTPE.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                if (pagecount == 3)
                {
                    PlayerTPE.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());
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

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });
                var NameAndTPEAndURL = NameAndTPE.Zip(href1, (x, y) => new { NameAndTPE = x, href1 = y });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "SJSPlayers.txt");
                foreach (var nt in NameAndTPEAndURL)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.NameAndTPE.PlayerNames.InnerText + "," + nt.NameAndTPE.PlayerTPE.InnerText + ", " + nt.href1.ToString() + ",");
                }

                CloseAndDispose(file1);

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "SJSPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "SJSTeam.txt");

                file2.WriteLine("San Jose Sabercats' TPE: " + Total.Sum());
                CloseAndDispose(file2);
            }
            #endregion

            #region Hawks
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["BH1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["BH2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = null;
                List<HtmlNode> PlayerNames = GetNodes(doc, ConfigurationManager.AppSettings["PlayerNodes"].ToString());
                PlayerNames.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                int pagecount = (Convert.ToInt32(ConfigurationManager.AppSettings["BHPageCount"]));

                if (pagecount == 3)
                {
                    doc3 = web.Load(ConfigurationManager.AppSettings["BH3"].ToString());
                    PlayerNames.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());
                }

                PlayerNames.ForEach(x => x.InnerHtml = x.InnerHtml.Replace("&#39;", "'"));

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = GetNodes(doc, ConfigurationManager.AppSettings["TPENodes"].ToString());

                PlayerTPE.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                if (pagecount == 3)
                {
                    PlayerTPE.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());
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

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });
                var NameAndTPEAndURL = NameAndTPE.Zip(href1, (x, y) => new { NameAndTPE = x, href1 = y });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "BHPlayers.txt");
                foreach (var nt in NameAndTPEAndURL)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.NameAndTPE.PlayerNames.InnerText + "," + nt.NameAndTPE.PlayerTPE.InnerText + ", " + nt.href1.ToString() + ",");
                }

                CloseAndDispose(file1);

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "BHPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "BHTeam.txt");

                file2.WriteLine("Baltimore Hawks' TPE: " + Total.Sum());
                CloseAndDispose(file2);
            }
            #endregion

            #region Yeti
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["CY1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["CY2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = null;
                List<HtmlNode> PlayerNames = GetNodes(doc, ConfigurationManager.AppSettings["PlayerNodes"].ToString());
                PlayerNames.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                int pagecount = (Convert.ToInt32(ConfigurationManager.AppSettings["CYPageCount"]));

                if (pagecount == 3)
                {
                    doc3 = web.Load(ConfigurationManager.AppSettings["CY3"].ToString());
                    PlayerNames.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());
                }

                PlayerNames.ForEach(x => x.InnerHtml = x.InnerHtml.Replace("&#39;", "'"));

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = GetNodes(doc, ConfigurationManager.AppSettings["TPENodes"].ToString());

                PlayerTPE.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                if (pagecount == 3)
                {
                    PlayerTPE.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());
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

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });
                var NameAndTPEAndURL = NameAndTPE.Zip(href1, (x, y) => new { NameAndTPE = x, href1 = y });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "CYPlayers.txt");
                foreach (var nt in NameAndTPEAndURL)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.NameAndTPE.PlayerNames.InnerText + "," + nt.NameAndTPE.PlayerTPE.InnerText + ", " + nt.href1.ToString() + ",");
                }

                CloseAndDispose(file1);

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "CYPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "CYTeam.txt");

                file2.WriteLine("Colorado Yetis' TPE: " + Total.Sum());
                CloseAndDispose(file2);
            }

            #endregion

            #region Liberty
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["PL1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["PL2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = null;
                List<HtmlNode> PlayerNames = GetNodes(doc, ConfigurationManager.AppSettings["PlayerNodes"].ToString());
                PlayerNames.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                int pagecount = (Convert.ToInt32(ConfigurationManager.AppSettings["PLPageCount"]));

                if (pagecount == 3)
                {
                    doc3 = web.Load(ConfigurationManager.AppSettings["PL3"].ToString());
                    PlayerNames.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());
                }

                PlayerNames.ForEach(x => x.InnerHtml = x.InnerHtml.Replace("&#39;", "'"));

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = GetNodes(doc, ConfigurationManager.AppSettings["TPENodes"].ToString());

                PlayerTPE.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                if (pagecount == 3)
                {
                    PlayerTPE.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());
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

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });
                var NameAndTPEAndURL = NameAndTPE.Zip(href1, (x, y) => new { NameAndTPE = x, href1 = y });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "PLPlayers.txt");
                foreach (var nt in NameAndTPEAndURL)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.NameAndTPE.PlayerNames.InnerText + "," + nt.NameAndTPE.PlayerTPE.InnerText + ", " + nt.href1.ToString() + ",");
                }

                CloseAndDispose(file1);

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "PLPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "PLTeam.txt");

                file2.WriteLine("Philadelphia Liberty' TPE: " + Total.Sum());
                CloseAndDispose(file2);
            }
            #endregion

            #region Wraiths
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["YW1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["YW2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = null;
                List<HtmlNode> PlayerNames = GetNodes(doc, ConfigurationManager.AppSettings["PlayerNodes"].ToString());
                PlayerNames.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                int pagecount = (Convert.ToInt32(ConfigurationManager.AppSettings["YWPageCount"]));

                if (pagecount == 3)
                {
                    doc3 = web.Load(ConfigurationManager.AppSettings["YW3"].ToString());
                    PlayerNames.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());
                }

                PlayerNames.ForEach(x => x.InnerHtml = x.InnerHtml.Replace("&#39;", "'"));

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = GetNodes(doc, ConfigurationManager.AppSettings["TPENodes"].ToString());

                PlayerTPE.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                if (pagecount == 3)
                {
                    PlayerTPE.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());
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

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });
                var NameAndTPEAndURL = NameAndTPE.Zip(href1, (x, y) => new { NameAndTPE = x, href1 = y });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "YWPlayers.txt");
                foreach (var nt in NameAndTPEAndURL)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.NameAndTPE.PlayerNames.InnerText + "," + nt.NameAndTPE.PlayerTPE.InnerText + ", " + nt.href1.ToString() + ",");
                }

                CloseAndDispose(file1);

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "YWPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                        Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "YWTeam.txt");

                file2.WriteLine("Yellowknife Wraiths' TPE: " + Total.Sum());
                CloseAndDispose(file2);
            }
            #endregion

            #region Outlaws
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["AO1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["AO2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = null;
                List<HtmlNode> PlayerNames = GetNodes(doc, ConfigurationManager.AppSettings["PlayerNodes"].ToString());
                PlayerNames.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                int pagecount = (Convert.ToInt32(ConfigurationManager.AppSettings["AOPageCount"]));

                if (pagecount == 3)
                {
                    doc3 = web.Load(ConfigurationManager.AppSettings["AO3"].ToString());
                    PlayerNames.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());
                }

                PlayerNames.ForEach(x => x.InnerHtml = x.InnerHtml.Replace("&#39;", "'"));

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = GetNodes(doc, ConfigurationManager.AppSettings["TPENodes"].ToString());

                PlayerTPE.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                if (pagecount == 3)
                {
                    PlayerTPE.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());
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

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });
                var NameAndTPEAndURL = NameAndTPE.Zip(href1, (x, y) => new { NameAndTPE = x, href1 = y });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "AOPlayers.txt");
                foreach (var nt in NameAndTPEAndURL)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.NameAndTPE.PlayerNames.InnerText + "," + nt.NameAndTPE.PlayerTPE.InnerText + ", " + nt.href1.ToString() + ",");
                }

                CloseAndDispose(file1);

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "AOPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "AOTeam.txt");

                file2.WriteLine("Arizona Outlaws' TPE: " + Total.Sum());
                CloseAndDispose(file2);
            }
            #endregion

            #region Legion
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["AO1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["AO2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = null;
                List<HtmlNode> PlayerNames = GetNodes(doc, ConfigurationManager.AppSettings["PlayerNodes"].ToString());
                PlayerNames.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                int pagecount = (Convert.ToInt32(ConfigurationManager.AppSettings["AOPageCount"]));

                if (pagecount == 3)
                {
                    doc3 = web.Load(ConfigurationManager.AppSettings["AO3"].ToString());
                    PlayerNames.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());
                }

                PlayerNames.ForEach(x => x.InnerHtml = x.InnerHtml.Replace("&#39;", "'"));

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = GetNodes(doc, ConfigurationManager.AppSettings["TPENodes"].ToString());

                PlayerTPE.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                if (pagecount == 3)
                {
                    PlayerTPE.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());
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

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });
                var NameAndTPEAndURL = NameAndTPE.Zip(href1, (x, y) => new { NameAndTPE = x, href1 = y });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "AOPlayers.txt");
                foreach (var nt in NameAndTPEAndURL)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.NameAndTPE.PlayerNames.InnerText + "," + nt.NameAndTPE.PlayerTPE.InnerText + ", " + nt.href1.ToString() + ",");
                }

                CloseAndDispose(file1);

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "AOPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "AOTeam.txt");

                file2.WriteLine("Arizona Outlaws' TPE: " + Total.Sum());
                CloseAndDispose(file2);
            }

            #endregion

            #region Otters
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["OCO1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["OCO2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = null;
                List<HtmlNode> PlayerNames = GetNodes(doc, ConfigurationManager.AppSettings["PlayerNodes"].ToString());
                PlayerNames.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                int pagecount = (Convert.ToInt32(ConfigurationManager.AppSettings["OCOPageCount"]));

                if (pagecount == 3)
                {
                    doc3 = web.Load(ConfigurationManager.AppSettings["OCO3"].ToString());
                    PlayerNames.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());
                }

                PlayerNames.ForEach(x => x.InnerHtml = x.InnerHtml.Replace("&#39;", "'"));

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = GetNodes(doc, ConfigurationManager.AppSettings["TPENodes"].ToString());

                PlayerTPE.AddRange(GetNodes(doc2, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                if (pagecount == 3)
                {
                    PlayerTPE.AddRange(GetNodes(doc3, ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());
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

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });
                var NameAndTPEAndURL = NameAndTPE.Zip(href1, (x, y) => new { NameAndTPE = x, href1 = y });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "OCOPlayers.txt");
                foreach (var nt in NameAndTPEAndURL)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.NameAndTPE.PlayerNames.InnerText + "," + nt.NameAndTPE.PlayerTPE.InnerText + ", " + nt.href1.ToString() + ",");
                }

                CloseAndDispose(file1);

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "OCOPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "OCOTeam.txt");

                file2.WriteLine("Orange County Otters' TPE: " + Total.Sum());
                CloseAndDispose(file2);
            }

            #endregion

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
