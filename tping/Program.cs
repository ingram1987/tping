using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;
using CommandLine;
using CommandLine.Text;

namespace tping
{
    class argsParser
    {
        [Option('i', "ip", Required = true)]
        public string ip { get; set; }

        [Option('c', "count")]
        public int count { get; set; }
    }
    class helper
    {
        public void pingHost(string hostName, int pingCount) 
        {
            if (pingCount == 0)
            {
                pingCount = 43200;
            }
            Console.WriteLine("Host, Response Time, Status, Time ");
            String fileName = String.Format(@"tping-{0}-{1}-{2}-{3}.csv", DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            for (int i = 0; i < pingCount; i++)
            {
                Ping ping = new Ping();
                try
                {
                    PingReply pingReply = ping.Send(hostName);
                    StreamWriter processedData = new StreamWriter(@fileName, true);
                    processedData.WriteLine("{0}, " + "{1}, " + "{2}, " + DateTime.Now.TimeOfDay, hostName, pingReply.RoundtripTime, pingReply.Status);
                    processedData.Close();
                    Console.WriteLine("{0}, " + "{1}, " + "{2}, " + DateTime.Now.TimeOfDay, hostName, pingReply.RoundtripTime, pingReply.Status);
                    Thread.Sleep(2000);
                }
                catch (System.Net.NetworkInformation.PingException)
                {
                    Console.WriteLine("Please enter a valid hostname or IP");
                    Environment.Exit(0);
                }

            }
            Console.WriteLine("\n" + "tping complete - {0} pings logged in {1}", pingCount, fileName);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var options = new argsParser();
            CommandLine.Parser.Default.ParseArguments(args, options);
            if (args.Length > 0)
            {
                var test = Uri.CheckHostName(options.ip);
                if (Uri.CheckHostName(options.ip) != UriHostNameType.Unknown)
                {
                    helper ping1 = new helper();
                    ping1.pingHost(options.ip, options.count);

                }
                else
                {
                    Console.WriteLine("Please enter a valid hostname or IP");
                }
                
            }
            else
            {
                Console.WriteLine("tping.exe -i [IP|Hostname] -c [Number of pings (Default is 43200)]");
            }
        }
    }
}
