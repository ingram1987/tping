﻿using System;
using System.Collections.Generic;
using Plossum.CommandLine;

namespace TrackPing
{
    [CommandLineManager(ApplicationName = "Track Ping",
    Copyright = "GNU GENERAL PUBLIC LICENSE Version 3")]
    [CommandLineOptionGroup("options", Name = "Options")]
    class Options
    {
        [CommandLineOption(Name = "i", Description = "Specify the IP address", MinOccurs = 1)]
        public List<string> Ip {
            get { return mIp; }
            set
            {
                foreach (string ip in value)
                {
                    if (string.IsNullOrWhiteSpace(ip) == false)
                    {
                        string[] splitValues = ip.Split('.');
                        if (splitValues.Length == 4)
                        {
                            Console.WriteLine("SPLIT");
                            mIp = value;
                        }
                    }
                }

            }
        }
        private List<string> mIp = new List<string>();

        [CommandLineOption(Name = "h", Description = "Shows this help text")]
        public bool Help { get; set; }
    }
}
