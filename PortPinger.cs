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

    /// <summary>
    /// sends a async tcp syn packet to check if the host port is up, also checking the host if ICMP is blocked
    /// </summary>
    class PortPinger
    {
        public void PingIt(string[] args)
        {

            //incoming format for PortPinger hostname[0] -p[1] portnumber[2] count[3]
            //getting ip address
            try
            {
                //tries to resolve the host frist
                var host = Convert.ToString(args[0]);
                IPAddress[] hostCheck = Dns.GetHostAddresses(host);
                var port = int.Parse(args[2]);

                Console.WriteLine("Ping Status To: {0} Port: {1}", host, port);
                Console.WriteLine("---------------------------");
                //default ping
                if (args.Length == 3)
                {

                    for (int i = 0; i != 4; i++)
                    {
                        try
                        {
                            //resolves the host name
                            IPAddress[] ip = Dns.GetHostAddresses(host);
                            Socket portPing = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                            IAsyncResult waiting = portPing.BeginConnect(host, port, null, null);
                            bool success = waiting.AsyncWaitHandle.WaitOne(1200, true);

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

                //continous ping
                else if (Convert.ToInt64(args[3]) == 0)
                {
                    while (true)
                    {

                        try
                        {
                            //resolves the host name
                            IPAddress[] ip = Dns.GetHostAddresses(host);
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

                //user customer reply count
                else
                {
                    var count = Convert.ToInt64(args[3]);
                    for (int i = 0; i != count; i++)
                    {
                        try
                        {
                            //resolves the host name
                            IPAddress[] ip = Dns.GetHostAddresses(host);
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
            }

            //Socket error when the host cannot be resolved
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
    

    
