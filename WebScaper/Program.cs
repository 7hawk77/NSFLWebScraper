using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

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
                HtmlAgilityPack.HtmlDocument doc3 = web.Load(ConfigurationManager.AppSettings["SJS3"].ToString());


                //This selects all of the elements for the player name. Unfortunately it also selects the amount of replies which I will have to remove later.
                var PlayerNames = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList();

                PlayerNames.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                PlayerNames.AddRange(doc3.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList();

                PlayerTPE.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                PlayerTPE.AddRange(doc3.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"]+"SJSPlayers.txt");
                foreach (var nt in NameAndTPE)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.PlayerNames.InnerText + "," + nt.PlayerTPE.InnerText + ",");
                }

                file1.Close();
                file1.Dispose();

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "SJSPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"]+"SJSTeam.txt");

                file2.WriteLine("San Jose Sabercats' TPE: " + Total.Sum());
                file2.Close();
                file2.Dispose();
            }
            #endregion

            #region Hawks
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["BH1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["BH2"].ToString());


                //This selects all of the elements for the player name. Unfortunately it also selects the amount of replies which I will have to remove later.
                var PlayerNames = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList();

                PlayerNames.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList();

                PlayerTPE.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "BHPlayers.txt");
                foreach (var nt in NameAndTPE)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.PlayerNames.InnerText + "," + nt.PlayerTPE.InnerText + ",");
                }

                file1.Close();
                file1.Dispose();

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "BHPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "BHTeam.txt");

                file2.WriteLine("Baltimore Hawk's TPE: " + Total.Sum());
                file2.Close();
                file2.Dispose();
            }

            #endregion

            #region Yeti
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["CY1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["CY2"].ToString());


                //This selects all of the elements for the player name. Unfortunately it also selects the amount of replies which I will have to remove later.
                var PlayerNames = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList();

                PlayerNames.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList();

                PlayerTPE.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "CYPlayers.txt");
                foreach (var nt in NameAndTPE)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.PlayerNames.InnerText + "," + nt.PlayerTPE.InnerText + ",");
                }

                file1.Close();
                file1.Dispose();

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "CYPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "CYTeam.txt");

                file2.WriteLine("Colorado Yeti TPE: " + Total.Sum());
                file2.Close();
                file2.Dispose();
            }

            #endregion

            #region Liberty
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["PL1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["PL2"].ToString());


                //This selects all of the elements for the player name. Unfortunately it also selects the amount of replies which I will have to remove later.
                var PlayerNames = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList();

                PlayerNames.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList();

                PlayerTPE.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "PLPlayers.txt");
                foreach (var nt in NameAndTPE)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.PlayerNames.InnerText + "," + nt.PlayerTPE.InnerText + ",");
                }

                file1.Close();
                file1.Dispose();

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "PLPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "PLTeam.txt");

                file2.WriteLine("Philadelphia Liberty's TPE: " + Total.Sum());
                file2.Close();
                file2.Dispose();
            }

            #endregion

            #region Wraiths
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["YW1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["YW2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = web.Load(ConfigurationManager.AppSettings["YW3"].ToString());


                //This selects all of the elements for the player name. Unfortunately it also selects the amount of replies which I will have to remove later.
                var PlayerNames = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList();

                PlayerNames.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                PlayerNames.AddRange(doc3.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList();

                PlayerTPE.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                PlayerTPE.AddRange(doc3.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "YWPlayers.txt");
                foreach (var nt in NameAndTPE)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.PlayerNames.InnerText + "," + nt.PlayerTPE.InnerText + ",");
                }

                file1.Close();
                file1.Dispose();

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
                file2.Close();
                file2.Dispose();
            }
            #endregion

            #region Outlaws
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["AO1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["AO2"].ToString());
                HtmlAgilityPack.HtmlDocument doc3 = web.Load(ConfigurationManager.AppSettings["AO3"].ToString());


                //This selects all of the elements for the player name. Unfortunately it also selects the amount of replies which I will have to remove later.
                var PlayerNames = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList();

                PlayerNames.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                PlayerNames.AddRange(doc3.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList();

                PlayerTPE.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                PlayerTPE.AddRange(doc3.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "AOPlayers.txt");
                foreach (var nt in NameAndTPE)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.PlayerNames.InnerText + "," + nt.PlayerTPE.InnerText + ",");
                }

                file1.Close();
                file1.Dispose();

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
                file2.Close();
                file2.Dispose();
            }
            #endregion

            #region Legion
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["LVL1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["LVL2"].ToString());


                //This selects all of the elements for the player name. Unfortunately it also selects the amount of replies which I will have to remove later.
                var PlayerNames = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList();

                PlayerNames.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList();

                PlayerTPE.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "LVLPlayers.txt");
                foreach (var nt in NameAndTPE)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.PlayerNames.InnerText + "," + nt.PlayerTPE.InnerText + ",");
                }

                file1.Close();
                file1.Dispose();

                string[] SJSTPETotal = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["LocalPath"] + "LVLPlayers.txt");
                List<int> Total = new List<int>();
                foreach (string line in SJSTPETotal)
                {
                    string[] splitwords = line.Split(',');
                    string[] splitAgain = splitwords[1].Split(':');
                    Total.Add(Convert.ToInt32(splitAgain[1]));
                }

                System.IO.StreamWriter file2 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "LVLTeam.txt");

                file2.WriteLine("Las Vegas Legion's TPE: " + Total.Sum());
                file2.Close();
                file2.Dispose();
            }

            #endregion

            #region Otters
            {
                HtmlAgilityPack.HtmlDocument doc = web.Load(ConfigurationManager.AppSettings["OCO1"].ToString());
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(ConfigurationManager.AppSettings["OCO2"].ToString());


                //This selects all of the elements for the player name. Unfortunately it also selects the amount of replies which I will have to remove later.
                var PlayerNames = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList();

                PlayerNames.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["PlayerNodes"].ToString()).ToList());

                //This removes the reply count from the Player Names list
                RemoveRepliesFromPlayerNames(PlayerNames);

                //This gets the player TPE
                var PlayerTPE = doc.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList();

                PlayerTPE.AddRange(doc2.DocumentNode
                    .SelectNodes(ConfigurationManager.AppSettings["TPENodes"].ToString()).ToList());

                //This combines the player Name and the PlayerName
                var NameAndTPE = PlayerNames.Zip(PlayerTPE, (n, t) => new { PlayerNames = n, PlayerTPE = t });

                System.IO.StreamWriter file1 = new System.IO.StreamWriter(ConfigurationManager.AppSettings["LocalPath"] + "OCOPlayers.txt");
                foreach (var nt in NameAndTPE)
                {
                    //I need to output this to a text file named SabercatsPlayers
                    file1.WriteLine(nt.PlayerNames.InnerText + "," + nt.PlayerTPE.InnerText + ",");
                }

                file1.Close();
                file1.Dispose();

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
                file2.Close();
                file2.Dispose();
            }

            #endregion

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
