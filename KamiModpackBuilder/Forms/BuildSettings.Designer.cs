namespace KamiModpackBuilder.Forms
{
    partial class BuildSettings
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
            this.buttonBuild = new System.Windows.Forms.Button();
            this.checkBoxWifiSafe = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxCrashSafety = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonBuild
            // 
            this.buttonBuild.Location = new System.Drawing.Point(244, 79);
            this.buttonBuild.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(50, 21);
            this.buttonBuild.TabIndex = 0;
            this.buttonBuild.Text = "Build";
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // checkBoxWifiSafe
            // 
            this.checkBoxWifiSafe.AutoSize = true;
            this.checkBoxWifiSafe.Location = new System.Drawing.Point(112, 15);
            this.checkBoxWifiSafe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxWifiSafe.Name = "checkBoxWifiSafe";
            this.checkBoxWifiSafe.Size = new System.Drawing.Size(15, 14);
            this.checkBoxWifiSafe.TabIndex = 24;
            this.checkBoxWifiSafe.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Wifi-Safe:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(147, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(265, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Exclude non-Wifi-Safe mods and extra Character Slots.";
            // 
            // comboBoxCrashSafety
            // 
            this.comboBoxCrashSafety.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCrashSafety.FormattingEnabled = true;
            this.comboBoxCrashSafety.Location = new System.Drawing.Point(113, 42);
            this.comboBoxCrashSafety.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxCrashSafety.Name = "comboBoxCrashSafety";
            this.comboBoxCrashSafety.Size = new System.Drawing.Size(188, 21);
            this.comboBoxCrashSafety.TabIndex = 52;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(320, 45);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(191, 13);
            this.label22.TabIndex = 51;
            this.label22.Text = "Exclude mods that may cause crashes.";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 45);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 13);
            this.label23.TabIndex = 50;
            this.label23.Text = "Crash Safety:";
            // 
            // BuildSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 107);
            this.Controls.Add(this.comboBoxCrashSafety);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.checkBoxWifiSafe);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonBuild);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "BuildSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Build Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BuildSettings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.CheckBox checkBoxWifiSafe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxCrashSafety;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
    }
}