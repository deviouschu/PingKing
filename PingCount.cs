using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;

namespace PingKing
{

    /// <summary>
    /// Allows the users to set a number of requests when the option -c is used
    /// having a 0 for the amount of counting replys will result in continous ping
    /// </summary>
    class PingCount
    {
        public void PCount(string[] args)
        {
            try
            {
                //user setting count
                var count = Convert.ToInt64(args[2]);
                //ping info
                Ping sendPing = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                var remoteHost = args[0];
                //32 bit data string
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                //timeout reply
                int timeout = 4000;
                PingReply hostAddress = sendPing.Send(remoteHost);
                //status info
                Console.WriteLine("Ping Status To: {0}", remoteHost);
                Console.WriteLine("---------------------------");
                if (count == 0)
                {
                    while (true)
                    {
                        PingReply reply = sendPing.Send(remoteHost, timeout, buffer, options);
                        if (reply.Status == IPStatus.Success)
                        {
                            Console.WriteLine("Address: {0} Time: {1} TTL: {2}", reply.Address, reply.RoundtripTime, reply.Options.Ttl);
                            Thread.Sleep(800);
                        }
                        else
                        {
                            Console.WriteLine(reply.Status);
                            Thread.Sleep(800);
                        }
                    }
                }

                else
                {


                    for (int i = 0; i != count; i++)
                    {
                        PingReply reply = sendPing.Send(remoteHost, timeout, buffer, options);
                        if (reply.Status == IPStatus.Success)
                        {
                            Console.WriteLine("Address: {0} Time: {1} TTL: {2}", reply.Address, reply.RoundtripTime, reply.Options.Ttl);
                            Thread.Sleep(500);
                        }
                        else
                        {
                            Console.WriteLine(reply.Status);
                        }
                    }

                }
            }
            //Ping error for not resolving the host
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

