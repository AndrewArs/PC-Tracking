using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace PC_Tracking
{
    public partial class Form1 : Form
    {
        Log log;
        string powerLineStatus;
        string batteryChargeStatus;

        int posX;
        int posY;
        bool drag = false;

        public Form1()
        {
            InitializeComponent();
            log = new Log(Application.StartupPath);

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

            log.WriteToLog("Program started");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.WriteToLog("Program closed");

            Application.ExitThread();
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

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }

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
    }
}
