using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;


namespace PingKing
{
    class GetPublicAddress
    {
        public void PublicAddress(string[] args)
        {
                var exAddress = args[0];
                string GetExAddress = new WebClient().DownloadString("http://ipinfo.io/ip");
                Console.WriteLine("YOUR EXTERNAL IP ADDRESS:{0}", GetExAddress);
        }
    }
}
