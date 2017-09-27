using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Net;

namespace PingKing
{
    class PortKeepAlive
    {

        /// <summary>
        /// allows a user to get a reply(keep alive) back every so many seconds
        /// uses tcp async packets
        /// </summary>
        /// <param name="args"></param>
        public void PingIt(string[] args)
        {

            //incoming format for PortPinger hostname[0] -p[1] portnumber[2] count[3]

            try
            {
                //user input for reply back in seconds
                int sleep = int.Parse(args[2]);
                //converts milliseconds to seconds
                int sleepConvert = (sleep * 1000);
                var host = Convert.ToString(args[0]);
                //tries to resolve the host address
                IPAddress[] ip = Dns.GetHostAddresses(host);
                var port = int.Parse(args[3]);
                //ping status
                Console.WriteLine("Ping Status To: {0} Port: {1}", host, port);
                Console.WriteLine("---------------------------");
                //conintous loop
                while (true)
                {
                    try
                    {
                        IPAddress[] checkHost = Dns.GetHostAddresses(host);
                        Socket portPing = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                        IAsyncResult waiting = portPing.BeginConnect(host, port, null, null);
                        bool success = waiting.AsyncWaitHandle.WaitOne(1000, true);
                        if (portPing.Connected == true)
                        {
                            Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: OPEN", host, port, portPing.Ttl);
                            portPing.Close();
                            Thread.Sleep(500);
                        }
                        else if (portPing.Connected == false)
                        {
                            Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: CLOSED", host, port, portPing.Ttl);
                            portPing.Close();
                        }

                    }
                    catch (SocketException e)
                    {
                        if (e.SocketErrorCode == SocketError.ConnectionRefused)
                        {
                            Socket portPing = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: FILTERED", host, port, portPing.Ttl);
                        }
                        else
                        {
                            Socket portPing = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: CLOSED", host, port, portPing.Ttl);
                        }


                    }
                }
            }
            //socket error when the host cannot be resolved
            catch (SocketException)
            {
                var host = Convert.ToString(args[0]);
                Console.WriteLine("Address: {0}", host);
                Console.WriteLine("---------------------------");
                Console.WriteLine("Cannot resolve host name");
            }
    }
    }
}
