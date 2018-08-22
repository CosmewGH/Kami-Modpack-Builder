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
            this.backgroundWorkerDragDrop = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // panelModList
            // 
            this.panelModList.AutoScroll = true;
            this.panelModList.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelModList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModList.Location = new System.Drawing.Point(0, 0);
            this.panelModList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelModList.Name = "panelModList";
            this.panelModList.Size = new System.Drawing.Size(892, 732);
            this.panelModList.TabIndex = 0;
            // 
            // backgroundWorkerDragDrop
            // 
            this.backgroundWorkerDragDrop.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDragDrop_DoWork);
            this.backgroundWorkerDragDrop.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDragDrop_RunWorkerCompleted);
            // 
            // DataGridModsList
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelModList);
            this.Name = "DataGridModsList";
            this.Size = new System.Drawing.Size(892, 732);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DataGridModsList_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.DataGridModsList_DragOver);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelModList;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDragDrop;
    }
}
