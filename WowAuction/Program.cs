using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Data.SqlClient;


namespace WowAuction
{
    class Program
    {


        static RealmAuction LoadAuctions(string file)
        {
            string input = File.ReadAllText(file);

            RealmAuction tmpRoot = new RealmAuction();

            tmpRoot = JsonConvert.DeserializeObject<RealmAuction>(input);

            return tmpRoot;
        }

        static DateTime UnixToDate(long time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(time/1000);
        }

        static void NewHordeAuctions(RealmAuction tmpRoot, SqlConnection currentConnection, int entryNumber, long timeStamp)
        {
            
            SqlCommand NewHordeCommand = new SqlCommand(
                "INSERT INTO [dbo].[Horde_auctions]" +
                   "([AuctionNumber]" +
                   ",[ItemNumber]" +
                   ",[Seller]" +
                   ",[Bid]" +
                   ",[Buyout]" +
                   ",[Quantity]" +
                   ",[TimeLeft]" +
                   ",[TimeStamp])" +
                 "VALUES" +
                       "(" +
                        tmpRoot.horde.auctions[entryNumber].auc.ToString() +
                       "," +
                       tmpRoot.horde.auctions[entryNumber].item.ToString() +
                       ",'" +
                        tmpRoot.horde.auctions[entryNumber].owner +
                       "'," +
                        tmpRoot.horde.auctions[entryNumber].bid.ToString() +
                       "," +
                       tmpRoot.horde.auctions[entryNumber].buyout.ToString() +
                       "," +
                       tmpRoot.horde.auctions[entryNumber].quantity.ToString() +
                       ",'" +
                       tmpRoot.horde.auctions[entryNumber].timeLeft +
                       "'," +
                       timeStamp.ToString() + ")");

            NewHordeCommand.Connection = currentConnection;

            NewHordeCommand.ExecuteNonQuery();

            
        }

        static void UpdateAllHordeAuctions(RealmAuction tmpRealmAuction, SqlConnection currentConnection, long timestamp)
        {
            foreach (Auction2 auction in tmpRealmAuction.horde.auctions)
            {

                SqlCommand UpdateCommand = new SqlCommand(
                    "UPDATE [dbo].[Horde_auctions]" +
                    "SET [AuctionNumber] = " + auction.auc.ToString() +
                        ",[ItemNumber] = " + auction.item.ToString() +
                        ",[Seller] = '" + auction.owner +
                        "',[Bid] = " + auction.bid.ToString() +
                        ",[Buyout] = " + auction.buyout.ToString() +
                        ",[Quantity] = " + auction.quantity.ToString() +
                        ",[TimeLeft] = '" + auction.timeLeft +
                        "',[TimeStamp] = " + timestamp.ToString() +
                    " WHERE [AuctionNumber] = " + auction.auc.ToString());

                UpdateCommand.Connection = currentConnection;

                UpdateCommand.ExecuteNonQuery();

                

            }




        }


        static void Main(string[] args)
        {
            menu programMenu = new menu();

            programMenu.MainMenu();

            bool isNum = false;

            while (true)
            {

                while (!isNum)
                {
                    string consoleInput = Console.ReadLine();

                    isNum = int.TryParse(consoleInput, out programMenu.menuChoice);

                    if (!isNum)
                    {
                        Console.WriteLine("Please choose an activity by writing a number from the list.");
                    }
                }

                programMenu.subMenu();

            }


            /*
            AuctionDownloader theDownloader = new AuctionDownloader();
            
            
            SQLManager SQL = new SQLManager();

            SQL.connection = SQL.DBConnection("TITAN\\SQLEXPRESS", "Auctions");

            SQL.connection.Open();

            SQL.checkTables();

            Auction anAuction = SQL.findAuctionByID(1337, "Alliance");
            

            
            AuctionDlInfo theinfo = theDownloader.AuctionUrl("eu.battle.net", "argent-dawn");

            string url = theinfo.files[0].url;

            long timestamp = theinfo.files[0].lastModified;

            Console.WriteLine(url);
            Console.WriteLine(timestamp);
            Console.WriteLine(UnixToDate(timestamp));
            
            theDownloader.AuctionDataDump(url);

            Console.WriteLine("Data downloaded.");

            RealmAuction auctionData = theDownloader.ParseAuctions();

            Console.WriteLine(auctionData.alliance.auctions.Count.ToString() + " Alliance auctions loaded");
            Console.WriteLine(auctionData.horde.auctions.Count.ToString() + " Horde auctions loaded");
            Console.WriteLine(auctionData.neutral.auctions.Count.ToString() + " Neutral auctions loaded");
            



            

            SQL.connection.Close();
            */

        }
    }
}
