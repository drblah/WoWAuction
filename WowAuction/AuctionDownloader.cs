using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace WowAuction
{
    class AuctionDownloader
    {
        WebClient client = new WebClient();

        public string RealmStatus(string host)
        {
            string reply = client.DownloadString("http://" + host + "/api/wow/realm/status");

            return reply;
        }

        public string AuctionUrl(string host, string realm)
        {

            string reply = client.DownloadString("http://" + host + "/api/wow/auction/data/" + realm);

            return reply;
        }

        public void AuctionDataDump(string url)
        {
            client.DownloadFile(url, "auctions.json");
        }
    }
}
