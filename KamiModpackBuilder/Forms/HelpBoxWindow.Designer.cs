namespace KamiModpackBuilder.Forms
{
    partial class HelpBoxWindow
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
            this.labelText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDoNotShow = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelText.Location = new System.Drawing.Point(0, 0);
            this.labelText.MaximumSize = new System.Drawing.Size(290, 0);
            this.labelText.Name = "labelText";
            this.labelText.Padding = new System.Windows.Forms.Padding(3);
            this.labelText.Size = new System.Drawing.Size(6, 19);
            this.labelText.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonDoNotShow);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 19);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(284, 35);
            this.panel1.TabIndex = 1;
            // 
            // buttonDoNotShow
            // 
            this.buttonDoNotShow.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonDoNotShow.Location = new System.Drawing.Point(162, 6);
            this.buttonDoNotShow.Name = "buttonDoNotShow";
            this.buttonDoNotShow.Size = new System.Drawing.Size(110, 23);
            this.buttonDoNotShow.TabIndex = 1;
            this.buttonDoNotShow.Text = "Don\'t Show Again";
            this.buttonDoNotShow.UseVisualStyleBackColor = true;
            this.buttonDoNotShow.Click += new System.EventHandler(this.buttonDoNotShow_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonOk.Location = new System.Drawing.Point(12, 6);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(110, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // HelpBoxWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 57);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelText);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpBoxWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tips";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDoNotShow;
        private System.Windows.Forms.Button buttonOk;
    }
}