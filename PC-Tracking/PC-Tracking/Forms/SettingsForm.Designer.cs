namespace PC_Tracking
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.startupCheckBox = new System.Windows.Forms.CheckBox();
            this.checkBoxClosing = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // startupCheckBox
            // 
            this.startupCheckBox.AutoSize = true;
            this.startupCheckBox.Location = new System.Drawing.Point(12, 33);
            this.startupCheckBox.Name = "startupCheckBox";
            this.startupCheckBox.Size = new System.Drawing.Size(126, 17);
            this.startupCheckBox.TabIndex = 0;
            this.startupCheckBox.Text = "Startup with windows";
            this.startupCheckBox.UseVisualStyleBackColor = true;
            this.startupCheckBox.CheckedChanged += new System.EventHandler(this.startupCheckBox_CheckedChanged);
            // 
            // checkBoxClosing
            // 
            this.checkBoxClosing.AutoSize = true;
            this.checkBoxClosing.Location = new System.Drawing.Point(12, 57);
            this.checkBoxClosing.Name = "checkBoxClosing";
            this.checkBoxClosing.Size = new System.Drawing.Size(114, 17);
            this.checkBoxClosing.TabIndex = 1;
            this.checkBoxClosing.Text = "Minimize at closing";
            this.checkBoxClosing.UseVisualStyleBackColor = true;
            this.checkBoxClosing.CheckedChanged += new System.EventHandler(this.checkBoxClosing_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 108);
            this.Controls.Add(this.checkBoxClosing);
            this.Controls.Add(this.startupCheckBox);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox startupCheckBox;
        private System.Windows.Forms.CheckBox checkBoxClosing;
    }
}