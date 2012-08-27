using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace WowAuction
{
    class AuctionDownloader
    {

        //Create the client used to download data.
        WebClient client = new WebClient();


        //Downloads the realm list of a specifyed region. Ie. eu.battle.net or us.battle.net.
        //The list is JSON and is returned as a string.
        public string RealmStatus(string region)
        {
            try
            {
                string reply = client.DownloadString("http://" + region + "/api/wow/realm/status");

                return reply;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            
        }

        /*Requests the url for the newest auction data for a specific realm.
         *Blizzard's API will return the result as JSON. It contains a url or the auction data and a time stamp for when the data was updated.
         *
         * 
        */
        public AuctionDlInfo AuctionUrl(string host, string realm)
        {
            try
            {
                string reply = client.DownloadString("http://" + host + "/api/wow/auction/data/" + realm);

                AuctionDlInfo dlinfo = JsonConvert.DeserializeObject<AuctionDlInfo>(reply);

                return dlinfo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public void AuctionDataDump(string url)
        {
            try
            {
                client.DownloadFile(url, "auctions.json");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public RealmAuction ParseAuctions()
        {
            try
            {
                string data = File.ReadAllText("auctions.json");

                return JsonConvert.DeserializeObject<RealmAuction>(data);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}
