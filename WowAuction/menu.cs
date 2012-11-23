using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace WowAuction
{
    class menu
    {
        // Variable, used to indicate which menu item the user choose.
        public int menuChoice = 0;

        private long globalTimestamp = 0;

        // Clears the screen and draws the main menu.
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to WowAuction!");
            Console.WriteLine("\n1. Download new Auction data");
            Console.WriteLine("\n2. Load Auctions");

            Console.WriteLine("\n0. Exit application");
            Console.WriteLine("\nPlease choose an activity:");

            
        }

        // Prints a sub-menu based uppon the menuChoice variable.
        public void subMenu()
        {
            switch (menuChoice)
            {
                case 0:
                    Environment.Exit(0);
                    break;

                case 1:
                    AuctionDownloader downloader = new AuctionDownloader();

                    AuctionDlInfo auctionInfo = downloader.AuctionUrl("eu.battle.net", "argent-dawn");

                    string url = auctionInfo.files[0].url;
                    long timestamp = auctionInfo.files[0].lastModified;

                    globalTimestamp = timestamp;

                    Console.WriteLine(url);
                    Console.WriteLine(timestamp);
                    Console.WriteLine(UnixToDate(timestamp));
            
                    downloader.AuctionDataDump(url);

                    try
                    {
                        int fileSize = File.ReadAllBytes("auctions.json").Length;
                        Console.WriteLine("Downloaded " + fileSize + " bytes.");

                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);

                        Console.ReadLine();

                        break;
                    }

                    break;

                case 2:
                    SQLManager database = new SQLManager();
                    database.connection = database.DBConnection("TITAN\\SQLEXPRESS", "Auctions");

                    database.connection.Open();

                    database.checkTables();

                    try
                    {
                        string data = File.ReadAllText("auctions.json");

                        RealmAuction auctions = JsonConvert.DeserializeObject<RealmAuction>(data);

                        Console.WriteLine(auctions.alliance.auctions.Count.ToString() + " Alliance auctions loaded");
                        Console.WriteLine(auctions.horde.auctions.Count.ToString() + " Horde auctions loaded");
                        Console.WriteLine(auctions.neutral.auctions.Count.ToString() + " Neutral auctions loaded");

                        database.UpdateAuctions(auctions, globalTimestamp);

                        Console.WriteLine("\nPress any key to continue.");

                        Console.ReadKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                        Console.ReadLine();
                    }

                    database.connection.Close();

                    break;

                default:
                    Console.WriteLine("Please choose an activity by writing a number from the list.");
                    break;
            }

        }

        DateTime UnixToDate(long time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(time / 1000);
        }
    }
}
