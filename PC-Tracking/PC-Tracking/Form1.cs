using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using System.Reflection;
using System.IO;

namespace PC_Tracking
{
    public partial class Form1 : Form
    {
        string logPath = "log.txt";
        string powerLineStatus;
        string batteryChargeStatus;

        public Form1()
        {
            InitializeComponent();

            this.Resize += Form1_Resize;
            this.FormClosing += Form1_FormClosing;
            this.Load += Form1_Load;

            Type t = typeof(System.Windows.Forms.PowerStatus);
            PropertyInfo[] pi = t.GetProperties();

            powerLineStatus = t.GetProperty("PowerLineStatus").GetValue(SystemInformation.PowerStatus, null)
                .ToString();

            batteryChargeStatus = t.GetProperty("BatteryChargeStatus").GetValue(SystemInformation.PowerStatus, null)
                .ToString();

            for (int i = 0; i < pi.Length; i++)
                listBox1.Items.Add(pi[i].Name);
            textBox1.Text = "The PowerStatus class has " + pi.Length.ToString() + " properties.\r\n";

            // Configure the list item selected handler for the list box to invoke a 
            // method that displays the value of each property.           
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);


            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();
            result.Append(DateTime.Now).Append("  ---  ").Append("Program started").AppendLine();
            File.AppendAllText(logPath, result.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StringBuilder result = new StringBuilder();
            result.Append(DateTime.Now).Append("  ---  ").Append("Program closed").AppendLine();
            File.AppendAllText(logPath, result.ToString());
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
                        result.Append("Power line plugged");
                    else result.Append("Power line unplugged");

                    powerLineStatus = pls;
                }
                else if (!bcs.Equals(batteryChargeStatus))
                {
                    result.Append("Battery changes: ").Append(bcs);
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
    }
}
