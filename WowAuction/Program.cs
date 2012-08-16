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
            /*
            RealmAuction AuctionData = new RealmAuction();

            AuctionData = LoadAuctions("input.txt");

            SqlConnection myConnection = new SqlConnection("user id=username;" +
                                       "password=password;server=WANDERER\\SQLEXPRESS;" +
                                       "Trusted_Connection=yes;" +
                                       "database=auctions; " +
                                       "connection timeout=5");

            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            for (int updates = 0; updates < 10; updates++)
            {
                NewHordeAuctions(AuctionData, myConnection, updates, 10);
            }
            */

            /*
            AuctionDownloader test = new AuctionDownloader();

            AuctionDlInfo myInfo = new AuctionDlInfo();

            myInfo = JsonConvert.DeserializeObject<AuctionDlInfo>(test.AuctionUrl("eu.battle.net", "argent-dawn"));

            test.AuctionDataDump(myInfo.files[0].url);
            */
            long timestamp = 1337;  //myInfo.files[0].lastModified;

            RealmAuction AuctionData = new RealmAuction();

            AuctionData = LoadAuctions("auctions.json");

            SqlConnection myConnection = new SqlConnection("user id=username;" +
                                       "password=password;server=WANDERER\\SQLEXPRESS;" +
                                       "Trusted_Connection=yes;" +
                                       "database=auctions; " +
                                       "connection timeout=5");

            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            UpdateAllHordeAuctions(AuctionData, myConnection, timestamp);

            /*
            for (int item = 0; item < AuctionData.horde.auctions.Count; item++)
            {
                NewHordeAuctions(AuctionData, myConnection, item, timestamp);

                if (item % 10 == 0)
                {
                    Console.WriteLine(item + " added.");
                }
            }
            */
            Console.WriteLine(timestamp);
            Console.WriteLine("Done!");
            

            Console.ReadLine();

            myConnection.Close();
        }
    }
}
