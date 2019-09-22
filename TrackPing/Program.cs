using System;
using Plossum.CommandLine;

namespace TrackPing
{
    class Program
    {
        static void Main(string[] args)
        {
            Options options = new Options();
            CommandLineParser parser = new CommandLineParser(options);
            parser.Parse();

            if (options.Help)
            {
                Console.WriteLine(parser.UsageInfo.ToString(78, false));
                showExamples();

            }
            else if (parser.HasErrors)
            {
                Console.WriteLine(parser.UsageInfo.ToString(78, true));
                showExamples();

            }
            else if (options.Ip != null)
            {
                bool validIp = false;
                Parse parse = new Parse();
                foreach (string ipArg in options.Ip)
                {
                    if (parse.ValidateIPv4(ipArg) == true)
                    {
                        validIp = true;
                    }
                }

                if (validIp)
                {
                    Pinger pinger2 = new Pinger();
                    pinger2.pinger(options.Ip, options.PingCount, options.PingSeconds, options.DatabaseName);
                }
            }

            void showExamples()
            {
                Console.WriteLine("Use Case:");
                Console.WriteLine("Ping IP addresses and log the results to an SQLite Database.");
                Console.WriteLine("Analyze the results to search for network latency and dropped packets.");
                Console.WriteLine();
                Console.WriteLine("Examining Results:");
                Console.WriteLine("Use SQLiteBrowser \"https://sqlitebrowser.org\" to view, filter, and analyze the ping results.");
                Console.WriteLine("Open Database, Click \"Browse Data\", Select the table, view/filter/analyze/export the data.");
                Console.WriteLine();
                Console.WriteLine("Examples:");
                Console.WriteLine("TrackPing.exe -i 10.0.0.1");
                Console.WriteLine("     --Pings 10.0.0.1 for the default number of pings at the default interval");
                Console.WriteLine("TrackPing.exe -i 10.0.0.1 -i 10.0.0.2");
                Console.WriteLine("     --Pings 10.0.0.1 & 10.0.0.2 for the default number of pings at the default interval");
                Console.WriteLine("TrackPing.exe -i 10.0.0.1 -c 10 -s 2");
                Console.WriteLine("     --Pings 10.0.0.1 for the 10 pings at a 2 second interval");
                Console.WriteLine("TrackPing.exe -i 10.0.0.1 -d MyDatabase.sqlite");
                Console.WriteLine("     --Pings Pings 10.0.0.1 for the default number of pings at the default interval, and sets the database name");
            }
        }


    }
}
