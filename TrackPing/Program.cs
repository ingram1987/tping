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

            }
            else if (parser.HasErrors)
            {
                Console.WriteLine(parser.UsageInfo.ToString(78, true));

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
                    pinger2.pinger(options.Ip, options.PingCount, options.PingSeconds);
                }
            }
        }
    }
}
