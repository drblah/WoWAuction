using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WowAuction
{
    public class Dlurl
    {
        public string url { get; set; }
        public long lastModified { get; set; }
    }

    public class AuctionDlInfo  
    {
        public List<Dlurl> files { get; set; }
    }
   
}
