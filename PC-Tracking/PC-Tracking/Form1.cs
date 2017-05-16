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
        public Form1()
        {
            InitializeComponent();

            Type t = typeof(System.Windows.Forms.PowerStatus);
            PropertyInfo[] pi = t.GetProperties();
            for (int i = 0; i < pi.Length; i++)
                listBox1.Items.Add(pi[i].Name);
            textBox1.Text = "The PowerStatus class has " + pi.Length.ToString() + " properties.\r\n";

            // Configure the list item selected handler for the list box to invoke a 
            // method that displays the value of each property.           
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);


            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

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
            result.Append(e.Mode.ToString());
            result.AppendLine();

            File.AppendAllText(logPath, result.ToString());
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
