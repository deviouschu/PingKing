using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Management.Automation;
namespace PingKing
{
    class Program
    {
      /// <summary>
      /// This app allows a user to ping a host with fast results, you can also send tcp packets to check ports
      /// using -p will hit a port and give whether it is open or closed
      /// using -c will do a reply count using 0 will result in continous ping
      /// using -k will do a ping reply every so many seconds
      /// using -pk will do a ping to the port you are requesting every so many seconds
      /// using -getex will go out and grab your external ip address
      /// </summary>
      /// <param name="args"></param>
        
        static void Main(string [] args) 
        {
            
            //version and author
            Console.WriteLine("PingKing ver 1.2.4");
            Console.WriteLine("Developer: Devious Chu ^^");
            Console.WriteLine("");
            try
            {
                //args options
                var count = "-c";
                var port = "-p";
                var keepAlive = "-k";
                var portAlive = "-pk";
                var singlePortTargeting = "-sp";
                var WKnownSinglePortScan = "-well";
                var top1000PortScan = "-thou";
                var helpWKnownPortList = "-portlist";
                var getExternalAddress = "-getex";
                //var networkScan = "-ns";

                //looking for the right option
                if (args == null || args.Length == 0)
                {
                    Console.WriteLine("Help Menu");
                    Console.WriteLine("---------");
                    Console.WriteLine("ONLY ONE OPTION AT A TIME");
                    Console.WriteLine("");
                    Console.WriteLine("Usage: pk hostname");
                    Console.WriteLine("Usage: pk hostname [options]");
                    Console.WriteLine("");
                    Console.WriteLine("ICMP PING");
                    Console.WriteLine("----------");
                    Console.WriteLine("-c (number) Count, Get a number of ping replys back");
                    Console.WriteLine("using 0 results in continuous ping!");
                    Console.WriteLine("");
                    Console.WriteLine("-k (seconds to ping) Keep Alive, ping the host every so many seconds (continuous ping) ");
                    Console.WriteLine("To Cancel keep alive use ctrl c");
                    Console.WriteLine("");
                    Console.WriteLine("GET PUBLIC IP ADDRESS");
                    Console.WriteLine("----------------------");
                    Console.WriteLine("-getex");
                    Console.WriteLine("The command will go and grab your");
                    Console.WriteLine("external ip address");
                    Console.WriteLine("");
                    Console.WriteLine("SINGLE TARGET PORT PING");
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("-p (port number) Port you want to ping");
                    Console.WriteLine("enter another number after the port number for ping count");
                    Console.WriteLine("example pk google.com -p 80(port) 5(reply count)");
                    Console.WriteLine("using 0 results in continuous ping");
                    Console.WriteLine("");
                    Console.WriteLine("-pk (seconds to ping) Port Keep Alive, ping the port every so many seconds (continuous port ping)");
                    Console.WriteLine("port number is after the amount of seconds to keep alive");
                    Console.WriteLine("example pk google.com -pk 5(seconds) 80(port)");
                    Console.WriteLine("To Cancel keep alive use ctrl c");
                    Console.WriteLine("");
                    Console.WriteLine("Single Target Mass port Scan");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("-well Scans for open well known ports");
                    Console.WriteLine("example pk google.com -well");
                    Console.WriteLine("");
                    Console.WriteLine("-thou Scans frist 1000 ports 1-1000");
                    Console.WriteLine("example pk google.com -thou");
                    Console.WriteLine("");
                    Console.WriteLine("Range Scan use -sp(starting port) with -ep(end port)");
                    Console.WriteLine("Range Scan will start at the port you placed in and scan to the end port you placed");
                    Console.WriteLine("example pk google.com -sp 1000 -ep 30000, will scan from 1000 to 30000");
                    Console.WriteLine("");
                    Console.WriteLine("OTHER OPTIONS");
                    Console.WriteLine("---------------");
                    Console.WriteLine("-portlist Prints out well known ports");
                    Console.WriteLine("");
                    Console.WriteLine("to cancel the app at anytime use ctrl c");

                }

                if (args.Contains(count))
                {
                    PingCount cping = new PingCount();
                    cping.PCount(args);
                }

                else if (args.Contains(port))
                {
                    PortPinger portping = new PortPinger();
                    portping.PingIt(args);
                }

                else if (args.Contains(keepAlive))
                {
                    PingKeepAlive keepA = new PingKeepAlive();
                    keepA.PingIt(args);
                }

                else if (args.Contains(portAlive))
                {
                    PortKeepAlive pkAlive = new PortKeepAlive();
                    pkAlive.PingIt(args);
                }

                else if (args.Contains(singlePortTargeting))
                {
                    SingleMassPortScan MassScanPorts = new SingleMassPortScan();
                    MassScanPorts.ScanPorts(args);
                }

                else if (args.Contains(WKnownSinglePortScan))
                {
                    SingleMassPortScan MassScanPorts = new SingleMassPortScan();
                    MassScanPorts.ScanPorts(args);
                }

                else if (args.Contains(top1000PortScan))
                {
                    SingleMassPortScan MassScanPorts = new SingleMassPortScan();
                    MassScanPorts.ScanPorts(args);
                }
                else if (args.Contains(getExternalAddress))
                {
                    GetPublicAddress GetExternal = new GetPublicAddress();
                    GetExternal.PublicAddress(args);
                }
                else if (args.Contains(helpWKnownPortList))
                {
                    Console.WriteLine("Well known ports");
                    Console.WriteLine("Referenced from http://www.webopedia.com/quick_ref/portnumbers.asp");
                    Console.WriteLine("--------------------------------------------------------------------");
                    Console.WriteLine("PORT:1	TCP Port Service Multiplexer (TCPMUX)");
                    Console.WriteLine("PORT:5	Remote Job Entry (RJE)");
                    Console.WriteLine("PORT:7	ECHO");
                    Console.WriteLine("PORT:18	Message Send Protocol (MSP)");
                    Console.WriteLine("PORT:20	FTP -- Data");
                    Console.WriteLine("PORT:21	FTP -- Control");
                    Console.WriteLine("PORT:22	SSH Remote Login Protocol");
                    Console.WriteLine("PORT:23	Telnet");
                    Console.WriteLine("PORT:25	Simple Mail Transfer Protocol (SMTP)");
                    Console.WriteLine("PORT:29	MSG ICP");
                    Console.WriteLine("PORT:37	Time");
                    Console.WriteLine("PORT:42	Host Name Server (Nameserv)");
                    Console.WriteLine("PORT:43	WhoIs");
                    Console.WriteLine("PORT:49	Login Host Protocol (Login)");
                    Console.WriteLine("PORT:53	Domain Name System (DNS)");
                    Console.WriteLine("PORT:69	Trivial File Transfer Protocol (TFTP)");
                    Console.WriteLine("PORT:70	Gopher Services");
                    Console.WriteLine("PORT:79	Finger");
                    Console.WriteLine("PORT:80	HTTP");
                    Console.WriteLine("PORT:103 X.400 Standard");
                    Console.WriteLine("PORT:108 SNA Gateway Access Server");
                    Console.WriteLine("PORT:109 POP2");
                    Console.WriteLine("PORT:110 POP3");
                    Console.WriteLine("PORT:115 Simple File Transfer Protocol (SFTP)");
                    Console.WriteLine("PORT:118 SQL Services");
                    Console.WriteLine("PORT:119 Newsgroup (NNTP)");
                    Console.WriteLine("PORT:137 NetBIOS Name Service");
                    Console.WriteLine("PORT:139 NetBIOS Datagram Service");
                    Console.WriteLine("PORT:143 Interim Mail Access Protocol (IMAP)");
                    Console.WriteLine("PORT:150 NetBIOS Session Service");
                    Console.WriteLine("PORT:156 SQL Server");
                    Console.WriteLine("PORT:161 SNMP");
                    Console.WriteLine("PORT:179 Border Gateway Protocol (BGP)");
                    Console.WriteLine("PORT:190 Gateway Access Control Protocol (GACP)");
                    Console.WriteLine("PORT:194 Internet Relay Chat (IRC)");
                    Console.WriteLine("PORT:197 Directory Location Service (DLS)");
                    Console.WriteLine("PORT:389 Lightweight Directory Access Protocol (LDAP)");
                    Console.WriteLine("PORT:396 Novell Netware over IP");
                    Console.WriteLine("PORT:443 HTTPS");
                    Console.WriteLine("PORT:444 Simple Network Paging Protocol (SNPP)");
                    Console.WriteLine("PORT:445 Microsoft-DS");
                    Console.WriteLine("PORT:458 Apple QuickTime");
                    Console.WriteLine("PORT:546 DHCP Client");
                    Console.WriteLine("PORT:547 DHCP Serve");
                    Console.WriteLine("PORT:563 SNEWS");
                    Console.WriteLine("PORT:569 MSN");
                    Console.WriteLine("PORT:1080 Socks");
                    Console.WriteLine("PORT:3389 Remote Desktop Protocol(RDP)");
                    Console.WriteLine("");
                    Console.WriteLine("");
                }

                else
                {
                    PingTo pingit = new PingTo();
                    pingit.PingIt(args);
                }
            }

            //catches all exceptions for a smooth operation will out bothersome windows dialog errors shutting down the program
            catch (Exception)
            {
                
            }
        }
        
    }
}
