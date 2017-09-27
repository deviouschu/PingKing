using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;

namespace PingKing
{
    class PingKeepAlive
    {
        /// <summary>
        /// a user can do a continous ping and get a reply(keep alive) every so many seconds back
        /// </summary>
        /// <param name="args"></param>
        public void PingIt(string[] args)
        {
            try
            {
                //user input retry reply
                int sleep = int.Parse(args[2]);
                //converts milliseconds to seconds
                int sleepConvert = (sleep * 1000);
                //ping info
                var remoteHost = args[0];
                Ping sendPing = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                //32 bit string
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                //timeout
                int timeout = 1800;
                PingReply hostAddress = sendPing.Send(remoteHost);
                //ping status
                Console.WriteLine("Ping Status To: {0}", hostAddress.Address);
                Console.WriteLine("---------------------------");
                //reply continous loop
                while (true)
                {
                    PingReply reply = sendPing.Send(remoteHost, timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine("Address: {0} Time: {1} TTL: {2}", reply.Address, reply.RoundtripTime, reply.Options.Ttl);
                        Thread.Sleep(sleepConvert);
                    }
                    else
                    {
                        Console.WriteLine(reply.Status);
                        Thread.Sleep(sleepConvert);
                    }
                }
            }
            //when remote host cannot be resolved
            catch (PingException)
            {
                var remoteHost = args[0];
                Console.WriteLine("Ping Status To: {0}", remoteHost);
                Console.WriteLine("---------------------------");
                Console.WriteLine("Cannot resolve host name");
            }
        }
    }
}
