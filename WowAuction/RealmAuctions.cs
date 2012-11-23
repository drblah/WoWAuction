using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WowAuction
{
    public class Realm
    {
        public string name { get; set; }
        public string slug { get; set; }
    }

    public class Auction
    {
        public long auc { get; set; }
        public int item { get; set; }
        public string owner { get; set; }
        public long bid { get; set; }
        public long buyout { get; set; }
        public int quantity { get; set; }
        public string timeLeft { get; set; }
    }

    public class Alliance
    {
        public List<Auction> auctions { get; set; }
    }

    public class Auction2
    {
        public long auc { get; set; }
        public int item { get; set; }
        public string owner { get; set; }
        public long bid { get; set; }
        public long buyout { get; set; }
        public int quantity { get; set; }
        public string timeLeft { get; set; }
    }

    public class Horde
    {
        public List<Auction2> auctions { get; set; }
    }

    public class Auction3
    {
        public long auc { get; set; }
        public int item { get; set; }
        public string owner { get; set; }
        public long bid { get; set; }
        public long buyout { get; set; }
        public int quantity { get; set; }
        public string timeLeft { get; set; }
    }

    public class Neutral
    {
        public List<Auction3> auctions { get; set; }
    }

    public class RealmAuction
    {
        public Realm realm { get; set; }
        public Alliance alliance { get; set; }
        public Horde horde { get; set; }
        public Neutral neutral { get; set; }
    }

}
