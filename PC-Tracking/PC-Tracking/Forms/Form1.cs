using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace PC_Tracking
{
    public partial class Form1 : Form
    {
        public string hwid = String.Empty;


        Log log;
        string powerLineStatus;
        string batteryChargeStatus;

        int posX;
        int posY;
        bool drag = false;

        BindingSource bs = new BindingSource();

        public Form1(string _hwid)
        {
            hwid = _hwid;
            InitializeComponent();

            log = new Log(Application.StartupPath, hwid);

            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
            SystemEvents.SessionEnded += SystemEvents_SessionEnded;

            webBrowser.Url = new Uri("https://andriiarsienov.000webhostapp.com/?hwid=" + hwid);

            Type t = typeof(System.Windows.Forms.PowerStatus);

            powerLineStatus = t.GetProperty("PowerLineStatus").GetValue(SystemInformation.PowerStatus, null)
                .ToString();

            batteryChargeStatus = t.GetProperty("BatteryChargeStatus").GetValue(SystemInformation.PowerStatus, null)
                .ToString();

            bs.DataSource = log.dataTable;
            dataGridView.DataSource = bs;

            log.WriteToLog("Program started");
        }

        private void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            log.WriteToLog("Program closed");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.WriteToLog("Program closed");

            Application.ExitThread();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
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
                        log.WriteToLog("Power line plugged");
                    }
                    else
                    {
                        log.WriteToLog("Power line unplugged");
                    }

                    powerLineStatus = pls;
                }
                else if (!bcs.Equals(batteryChargeStatus))
                {
                    log.WriteToLog(bcs);
                    batteryChargeStatus = bcs;
                }
            }
            else log.WriteToLog(e.Mode.ToString());
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }


        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.MinimizeOnClosing)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
            }
            else
                Form1_FormClosed(null, null);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            webBrowser.Refresh();
        }

        private void buttonBrowser_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(webBrowser.Url.AbsoluteUri);
        }

        private void buttonLocalDB_Click(object sender, EventArgs e)
        {
            if (webBrowser.Visible)
            {
                webBrowser.Hide();
                dataGridView.Show();
                buttonUpdate.Enabled = false;
                buttonLocalDB.Text = "Switch back";
            }
            else
            {
                dataGridView.Hide();
                webBrowser.Show();
                buttonUpdate.Enabled = true;
                buttonLocalDB.Text = "Switch to local";
            }
        }

        #region menu items event handlers
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program for tracking PC's state.\nCreated by Andrew Ars\n Kharkiv 2017",
                "About", MessageBoxButtons.OK);
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
        #endregion

        #region move window functions 
        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                this.Top = System.Windows.Forms.Cursor.Position.Y - posY;
                this.Left = System.Windows.Forms.Cursor.Position.X - posX;
            }
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                posX = Cursor.Position.X - this.Left;
                posY = Cursor.Position.Y - this.Top;
            }
        }
        #endregion
    }
}
