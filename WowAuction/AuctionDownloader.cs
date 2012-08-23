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
        WebClient client = new WebClient();

        public string RealmStatus(string host)
        {
            try
            {
                string reply = client.DownloadString("http://" + host + "/api/wow/realm/status");

                return reply;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            
        }

        public string AuctionUrl(string host, string realm)
        {
            try
            {
                string reply = client.DownloadString("http://" + host + "/api/wow/auction/data/" + realm);

                AuctionDlInfo dlinfo = JsonConvert.DeserializeObject<AuctionDlInfo>(reply);

                return dlinfo.files[0].url;
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
