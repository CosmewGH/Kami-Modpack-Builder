namespace KamiModpackBuilder.UserControls
{
    partial class StageMods
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonImportMod = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanelSlotButtons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.openFileDialogImportZip = new System.Windows.Forms.OpenFileDialog();
            this.buttonDeleteMod = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanelSlotButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonDeleteMod);
            this.panel1.Controls.Add(this.buttonImportMod);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(854, 37);
            this.panel1.TabIndex = 0;
            // 
            // buttonImportMod
            // 
            this.buttonImportMod.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonImportMod.Location = new System.Drawing.Point(269, 2);
            this.buttonImportMod.Name = "buttonImportMod";
            this.buttonImportMod.Size = new System.Drawing.Size(158, 35);
            this.buttonImportMod.TabIndex = 8;
            this.buttonImportMod.Text = "Import Stage Mod";
            this.buttonImportMod.UseVisualStyleBackColor = true;
            this.buttonImportMod.Click += new System.EventHandler(this.buttonImportMod_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanelSlotButtons, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 37);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.53846F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.46154F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(854, 466);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Active Stages";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(576, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Inactive Stage Mods";
            // 
            // tableLayoutPanelSlotButtons
            // 
            this.tableLayoutPanelSlotButtons.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelSlotButtons.ColumnCount = 1;
            this.tableLayoutPanelSlotButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSlotButtons.Controls.Add(this.buttonDown, 0, 3);
            this.tableLayoutPanelSlotButtons.Controls.Add(this.buttonLeft, 0, 0);
            this.tableLayoutPanelSlotButtons.Controls.Add(this.buttonRight, 0, 1);
            this.tableLayoutPanelSlotButtons.Controls.Add(this.buttonUp, 0, 2);
            this.tableLayoutPanelSlotButtons.Location = new System.Drawing.Point(405, 155);
            this.tableLayoutPanelSlotButtons.Name = "tableLayoutPanelSlotButtons";
            this.tableLayoutPanelSlotButtons.RowCount = 4;
            this.tableLayoutPanelSlotButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSlotButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSlotButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSlotButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSlotButtons.Size = new System.Drawing.Size(44, 185);
            this.tableLayoutPanelSlotButtons.TabIndex = 9;
            // 
            // buttonDown
            // 
            this.buttonDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDown.Enabled = false;
            this.buttonDown.Location = new System.Drawing.Point(0, 141);
            this.buttonDown.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(44, 41);
            this.buttonDown.TabIndex = 7;
            this.buttonDown.Text = "↓";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLeft.Enabled = false;
            this.buttonLeft.Location = new System.Drawing.Point(0, 3);
            this.buttonLeft.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(44, 40);
            this.buttonLeft.TabIndex = 6;
            this.buttonLeft.Text = "←";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRight.Enabled = false;
            this.buttonRight.Location = new System.Drawing.Point(0, 49);
            this.buttonRight.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(44, 40);
            this.buttonRight.TabIndex = 5;
            this.buttonRight.Text = "→";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUp.Enabled = false;
            this.buttonUp.Location = new System.Drawing.Point(0, 95);
            this.buttonUp.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(44, 40);
            this.buttonUp.TabIndex = 4;
            this.buttonUp.Text = "↑";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // openFileDialogImportZip
            // 
            this.openFileDialogImportZip.Filter = "Zip file|*.zip|RAR Archive|*.rar|7 Zip Archive|*.7z";
            // 
            // buttonDeleteMod
            // 
            this.buttonDeleteMod.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonDeleteMod.Location = new System.Drawing.Point(433, 2);
            this.buttonDeleteMod.Name = "buttonDeleteMod";
            this.buttonDeleteMod.Size = new System.Drawing.Size(120, 35);
            this.buttonDeleteMod.TabIndex = 9;
            this.buttonDeleteMod.Text = "Delete Mod";
            this.buttonDeleteMod.UseVisualStyleBackColor = true;
            this.buttonDeleteMod.Click += new System.EventHandler(this.buttonDeleteMod_Click);
            // 
            // StageMods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "StageMods";
            this.Size = new System.Drawing.Size(854, 503);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanelSlotButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSlotButtons;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonImportMod;
        private System.Windows.Forms.OpenFileDialog openFileDialogImportZip;
        private System.Windows.Forms.Button buttonDeleteMod;
    }
}
