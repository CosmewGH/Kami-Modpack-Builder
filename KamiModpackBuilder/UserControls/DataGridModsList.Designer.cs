namespace KamiModpackBuilder.UserControls
{
    partial class DataGridModsList
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
            this.SuspendLayout();
            // 
            // panelModList
            // 
            this.panelModList.AutoScroll = true;
            this.panelModList.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelModList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModList.Location = new System.Drawing.Point(0, 0);
            this.panelModList.Name = "panelModList";
            this.panelModList.Size = new System.Drawing.Size(595, 476);
            this.panelModList.TabIndex = 0;
            // 
            // DataGridModsList
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelModList);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DataGridModsList";
            this.Size = new System.Drawing.Size(595, 476);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DataGridModsList_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.DataGridModsList_DragOver);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelModList;
    }
}
