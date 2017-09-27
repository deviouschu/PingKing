using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace PingKing
{

    /// <summary>
    /// Scans a number of ports on a single target
    /// </summary>
    class SingleMassPortScan
    {
        // incoming info for scan args[0]hostname args[1]-sp args[2]portStart args[3]-ep args[4]endPort
        //options 
        //-sp(start port) -ep(end port)
        //-thou(frist 1000 ports)
        //-well(well known ports)
        public void ScanPorts(string[] args)
        {
            //host or ip address
            var host = Convert.ToString(args[0]);
            //Stats on targets wether a port is open, closed, or filtered
            List<int> openPorts = new List<int>();
            List<int> closedPorts = new List<int>();
            List<int> filteredPorts = new List<int>();
            try
            {
                //resolving address
                IPAddress[] checkHost = Dns.GetHostAddresses(host);
                //user defined port scan
                if (args.Contains("-sp"))
                {
                    //start and end port
                    var startp = Convert.ToInt32(args[2]);
                    var endp = Convert.ToInt32(args[4]);
                    //estimated time for port scan
                    var amountOfPorts = (endp - startp);
                    var millSecResult = (amountOfPorts * 1200);
                    var expectedTimeSec = (millSecResult / 1000);
                    var expectedTimeMin = (expectedTimeSec / 60);
                    //Stats for ping status
                    Console.WriteLine("Ping Status To: {0} Start Port: {1} End Port: {2}", host, startp, endp);
                    Console.WriteLine("Time until scan is complete {0} mintues or {1} seconds", expectedTimeMin, expectedTimeSec);
                    Console.WriteLine("---------------------------");
                    for (int i = startp; i != endp; i++)
                    {

                        try
                        {
                            IPAddress[] recheck = Dns.GetHostAddresses(host);
                            Socket portPings = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            IAsyncResult waitReply = portPings.BeginConnect(host, startp, null, null);
                            bool success2 = waitReply.AsyncWaitHandle.WaitOne(1200, true);
                            if (portPings.Connected == true)
                            {
                                openPorts.Add(startp);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: OPEN", host, startp, portPings2.Ttl);
                                portPings.Close();
                                //Thread.Sleep(500);
                            }
                            else if (portPings.Connected == false)
                            {
                                closedPorts.Add(startp);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: CLOSED", host, startp, portPings2.Ttl);
                                portPings.Close();
                            }
                        }
                        //socket error for closed or filtered ports
                        catch (SocketException e)
                        {
                            Socket portPings = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            if (e.SocketErrorCode == SocketError.ConnectionRefused)
                            {
                                filteredPorts.Add(startp);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: FILTERED", host, startp, portPings2.Ttl);
                                portPings.Close();
                            }
                            else
                            {
                                closedPorts.Add(startp);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: CLOSED", host, startp, portPings2.Ttl);
                                portPings.Close();
                            }

                        }
                        //going to the next port
                        startp++;
                    }
                    //results after scan
                    foreach (int port in openPorts)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Port {0} Open on {1}", port, host);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("{0} ports closed on {1}", closedPorts.Count, host);
                    Console.WriteLine("");
                    Console.WriteLine("{0} ports filtered on {1}", filteredPorts.Count, host);
                    Console.WriteLine("");
                }
                //scan frist 1000 ports
                if (args.Contains("-thou"))
                {
                    //ports
                    int startport = 1;
                    int endport = 1000;
                    //estimated time
                    var amountOfPorts = (endport - startport);
                    var millSecResult = (amountOfPorts * 1200);
                    var expectedTimeSec = (millSecResult / 1000);
                    var expectedTimeMin = (expectedTimeSec / 60);
                    //Stats for Ping
                    Console.WriteLine("Ping Status To: {0} Start Port: {1} End Port: {2}", host, startport, endport);
                    Console.WriteLine("Time until scan is complete {0} mintues or {1} seconds", expectedTimeMin, expectedTimeSec);
                    Console.WriteLine("---------------------------");
                    for (int ii = startport; startport != endport; ii++)
                    {
                        try
                        {
                            IPAddress[] recheckHost = Dns.GetHostAddresses(host);
                            Socket portPings = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            IAsyncResult waitReply = portPings.BeginConnect(host, startport, null, null);
                            bool success2 = waitReply.AsyncWaitHandle.WaitOne(1200, true);
                            if (portPings.Connected == true)
                            {
                                openPorts.Add(startport);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: OPEN", host, startp, portPings2.Ttl);
                                portPings.Close();
                                //Thread.Sleep(500);
                            }
                            else if (portPings.Connected == false)
                            {
                                closedPorts.Add(startport);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: CLOSED", host, startp, portPings2.Ttl);
                                portPings.Close();
                            }
                        }
                        //socket error for filtered or closed ports
                        catch (SocketException e)
                        {
                            Socket portPings = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            if (e.SocketErrorCode == SocketError.ConnectionRefused)
                            {
                                filteredPorts.Add(startport);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: FILTERED", host, startp, portPings2.Ttl);
                                portPings.Close();
                            }
                            else
                            {
                                closedPorts.Add(startport);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: CLOSED", host, startp, portPings2.Ttl);
                                portPings.Close();
                            }

                        }
                        //going to the next port
                        startport++;
                    }
                    //results
                    foreach (int port in openPorts)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Port {0} Open on {1}", port, host);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("{0} ports closed on {1}", closedPorts.Count, host);
                    Console.WriteLine("");
                    Console.WriteLine("{0} ports filtered on {1}", filteredPorts.Count, host);
                    Console.WriteLine("");
                }
                //scans all well known ports
                if (args.Contains("-well"))
                {
                    //well know ports
                    List<int> WellPorts = new List<int>() { 1,5,7,18,20,21,22,23,25,29,37,42,43,49,53,69,70,79,80,103,108,109,110,115,118,119,137,139,143,150,156,161,179,190,194,197,389,396,443,444,445,458,546,547,563,569,1080,3389};
                    //estimated time
                    var millSecResult = (47 * 1200);
                    var expectedTimeSec = (millSecResult / 1000);
                    var expectedTimeMin = (expectedTimeSec / 60);
                    //ping info
                    Console.WriteLine("Ping Status To: {0} Scanning all well known ports", host);
                    Console.WriteLine("For a list of the well known ports Ping King scans use pk -portlist");
                    Console.WriteLine("Time until scan is complete {0} mintues or {1} seconds", expectedTimeMin, expectedTimeSec);
                    Console.WriteLine("---------------------------");

                    //process for scanning each port
                    foreach (int port in WellPorts)
                    {
                        try
                        {
                            IPAddress[] recheckHost = Dns.GetHostAddresses(host);
                            Socket portPings = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            IAsyncResult waitReply = portPings.BeginConnect(host, port, null, null);
                            bool success2 = waitReply.AsyncWaitHandle.WaitOne(1200, true);
                            if (portPings.Connected == true)
                            {
                                openPorts.Add(port);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: OPEN", host, startp, portPings2.Ttl);
                                portPings.Close();
                                //Thread.Sleep(500);
                            }
                            else if (portPings.Connected == false)
                            {
                                closedPorts.Add(port);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: CLOSED", host, startp, portPings2.Ttl);
                                portPings.Close();
                            }
                        }
                        //socket error if port is closed or filtered
                        catch (SocketException e)
                        {
                            Socket portPings = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            if (e.SocketErrorCode == SocketError.ConnectionRefused)
                            {
                                filteredPorts.Add(port);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: FILTERED", host, startp, portPings2.Ttl);
                                portPings.Close();
                            }
                            else
                            {
                                closedPorts.Add(port);
                                //Console.WriteLine("Address: {0} Port: {1} TTL: {2} Status: CLOSED", host, startp, portPings2.Ttl);
                                portPings.Close();
                            }

                        }
                        
                    }
                    //results
                    foreach (int port in openPorts)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Port {0} Open on {1}", port, host);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("{0} ports are closed on {1}", closedPorts.Count, host);
                    Console.WriteLine("");
                    Console.WriteLine("{0} ports are filtered on {1}", filteredPorts.Count, host);
                    Console.WriteLine("");
                }
            }
            //socket error thrown when unable to resolve
            catch (SocketException)
            {
                Console.WriteLine("Address: {0}", host);
                Console.WriteLine("---------------------------");
                Console.WriteLine("Cannot resolve host name");
            }
        }

    }
}
