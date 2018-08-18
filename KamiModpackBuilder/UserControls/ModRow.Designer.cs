namespace KamiModpackBuilder.UserControls
{
    partial class ModRow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelModList = new System.Windows.Forms.Panel();
            this.buttonError = new System.Windows.Forms.Button();
            this.labelModName = new System.Windows.Forms.Label();
            this.labelSlotNumber = new System.Windows.Forms.Label();
            this.buttonProperties = new System.Windows.Forms.Button();
            this.panelModList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelModList
            // 
            this.panelModList.BackColor = System.Drawing.SystemColors.Control;
            this.panelModList.Controls.Add(this.buttonError);
            this.panelModList.Controls.Add(this.labelModName);
            this.panelModList.Controls.Add(this.labelSlotNumber);
            this.panelModList.Controls.Add(this.buttonProperties);
            this.panelModList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModList.Location = new System.Drawing.Point(0, 0);
            this.panelModList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelModList.Name = "panelModList";
            this.panelModList.Size = new System.Drawing.Size(892, 38);
            this.panelModList.TabIndex = 0;
            this.panelModList.Click += new System.EventHandler(this.panelModList_Click);
            // 
            // buttonError
            // 
            this.buttonError.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonError.FlatAppearance.BorderSize = 0;
            this.buttonError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonError.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonError.Image = global::KamiModpackBuilder.Properties.Resources.icon_error;
            this.buttonError.Location = new System.Drawing.Point(726, -2);
            this.buttonError.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonError.Name = "buttonError";
            this.buttonError.Size = new System.Drawing.Size(40, 42);
            this.buttonError.TabIndex = 3;
            this.buttonError.UseVisualStyleBackColor = true;
            // 
            // labelModName
            // 
            this.labelModName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelModName.AutoSize = true;
            this.labelModName.Location = new System.Drawing.Point(42, 9);
            this.labelModName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelModName.Name = "labelModName";
            this.labelModName.Size = new System.Drawing.Size(86, 20);
            this.labelModName.TabIndex = 2;
            this.labelModName.Text = "Mod Name";
            this.labelModName.Click += new System.EventHandler(this.labelModName_Click);
            // 
            // labelSlotNumber
            // 
            this.labelSlotNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSlotNumber.AutoSize = true;
            this.labelSlotNumber.Location = new System.Drawing.Point(4, 9);
            this.labelSlotNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSlotNumber.Name = "labelSlotNumber";
            this.labelSlotNumber.Size = new System.Drawing.Size(27, 20);
            this.labelSlotNumber.TabIndex = 1;
            this.labelSlotNumber.Text = "00";
            this.labelSlotNumber.Click += new System.EventHandler(this.labelSlotNumber_Click);
            // 
            // buttonProperties
            // 
            this.buttonProperties.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonProperties.Location = new System.Drawing.Point(776, 1);
            this.buttonProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonProperties.Name = "buttonProperties";
            this.buttonProperties.Size = new System.Drawing.Size(112, 35);
            this.buttonProperties.TabIndex = 0;
            this.buttonProperties.Text = "Properties";
            this.buttonProperties.UseVisualStyleBackColor = true;
            this.buttonProperties.Click += new System.EventHandler(this.buttonProperties_Click);
            // 
            // ModRow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelModList);
            this.Name = "ModRow";
            this.Size = new System.Drawing.Size(892, 38);
            this.panelModList.ResumeLayout(false);
            this.panelModList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelModList;
        private System.Windows.Forms.Label labelSlotNumber;
        private System.Windows.Forms.Button buttonProperties;
        private System.Windows.Forms.Label labelModName;
        private System.Windows.Forms.Button buttonError;
    }
}
