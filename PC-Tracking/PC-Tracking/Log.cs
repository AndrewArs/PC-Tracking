using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Management;

namespace PC_Tracking
{
    /// <summary>
    /// Class for send data to local db and web server
    /// </summary>
    class Log
    {
        SQLiteConnection SQLite;
        const string urlAddress = "https://andriiarsienov.000webhostapp.com/WriteToLog.php";
        string pathToDB;
        string hwid;

        public DataTable dataTable = new DataTable();

        /// <param name="startupPath">Application startup path where the local database is located</param>
        /// <param name="_hwid">Processor id used to identify a unique user</param>
        public Log(String startupPath, string _hwid)
        {
            hwid = _hwid;
            pathToDB = startupPath + "\\LogDB.db";

            connectToDB();
            SelectFromLocal();
        }

        /// <summary>
        /// Write to local database and send to web server
        /// </summary>
        /// <param name="_operation"> operation that arise when power mode changed</param>
        public void WriteToLog(string _operation)
        {
            WriteToDB(_operation);
            SelectFromLocal();
            SendToWebClient(_operation);
        }

        /// <summary>
        /// Get all rows from local database
        /// </summary>
        public void SelectFromLocal()
        {
            string query = "SELECT Date, Operation FROM 'Log'";

            using (SQLiteCommand cmd = new SQLiteCommand(query, SQLite))
            {
                dataTable.Clear();
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dataTable);
            }
        }

        private void WriteToDB (string _operation)
        {
            string query = "INSERT INTO Log(Date, Operation) VALUES(@date, @operation)";

            using (SQLiteCommand cmd = new SQLiteCommand(query, SQLite))
            {
                cmd.Parameters.Add("@date", DbType.String);
                cmd.Parameters.Add("@operation", DbType.String);

                cmd.Parameters["@date"].Value = DateTime.Now.ToString();
                cmd.Parameters["@operation"].Value = _operation;
                cmd.ExecuteNonQuery();
            }
        }

        private void SendToWebClient (string _operation)
        {
            using (WebClient client = new WebClient())
            {
                NameValueCollection postData = new NameValueCollection()
                {
                    { "date", DateTime.Now.ToString() },
                    { "operation", _operation },
                    { "hwid", hwid}
                };

                client.UploadValues(urlAddress, postData);
            }
        }

        /// <summary>
        /// Open connection to local database. If db not exists creates database file 
        /// </summary>
        private void connectToDB()
        {
            if(!File.Exists(pathToDB))
            {
                SQLiteConnection.CreateFile(pathToDB);

                SQLite = new SQLiteConnection(
                String.Format("Data Source={0};", pathToDB));

                SQLite.Open();
                SQLite.ChangePassword("qwerty");
                SQLite.Close();
            }
            
            SQLite = new SQLiteConnection(
                String.Format("Data Source={0};Password=qwerty", pathToDB));
            SQLite.Open();

            string query = "CREATE TABLE IF NOT EXISTS Log ( `Date` TEXT NOT NULL , `Operation` TEXT NOT NULL" + 
                " ,CONSTRAINT PK_log PRIMARY KEY (`Date`))";

            using (SQLiteCommand cmd = new SQLiteCommand(query, SQLite))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
