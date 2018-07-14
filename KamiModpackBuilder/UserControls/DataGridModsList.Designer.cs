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
            this.dataGridModList = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWarning = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridModList)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridModList
            // 
            this.dataGridModList.AllowUserToAddRows = false;
            this.dataGridModList.AllowUserToDeleteRows = false;
            this.dataGridModList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridModList.ColumnHeadersVisible = false;
            this.dataGridModList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnWarning,
            this.ColumnEdit});
            this.dataGridModList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridModList.Location = new System.Drawing.Point(0, 0);
            this.dataGridModList.MultiSelect = false;
            this.dataGridModList.Name = "dataGridModList";
            this.dataGridModList.RowHeadersVisible = false;
            this.dataGridModList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridModList.RowTemplate.Height = 28;
            this.dataGridModList.Size = new System.Drawing.Size(893, 733);
            this.dataGridModList.TabIndex = 5;
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnWarning
            // 
            this.ColumnWarning.HeaderText = "";
            this.ColumnWarning.MinimumWidth = 30;
            this.ColumnWarning.Name = "ColumnWarning";
            this.ColumnWarning.ReadOnly = true;
            this.ColumnWarning.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnWarning.Width = 30;
            // 
            // ColumnEdit
            // 
            this.ColumnEdit.HeaderText = "";
            this.ColumnEdit.MinimumWidth = 30;
            this.ColumnEdit.Name = "ColumnEdit";
            this.ColumnEdit.ReadOnly = true;
            this.ColumnEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnEdit.Width = 50;
            // 
            // DataGridModsList
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridModList);
            this.Name = "DataGridModsList";
            this.Size = new System.Drawing.Size(893, 733);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DataGridModsList_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.DataGridModsList_DragOver);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridModList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridModList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewImageColumn ColumnWarning;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEdit;
    }
}
