﻿using System;
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

                command = new SqlCommand("CREATE TABLE [dbo].[Alliance]([auc] [bigint] NOT NULL,[item] [int] NULL,[owner] [text] NULL,[bid] [bigint] NULL,[buyout] [bigint] NULL,[quantity] [int] NULL,[timeleft] [bigint] NULL, CONSTRAINT [PK_Alliance] PRIMARY KEY CLUSTERED ([auc] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]", connection);

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

                command = new SqlCommand("CREATE TABLE [dbo].[Horde]([auc] [bigint] NOT NULL,[item] [int] NULL,[owner] [text] NULL,[bid] [bigint] NULL,[buyout] [bigint] NULL,[quantity] [int] NULL,[timeleft] [bigint] NULL, CONSTRAINT [PK_Horde] PRIMARY KEY CLUSTERED ([auc] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]", connection);

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

                command = new SqlCommand("CREATE TABLE [dbo].[Neutral]([auc] [bigint] NOT NULL,[item] [int] NULL,[owner] [text] NULL,[bid] [bigint] NULL,[buyout] [bigint] NULL,[quantity] [int] NULL,[timeleft] [bigint] NULL, CONSTRAINT [PK_Neutral] PRIMARY KEY CLUSTERED ([auc] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]", connection);

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


    }
}
