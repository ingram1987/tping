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
        public void pinger(List<string> ipArgs)
        {
            int pingCount = 99;
            SqliteConnect sqliteConnect = new SqliteConnect();
            string myCommand = "create table IF NOT EXISTS tping_today (id INTEGER PRIMARY KEY AUTOINCREMENT, hostname TEXT NOT NULL, roundtriptime INTEGER NOT NULL, tod TEXT NOT NULL, status TEXT NOT NULL)";
            sqliteConnect.OpenConnection();
            SQLiteCommand command = new SQLiteCommand(myCommand, sqliteConnect.myConnection);
            command.ExecuteNonQuery();
            for (int i = 0; i < pingCount; i++)
            {
                foreach (string ipAddress in ipArgs)
                {
                    Ping pingHost = new Ping();
                    PingReply pingReply = pingHost.Send("4.2.2.1");
                    DateTime dateTime = DateTime.Now;
                    string sqlInsert = "insert into tping_today (hostname, roundtriptime, tod, status) values (@hostnameValue, @roundtriptimeValue, @todValue, @statusValue)";
                    SQLiteCommand command2 = new SQLiteCommand(sqlInsert, sqliteConnect.myConnection);
                    command2.Parameters.AddWithValue("@hostnameValue", ipAddress);
                    command2.Parameters.AddWithValue("@roundtriptimeValue", pingReply.RoundtripTime.ToString());
                    command2.Parameters.AddWithValue("todValue", dateTime.TimeOfDay.ToString());
                    command2.Parameters.AddWithValue("statusValue", pingReply.Status.ToString());
                    command2.ExecuteNonQuery();
                    Console.WriteLine("{0}, " + "{1}, " + "{2}, " + dateTime.TimeOfDay, ipAddress, pingReply.RoundtripTime, pingReply.Status);
                    Thread.Sleep(10);
                }
                
            }
            sqliteConnect.CloseConnection();
        }

        public string GetApplicationExeDirName()
        {
            return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        }
    }
}
