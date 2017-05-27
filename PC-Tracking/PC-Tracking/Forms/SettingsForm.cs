using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace PC_Tracking
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            RegistryKey rkApp = Registry.CurrentUser
                    .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);

            if (rkApp.GetValue("PC Tracking") == null)
                startupCheckBox.Checked = false;
            else startupCheckBox.Checked = true;

            checkBoxClosing.Checked = config.Default.MinimizeOnClosing;
        }

        private void startupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rkApp = Registry.CurrentUser
                    .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (startupCheckBox.Checked)
            {
                rkApp.SetValue("PC Tracking", Application.ExecutablePath.ToString());
            }
            else
            {
                rkApp.DeleteValue("PC Tracking", false);
            }
        }

        private void checkBoxClosing_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxClosing.Checked)
            {
                config.Default.MinimizeOnClosing = true;
            }
            else
            {
                config.Default.MinimizeOnClosing = false;
            }

            config.Default.Save();
        }
    }
}
