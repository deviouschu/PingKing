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

    /// <summary>
    /// Sends a ICMP packet to the remote host and recieve a response back.
    /// </summary>
    public class PingTo
    {
        public void PingIt(string[] args)
        {
            try
            {
                //reply count
                int count = 10;
                //ping info
                var remoteHost = args[0];
                Ping sendPing = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                //the amount of data being sent, 32 bit string.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                //timeout reply
                int timeout = 1800;
                PingReply hostAddress = sendPing.Send(remoteHost);
                //status info
                Console.WriteLine("Ping Status To: {0}", remoteHost);
                Console.WriteLine("---------------------------");
                for (int i = 0; i != count; i++)
                {

                    try
                    {
                        PingReply reply = sendPing.Send(remoteHost, timeout, buffer, options);
                        if (reply.Status == IPStatus.Success)
                        {
                            Console.WriteLine("Address: {0} Time: {1} TTL: {2}", remoteHost, reply.RoundtripTime, reply.Options.Ttl);
                            Thread.Sleep(500);
                        }
                        else
                        {
                            Console.WriteLine(reply.Status);
                        }
                    }
                    catch (PingException)
                    {
                        PingReply reply = sendPing.Send(remoteHost, timeout, buffer, options);
                        Console.WriteLine(reply.Status);
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
    

