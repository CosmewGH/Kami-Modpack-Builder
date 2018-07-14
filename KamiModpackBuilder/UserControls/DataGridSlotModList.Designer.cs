namespace KamiModpackBuilder.UserControls
{
    partial class DataGridSlotModList
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
            this.dataGridSlotsModList = new System.Windows.Forms.DataGridView();
            this.ColumnSlotNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWarning = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBoxVoiceMods = new System.Windows.Forms.GroupBox();
            this.comboBoxVoiceSlot1 = new System.Windows.Forms.ComboBox();
            this.comboBoxVoiceSlot2 = new System.Windows.Forms.ComboBox();
            this.groupBoxSoundSlots = new System.Windows.Forms.GroupBox();
            this.comboBoxSoundSlot1 = new System.Windows.Forms.ComboBox();
            this.comboBoxSoundSlot2 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSlotsModList)).BeginInit();
            this.groupBoxVoiceMods.SuspendLayout();
            this.groupBoxSoundSlots.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridSlotsModList
            // 
            this.dataGridSlotsModList.AllowUserToAddRows = false;
            this.dataGridSlotsModList.AllowUserToDeleteRows = false;
            this.dataGridSlotsModList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSlotsModList.ColumnHeadersVisible = false;
            this.dataGridSlotsModList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSlotNum,
            this.ColumnName,
            this.ColumnWarning,
            this.ColumnEdit});
            this.dataGridSlotsModList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSlotsModList.Location = new System.Drawing.Point(3, 3);
            this.dataGridSlotsModList.MultiSelect = false;
            this.dataGridSlotsModList.Name = "dataGridSlotsModList";
            this.dataGridSlotsModList.RowHeadersVisible = false;
            this.dataGridSlotsModList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridSlotsModList.RowTemplate.Height = 28;
            this.dataGridSlotsModList.Size = new System.Drawing.Size(1247, 776);
            this.dataGridSlotsModList.TabIndex = 5;
            // 
            // ColumnSlotNum
            // 
            this.ColumnSlotNum.HeaderText = "Slot";
            this.ColumnSlotNum.MinimumWidth = 50;
            this.ColumnSlotNum.Name = "ColumnSlotNum";
            this.ColumnSlotNum.ReadOnly = true;
            this.ColumnSlotNum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnSlotNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnSlotNum.Width = 50;
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
            // groupBoxVoiceMods
            // 
            this.groupBoxVoiceMods.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxVoiceMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxVoiceMods.Location = new System.Drawing.Point(3, 3);
            this.groupBoxVoiceMods.Name = "groupBoxVoiceMods";
            this.groupBoxVoiceMods.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.groupBoxVoiceMods.Size = new System.Drawing.Size(617, 63);
            this.groupBoxVoiceMods.TabIndex = 0;
            this.groupBoxVoiceMods.TabStop = false;
            this.groupBoxVoiceMods.Text = "Voice Mod Slots";
            // 
            // comboBoxVoiceSlot1
            // 
            this.comboBoxVoiceSlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxVoiceSlot1.FormattingEnabled = true;
            this.comboBoxVoiceSlot1.Location = new System.Drawing.Point(3, 3);
            this.comboBoxVoiceSlot1.Name = "comboBoxVoiceSlot1";
            this.comboBoxVoiceSlot1.Size = new System.Drawing.Size(299, 28);
            this.comboBoxVoiceSlot1.TabIndex = 0;
            // 
            // comboBoxVoiceSlot2
            // 
            this.comboBoxVoiceSlot2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxVoiceSlot2.FormattingEnabled = true;
            this.comboBoxVoiceSlot2.Location = new System.Drawing.Point(308, 3);
            this.comboBoxVoiceSlot2.Name = "comboBoxVoiceSlot2";
            this.comboBoxVoiceSlot2.Size = new System.Drawing.Size(300, 28);
            this.comboBoxVoiceSlot2.TabIndex = 1;
            // 
            // groupBoxSoundSlots
            // 
            this.groupBoxSoundSlots.Controls.Add(this.tableLayoutPanel3);
            this.groupBoxSoundSlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSoundSlots.Location = new System.Drawing.Point(626, 3);
            this.groupBoxSoundSlots.Name = "groupBoxSoundSlots";
            this.groupBoxSoundSlots.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.groupBoxSoundSlots.Size = new System.Drawing.Size(618, 63);
            this.groupBoxSoundSlots.TabIndex = 1;
            this.groupBoxSoundSlots.TabStop = false;
            this.groupBoxSoundSlots.Text = "Sound Effect Mod Slots";
            // 
            // comboBoxSoundSlot1
            // 
            this.comboBoxSoundSlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSoundSlot1.FormattingEnabled = true;
            this.comboBoxSoundSlot1.Location = new System.Drawing.Point(3, 3);
            this.comboBoxSoundSlot1.Name = "comboBoxSoundSlot1";
            this.comboBoxSoundSlot1.Size = new System.Drawing.Size(300, 28);
            this.comboBoxSoundSlot1.TabIndex = 0;
            // 
            // comboBoxSoundSlot2
            // 
            this.comboBoxSoundSlot2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSoundSlot2.FormattingEnabled = true;
            this.comboBoxSoundSlot2.Location = new System.Drawing.Point(309, 3);
            this.comboBoxSoundSlot2.Name = "comboBoxSoundSlot2";
            this.comboBoxSoundSlot2.Size = new System.Drawing.Size(300, 28);
            this.comboBoxSoundSlot2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSoundSlots, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxVoiceMods, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 785);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1247, 69);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.comboBoxVoiceSlot1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxVoiceSlot2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(611, 36);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.comboBoxSoundSlot1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.comboBoxSoundSlot2, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(612, 36);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.dataGridSlotsModList, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1253, 857);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // DataGridSlotModList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel4);
            this.Name = "DataGridSlotModList";
            this.Size = new System.Drawing.Size(1253, 857);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSlotsModList)).EndInit();
            this.groupBoxVoiceMods.ResumeLayout(false);
            this.groupBoxSoundSlots.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridSlotsModList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSlotNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewImageColumn ColumnWarning;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEdit;
        private System.Windows.Forms.GroupBox groupBoxVoiceMods;
        private System.Windows.Forms.ComboBox comboBoxVoiceSlot1;
        private System.Windows.Forms.ComboBox comboBoxVoiceSlot2;
        private System.Windows.Forms.GroupBox groupBoxSoundSlots;
        private System.Windows.Forms.ComboBox comboBoxSoundSlot1;
        private System.Windows.Forms.ComboBox comboBoxSoundSlot2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}
