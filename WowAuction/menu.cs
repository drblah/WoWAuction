using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WowAuction
{
    class menu
    {
        public int menuChoice = 0;

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to WowAuction!");
            Console.WriteLine("\n1. Download new Auction data");

            Console.WriteLine("\n0. Exit application");
            Console.WriteLine("\nPlease choose an activity:");

            
        }

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

                    Console.WriteLine(url);
                    Console.WriteLine(timestamp);
                    Console.WriteLine(UnixToDate(timestamp));
            
                    downloader.AuctionDataDump(url);

                    try
                    {
                        int fileSize = File.ReadAllBytes("auctions.txt").Length;
                        Console.WriteLine("Downloaded " + fileSize + " bytes.");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }

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
