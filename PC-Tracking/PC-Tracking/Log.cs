using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Management;

namespace PC_Tracking
{
    class Log
    {
        SQLiteConnection SQLite;
        const string urlAddress = "https://andriiarsienov.000webhostapp.com/WriteToLog.php";

        public DataTable dataTable = new DataTable();

        public Log(String startupPath)
        {
            connectToDB(startupPath);
            SelectFromLocal();
        }

        public void WriteToLog(string _operation)
        {
            WriteToDB(_operation);
            SelectFromLocal();
            SendToWebClient(_operation);
        }

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
            string hwid = String.Empty;

            ManagementClass mc = new ManagementClass("win32_Processor");
            ManagementObjectCollection moc =  mc.GetInstances();

            foreach(ManagementObject mo in moc)
            {
                if(String.IsNullOrEmpty(hwid))
                {
                    hwid = mo.GetPropertyValue("processorID").ToString();
                    break;
                }
            }

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

        private void connectToDB(String startupPath)
        {
            if(!File.Exists(startupPath + "\\LogDB.db"))
            {
                SQLiteConnection.CreateFile(startupPath + "\\LogDB.db");

                SQLite = new SQLiteConnection(
                String.Format("Data Source={0};", startupPath + "\\LogDB.db"));

                SQLite.Open();
                SQLite.ChangePassword("qwerty");
                SQLite.Close();
            }
            
            SQLite = new SQLiteConnection(
                String.Format("Data Source={0};Password=qwerty", startupPath + "\\LogDB.db"));
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
