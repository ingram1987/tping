using System;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;

namespace TrackPing
{
    class SqliteConnect
    {
        public string DbName = "trackping.sqlite";
        public SQLiteConnection myConnection;


        public SqliteConnect()
        {
            myConnection = new SQLiteConnection("Data Source =" + DbName + ";Version=3;");
            if (!File.Exists(DbName))
            {
                SQLiteConnection.CreateFile(DbName);
            }
        }
        // Open Connection
        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        // Close Connection
        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }

        public void Database()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source =" + DbName + ";Version=3;");

        }

        //Check if Database Exists
        public bool dbExists()
        {
            
            string ConstBackSlash = "\\";
            if (File.Exists(GetApplicationExeDirName() + ConstBackSlash + DbName))
            {
                Console.WriteLine("File Exists");
                return true;
            }
            else
            {
                Console.WriteLine("File Does Not Exist");
                return false;
            }
        }

        //Get Current Directory
        public string GetApplicationExeDirName()
        {
            return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        }

        //Create Database
        public void createDb()
        {
            SQLiteConnection.CreateFile(DbName);
        }
    }
}
