using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace WowAuction
{
    class SQLManager
    {

        public SqlConnection connection;

        public SqlConnection DBConnection(string address, string database)
        {
            string connectionString =   "server=" + address + ";" +
                                        "Trusted_Connection=yes;" +
                                        "database=" + database + ";";

                return new SqlConnection(connectionString); 
        }


        public void checkTables()
        {


            SqlCommand command = new SqlCommand("SELECT * FROM sys.tables WHERE name = 'Alliance'", connection);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            if (reader.HasRows == true)
            {
                Console.WriteLine("Alliance table exists.");
            }
            else
            {
                Console.WriteLine("Alliance table is missing. Attempting to create it");
                reader.Close();

                command = new SqlCommand("CREATE TABLE [dbo].[Alliance]([auc] [bigint] NOT NULL,[item] [int] NULL,[owner] [text] NULL,[bid] [bigint] NULL,[buyout] [bigint] NULL,[quantity] [int] NULL,[timeleft] [Text] NULL,[timestamp] [bigint] NULL , CONSTRAINT [PK_Alliance] PRIMARY KEY CLUSTERED ([auc] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]", connection);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Alliance table successfully created.");
                }
                catch
                {
                    Console.WriteLine("Failed to create Alliance table");
                }

            }

            reader.Close();

            command = new SqlCommand("SELECT * FROM sys.tables WHERE name = 'Horde'", connection);

            reader = command.ExecuteReader();

            reader.Read();

            if (reader.HasRows == true)
            {
                Console.WriteLine("Horde table exists.");
            }
            else
            {
                Console.WriteLine("Horde table is missing. Attempting to create it");
                reader.Close();

                command = new SqlCommand("CREATE TABLE [dbo].[Horde]([auc] [bigint] NOT NULL,[item] [int] NULL,[owner] [text] NULL,[bid] [bigint] NULL,[buyout] [bigint] NULL,[quantity] [int] NULL,[timeleft] [Text] NULL,[timestamp] [bigint] NULL , CONSTRAINT [PK_Horde] PRIMARY KEY CLUSTERED ([auc] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]", connection);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Horde table successfully created.");
                }
                catch
                {
                    Console.WriteLine("Failed to create Horde table");
                }

            }

            reader.Close();


            command = new SqlCommand("SELECT * FROM sys.tables WHERE name = 'Neutral'", connection);

            reader = command.ExecuteReader();

            reader.Read();

            if (reader.HasRows == true)
            {
                Console.WriteLine("Neutral table exists.");
            }
            else
            {
                Console.WriteLine("Neutral table is missing. Attempting to create it");
                reader.Close();

                command = new SqlCommand("CREATE TABLE [dbo].[Neutral]([auc] [bigint] NOT NULL,[item] [int] NULL,[owner] [text] NULL,[bid] [bigint] NULL,[buyout] [bigint] NULL,[quantity] [int] NULL,[timeleft] [Text] NULL,[timestamp] [bigint] NULL , CONSTRAINT [PK_Neutral] PRIMARY KEY CLUSTERED ([auc] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]", connection);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Neutral table successfully created.");
                }
                catch
                {
                    Console.WriteLine("Failed to create Neutral table");
                }

            }

            reader.Close();
             
              
             
        }

        public void UpdateAuctions(RealmAuction auctions, long currentTimestamp)
        {
            for (int counter = 0; counter < auctions.alliance.auctions.Count(); counter++)
            {
                SqlCommand command = new SqlCommand("SELECT auc FROM Alliance WHERE auc = " + auctions.alliance.auctions[counter].auc.ToString(), connection);
                
                Auction currentAuction = auctions.alliance.auctions[counter];

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close();
                    Console.WriteLine("\nNo auction found");

                    SqlCommand createAuction = new SqlCommand("INSERT INTO [dbo].[Alliance]" +
                                                                "([auc]" +
                                                                ",[item]" +
                                                                ",[owner]" +
                                                                ",[bid]" +
                                                                ",[buyout]" +
                                                                ",[quantity]" +
                                                                ",[timeleft]" +
                                                                ",[timestamp])" +
                                                                "VALUES" +
                                                                    "(" +
                                                                    currentAuction.auc.ToString() +
                                                                    "," +
                                                                    currentAuction.item.ToString() +
                                                                    ",'" +
                                                                    currentAuction.owner +
                                                                    "'," +
                                                                    currentAuction.bid.ToString() +
                                                                    "," +
                                                                    currentAuction.buyout.ToString() +
                                                                    "," +
                                                                    currentAuction.quantity.ToString() +
                                                                    ",'" +
                                                                    currentAuction.timeLeft +
                                                                    "'," +
                                                                    currentTimestamp.ToString() + ")", connection);

                    createAuction.ExecuteNonQuery();

                    
                }
                else
                {
                    reader.Close();
                    Console.WriteLine("\nAuction found");

                    SqlCommand updateAuction = new SqlCommand("UPDATE [dbo].[Alliance]" +
                                                               "SET [auc] = " + currentAuction.auc.ToString() +
                                                                  " ,[item] = " + currentAuction.item.ToString() +
                                                                  " ,[owner] = '" + currentAuction.owner +
                                                                  "' ,[bid] = " + currentAuction.bid.ToString() +
                                                                  " ,[buyout] = " + currentAuction.buyout.ToString() +
                                                                  " ,[quantity] = " + currentAuction.quantity.ToString() +
                                                                  " ,[timeleft] = '" + currentAuction.timeLeft +
                                                                  "' ,[timestamp] = " + currentTimestamp.ToString() +
                                                             "WHERE auc = " + currentAuction.auc.ToString(), connection);

                    updateAuction.ExecuteNonQuery();
                }

                
            }
        }


        public Auction findAuctionByID(long id, string faciton)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM " + faciton + " WHERE auc = " + id, connection);

            SqlDataReader reader = command.ExecuteReader();

            Auction foundAuction = new Auction();

            while (reader.Read())
            {
                if (reader.FieldCount == 7)
                {
                    

                    foundAuction.auc = (long)reader["auc"];
                    foundAuction.item = (int)reader["item"];
                    foundAuction.owner = (string)reader["owner"];
                    foundAuction.bid = (long)reader["bid"];
                    foundAuction.buyout = (long)reader["buyout"];
                    foundAuction.quantity = (int)reader["quantity"];
                    foundAuction.timeLeft = reader["timeleft"].ToString();
                    

                    for (int data = 0; data < 7; data++)
                    {
                        Console.Write(reader[data] + " ");
                    }
                }
            }

            reader.Close();

            return foundAuction;

        }

    }
}
