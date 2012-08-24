﻿using System;
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
            
            AuctionDownloader theDownloader = new AuctionDownloader();
            SQLManager SQL = new SQLManager();

            SQL.connection = SQL.DBConnection("TITAN\\SQLEXPRESS", "Auctions");

            SQL.connection.Open();

            SQL.checkTables();

            /*
            string url = theDownloader.AuctionUrl("eu.battle.net", "argent-dawn");

            Console.WriteLine(url);

            theDownloader.AuctionDataDump(url);

            Console.WriteLine("Data downloaded.");

            RealmAuction auctionData = theDownloader.ParseAuctions();

            Console.WriteLine(auctionData.alliance.auctions.Count.ToString() + " Alliance auctions loaded");
            Console.WriteLine(auctionData.horde.auctions.Count.ToString() + " Horde auctions loaded");
            Console.WriteLine(auctionData.neutral.auctions.Count.ToString() + " Neutral auctions loaded");
            */



            Console.ReadKey();


        }
    }
}
