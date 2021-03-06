﻿using System;
using System.Management;
using System.Windows.Forms;

namespace PC_Tracking
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1(GetHWID());
            Application.Run();
        }

        private static string GetHWID()
        {
            ManagementClass mc = new ManagementClass("win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            string hwid = String.Empty;

            foreach (ManagementObject mo in moc)
            {
                if (String.IsNullOrEmpty(hwid))
                {
                    hwid = mo.GetPropertyValue("processorID").ToString();
                    break;
                }
            }

            return hwid;
        }
    }
}
