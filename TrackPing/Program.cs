using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackPing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Argument length: " + args.Length);
            Console.WriteLine("Supplied Arguments are:");
            foreach (Object obj in args)
            {
                Console.WriteLine(obj);
            }
            Pinger pinger = new Pinger();
            pinger.pinger();
            Console.WriteLine("DONE");
        }
    }
}
