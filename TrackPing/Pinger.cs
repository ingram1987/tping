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
        public void pinger(List<string> ipArgs, int pingCount=0, int pingSeconds=0, string dbName=null)
        {

            DateTime dateTime = DateTime.Now;
            if (dbName == null)
            {
                dbName = dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day + "_" + dateTime.Hour + "-" + dateTime.Minute + "-" + dateTime.Second + ".sqlite";
            }
            //string DbName = dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day + "_" + dateTime.Hour + "-" + dateTime.Minute + "-" + dateTime.Second + ".sqlite";
            if (pingCount == 0)
            {
                pingCount = 2000;
            }

            if (pingSeconds == 0)
            {
                pingSeconds = 5;
            }

            SqliteConnect sqliteConnect = new SqliteConnect(dbName);
            string myCommand = "create table IF NOT EXISTS TrackPing (id INTEGER PRIMARY KEY AUTOINCREMENT, hostname TEXT NOT NULL, roundtriptime INTEGER NOT NULL, tod TEXT NOT NULL, status TEXT NOT NULL)";
            sqliteConnect.OpenConnection();
            SQLiteCommand command = new SQLiteCommand(myCommand, sqliteConnect.myConnection);
            //command.Parameters.AddWithValue("@tping_today", dateTime.Date);
            

            string sqlInsert = "insert into TrackPing (hostname, roundtriptime, tod, status) values (@hostnameValue, @roundtriptimeValue, @todValue, @statusValue)";
            SQLiteCommand command2 = null;
            Ping pingHost = new Ping();
            
            try
            {
                command.ExecuteNonQuery();
                command2 = new SQLiteCommand(sqlInsert, sqliteConnect.myConnection);
                
                for (int i = 0; i < pingCount; i++)
                {
                    foreach (string ipAddress in ipArgs)
                    {
                        dateTime = DateTime.Now;
                        try
                        {
                            
                            PingReply pingReply = pingHost.Send(ipAddress);

                            command2.Parameters.AddWithValue("@hostnameValue", ipAddress);
                            command2.Parameters.AddWithValue("@roundtriptimeValue", pingReply.RoundtripTime.ToString());
                            command2.Parameters.AddWithValue("todValue", dateTime.TimeOfDay.ToString());
                            command2.Parameters.AddWithValue("statusValue", pingReply.Status.ToString());
                            Console.WriteLine("{0}, " + "{1}, " + "{2}, " + dateTime.TimeOfDay, ipAddress, pingReply.RoundtripTime, pingReply.Status);
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine("Error Pinging " + ipAddress + ": " + ex.Message);
                            
                            command2 = new SQLiteCommand(sqlInsert, sqliteConnect.myConnection);
                            command2.Parameters.AddWithValue("@hostnameValue", ipAddress);
                            command2.Parameters.AddWithValue("@roundtriptimeValue", 0);
                            command2.Parameters.AddWithValue("todValue", dateTime.TimeOfDay.ToString());
                            command2.Parameters.AddWithValue("statusValue", "Failed: " + ex.Message);
                            
                        }
                        command2.ExecuteNonQuery();
                        command2.Parameters.Clear();
                    }
                    Thread.Sleep(pingSeconds * 1000);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                pingHost?.Dispose();
                command2?.Dispose();
                sqliteConnect?.CloseConnection();
            }
            
        }

        void ExecutePing(List<string> ipArgs)
        {
            
        }
        public string GetApplicationExeDirName()
        {
            return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        }
    }
}
