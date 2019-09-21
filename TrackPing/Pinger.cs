using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace TrackPing
{
    class Pinger
    {
        public void pinger(List<string> ipArgs, int pingCount=0, int pingSeconds=0)
        {
            DateTime dateTime = GetCurrentDateTime();
            if (pingCount == 0)
            {
                pingCount = 2000;
            }

            if (pingSeconds == 0)
            {
                pingSeconds = 5;
            }

            SqliteConnect sqliteConnect = new SqliteConnect();
            string myCommand = "create table IF NOT EXISTS tping_today (id INTEGER PRIMARY KEY AUTOINCREMENT, hostname TEXT NOT NULL, roundtriptime INTEGER NOT NULL, tod TEXT NOT NULL, status TEXT NOT NULL)";
            sqliteConnect.OpenConnection();
            SQLiteCommand command = new SQLiteCommand(myCommand, sqliteConnect.myConnection);
            command.ExecuteNonQuery();
            for (int i = 0; i < pingCount; i++)
            {
                foreach (string ipAddress in ipArgs)
                {
                    dateTime = GetCurrentDateTime();
                    try
                    {
                        Ping pingHost = new Ping();
                        PingReply pingReply = pingHost.Send(ipAddress);
                        string sqlInsert = "insert into tping_today (hostname, roundtriptime, tod, status) values (@hostnameValue, @roundtriptimeValue, @todValue, @statusValue)";
                        SQLiteCommand command2 = new SQLiteCommand(sqlInsert, sqliteConnect.myConnection);
                        command2.Parameters.AddWithValue("@hostnameValue", ipAddress);
                        command2.Parameters.AddWithValue("@roundtriptimeValue", pingReply.RoundtripTime.ToString());
                        command2.Parameters.AddWithValue("todValue", dateTime.TimeOfDay.ToString());
                        command2.Parameters.AddWithValue("statusValue", pingReply.Status.ToString());
                        command2.ExecuteNonQuery();
                        Console.WriteLine("{0}, " + "{1}, " + "{2}, " + dateTime.TimeOfDay, ipAddress, pingReply.RoundtripTime, pingReply.Status);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error Pinging " + ipAddress + ": " + ex.Message);
                        string sqlInsert = "insert into tping_today (hostname, roundtriptime, tod, status) values (@hostnameValue, 0, @todValue, @statusValue)";
                        SQLiteCommand command2 = new SQLiteCommand(sqlInsert, sqliteConnect.myConnection);
                        command2.Parameters.AddWithValue("@hostnameValue", ipAddress);
                        command2.Parameters.AddWithValue("todValue", dateTime.TimeOfDay.ToString());
                        command2.Parameters.AddWithValue("statusValue", "Failed: " + ex.Message);
                        command2.ExecuteNonQuery();
                    }
                }
                Thread.Sleep(pingSeconds * 1000);
            }
            sqliteConnect.CloseConnection();
        }

        void ExecutePing(List<string> ipArgs)
        {
            
        }
        public string GetApplicationExeDirName()
        {
            return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        }

        DateTime GetCurrentDateTime()
        {
            try
            {
                return DateTime.Now;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error Getting Date: " + ex.Message);
                return DateTime.Now;
            }
        }
    }
}
