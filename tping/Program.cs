using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;

namespace tping
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string[] pingResults = new string[] { };
                Console.WriteLine("Host, Response Time, Status, Time ");
                for (int i = 0; i < 43200; i++)
                {
                    Ping ping = new Ping();
                    PingReply pingReply = ping.Send(args[0]);
                    Console.WriteLine("{0}, " + "{1}, " + "{2}, " + DateTime.Now.TimeOfDay, pingReply.Address, pingReply.RoundtripTime, pingReply.Status);
                    Thread.Sleep(2000);
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You must supply an IP address to tping, like this:\ntping.exe 192.168.1.1");
            }
        }
    }
}
