using System;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace PC_Tracking
{
    public partial class Form1 : Form
    {
        string logPath = Application.StartupPath + "\\log.txt";
        string powerLineStatus;
        string batteryChargeStatus;
        SQLiteConnection SQLite;

        public Form1()
        {
            InitializeComponent();
            connectToDB();

            this.Resize += Form1_Resize;
            this.FormClosed += Form1_FormClosed;
            this.Load += Form1_Load;
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);

            Type t = typeof(System.Windows.Forms.PowerStatus);
            PropertyInfo[] pi = t.GetProperties();

            powerLineStatus = t.GetProperty("PowerLineStatus").GetValue(SystemInformation.PowerStatus, null)
                .ToString();

            batteryChargeStatus = t.GetProperty("BatteryChargeStatus").GetValue(SystemInformation.PowerStatus, null)
                .ToString();

            for (int i = 0; i < pi.Length; i++)
                listBox1.Items.Add(pi[i].Name);
            textBox1.Text = "The PowerStatus class has " + pi.Length.ToString() + " properties.\r\n";

            StringBuilder result = new StringBuilder();
            result.Append(DateTime.Now).Append("  ---  ").Append("Program started").AppendLine();
            File.AppendAllText(logPath, result.ToString());

            WriteToLog("Program started");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            StringBuilder result = new StringBuilder();
            result.Append(DateTime.Now).Append("  ---  ").Append("Program closed").AppendLine();
            File.AppendAllText(logPath, result.ToString());

            WriteToLog("Program closed");
            SQLite.Close();

            Application.ExitThread();
        }

        private void connectToDB()
        {
            SQLite = new SQLiteConnection(String.Format("Data Source={0};", Application.StartupPath + "\\LogDB.db"));
            SQLite.Open();
        }

        private void WriteToLog (string _operation)
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Return if no item is selected.
            if (listBox1.SelectedIndex == -1) return;
            // Get the property name from the list item
            string propname = listBox1.Text;

            // Display the value of the selected property of the PowerStatus type.
            Type t = typeof(System.Windows.Forms.PowerStatus);
            PropertyInfo[] pi = t.GetProperties();
            PropertyInfo prop = null;
            for (int i = 0; i < pi.Length; i++)
                if (pi[i].Name == propname)
                {
                    prop = pi[i];
                    break;
                }

            object propval = prop.GetValue(SystemInformation.PowerStatus, null);
            textBox1.Text += "\r\nThe value of the " + propname + " property is: " + propval.ToString();
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            StringBuilder result = new StringBuilder();
            result.Append(DateTime.Now.ToString());
            result.Append("  ---  ");

            if(e.Mode == PowerModes.StatusChange)
            {
                Type t = typeof(System.Windows.Forms.PowerStatus);

                string pls = t.GetProperty("PowerLineStatus").GetValue(SystemInformation.PowerStatus, null)
                .ToString();

                string bcs = t.GetProperty("BatteryChargeStatus").GetValue(SystemInformation.PowerStatus, null)
                .ToString();

                if (!pls.Equals(powerLineStatus))
                {
                    if (pls == "Online")
                    {
                        result.Append("Power line plugged");
                        WriteToLog("Power line plugged");
                    }
                    else
                    {
                        result.Append("Power line unplugged");
                        WriteToLog("Power line unplugged");
                    }

                    powerLineStatus = pls;
                }
                else if (!bcs.Equals(batteryChargeStatus))
                {
                    result.Append("Battery changes: ").Append(bcs);
                    WriteToLog(bcs);
                    batteryChargeStatus = bcs;
                }
            }
            else result.Append(e.Mode.ToString());

            result.AppendLine();

            File.AppendAllText(logPath, result.ToString());
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            sf.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_FormClosed(null, null);
        }
    }
}
