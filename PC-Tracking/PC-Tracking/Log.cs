using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PC_Tracking
{
    class Log
    {
        SQLiteConnection SQLite;
        string urlAddress = "https://andriiarsienov.000webhostapp.com/WriteToLog.php";

        public Log(String startupPath)
        {
            connectToDB(startupPath);
        }

        public void WriteToLog(string _operation)
        {
            WriteToDB(_operation);
            SendToWebClient(_operation);
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
                    { "operation", _operation }
                };

                client.UploadValues(urlAddress, postData);
            }
        }

        private void connectToDB(String startupPath)
        {
            SQLite = new SQLiteConnection(
                String.Format("Data Source={0};Password=qwerty", startupPath + "\\LogDB.db"));
            SQLite.Open();
        }
    }
}
