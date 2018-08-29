namespace KamiModpackBuilder.UserControls
{
    partial class CharacterMods
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxCharacters = new System.Windows.Forms.ComboBox();
            this.buttonTextureIDFixAll = new System.Windows.Forms.Button();
            this.buttonImportSlotMod = new System.Windows.Forms.Button();
            this.buttonImportGeneralMod = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanelGeneralButtons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonGeneralDown = new System.Windows.Forms.Button();
            this.buttonGeneralLeft = new System.Windows.Forms.Button();
            this.buttonGeneralRight = new System.Windows.Forms.Button();
            this.buttonGeneralUp = new System.Windows.Forms.Button();
            this.tableLayoutPanelSlotButtons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSlotBottom = new System.Windows.Forms.Button();
            this.buttonSlotDown = new System.Windows.Forms.Button();
            this.buttonSlotLeft = new System.Windows.Forms.Button();
            this.buttonSlotRight = new System.Windows.Forms.Button();
            this.buttonSlotUp = new System.Windows.Forms.Button();
            this.openFileDialogImportZip = new System.Windows.Forms.OpenFileDialog();
            this.buttonDeleteMod = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanelGeneralButtons.SuspendLayout();
            this.tableLayoutPanelSlotButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1396, 37);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.comboBoxCharacters);
            this.flowLayoutPanel1.Controls.Add(this.buttonTextureIDFixAll);
            this.flowLayoutPanel1.Controls.Add(this.buttonImportSlotMod);
            this.flowLayoutPanel1.Controls.Add(this.buttonImportGeneralMod);
            this.flowLayoutPanel1.Controls.Add(this.buttonDeleteMod);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1396, 37);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // comboBoxCharacters
            // 
            this.comboBoxCharacters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacters.FormattingEnabled = true;
            this.comboBoxCharacters.Location = new System.Drawing.Point(3, 3);
            this.comboBoxCharacters.Name = "comboBoxCharacters";
            this.comboBoxCharacters.Size = new System.Drawing.Size(184, 28);
            this.comboBoxCharacters.TabIndex = 1;
            this.comboBoxCharacters.SelectedIndexChanged += new System.EventHandler(this.comboBoxCharacters_SelectedIndexChanged);
            // 
            // buttonTextureIDFixAll
            // 
            this.buttonTextureIDFixAll.Location = new System.Drawing.Point(193, 3);
            this.buttonTextureIDFixAll.Name = "buttonTextureIDFixAll";
            this.buttonTextureIDFixAll.Size = new System.Drawing.Size(174, 35);
            this.buttonTextureIDFixAll.TabIndex = 3;
            this.buttonTextureIDFixAll.Text = "TextureID Fix All Slots";
            this.buttonTextureIDFixAll.Click += new System.EventHandler(this.buttonTextureIDFixAll_Click);
            // 
            // buttonImportSlotMod
            // 
            this.buttonImportSlotMod.Location = new System.Drawing.Point(373, 3);
            this.buttonImportSlotMod.Name = "buttonImportSlotMod";
            this.buttonImportSlotMod.Size = new System.Drawing.Size(158, 35);
            this.buttonImportSlotMod.TabIndex = 6;
            this.buttonImportSlotMod.Text = "Import Slot Mod";
            this.buttonImportSlotMod.Click += new System.EventHandler(this.buttonImportSlotMod_Click);
            // 
            // buttonImportGeneralMod
            // 
            this.buttonImportGeneralMod.Location = new System.Drawing.Point(537, 3);
            this.buttonImportGeneralMod.Name = "buttonImportGeneralMod";
            this.buttonImportGeneralMod.Size = new System.Drawing.Size(182, 35);
            this.buttonImportGeneralMod.TabIndex = 7;
            this.buttonImportGeneralMod.Text = "Import General Mod";
            this.buttonImportGeneralMod.Click += new System.EventHandler(this.buttonImportGeneralMod_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1396, 1098);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanelGeneralButtons, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanelSlotButtons, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.53846F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.46154F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1396, 1098);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Active Slot Mods";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(990, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Inactive Slot Mods";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 677);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Active General Character Mods";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(938, 677);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(242, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Inactive General Character Mods";
            // 
            // tableLayoutPanelGeneralButtons
            // 
            this.tableLayoutPanelGeneralButtons.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelGeneralButtons.ColumnCount = 1;
            this.tableLayoutPanelGeneralButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGeneralButtons.Controls.Add(this.buttonGeneralDown, 0, 3);
            this.tableLayoutPanelGeneralButtons.Controls.Add(this.buttonGeneralLeft, 0, 0);
            this.tableLayoutPanelGeneralButtons.Controls.Add(this.buttonGeneralRight, 0, 1);
            this.tableLayoutPanelGeneralButtons.Controls.Add(this.buttonGeneralUp, 0, 2);
            this.tableLayoutPanelGeneralButtons.Location = new System.Drawing.Point(676, 817);
            this.tableLayoutPanelGeneralButtons.Name = "tableLayoutPanelGeneralButtons";
            this.tableLayoutPanelGeneralButtons.RowCount = 4;
            this.tableLayoutPanelGeneralButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGeneralButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGeneralButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGeneralButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGeneralButtons.Size = new System.Drawing.Size(44, 160);
            this.tableLayoutPanelGeneralButtons.TabIndex = 8;
            // 
            // buttonGeneralDown
            // 
            this.buttonGeneralDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGeneralDown.Enabled = false;
            this.buttonGeneralDown.Location = new System.Drawing.Point(0, 123);
            this.buttonGeneralDown.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonGeneralDown.Name = "buttonGeneralDown";
            this.buttonGeneralDown.Size = new System.Drawing.Size(44, 34);
            this.buttonGeneralDown.TabIndex = 7;
            this.buttonGeneralDown.Text = "↓";
            this.buttonGeneralDown.UseVisualStyleBackColor = true;
            this.buttonGeneralDown.Click += new System.EventHandler(this.buttonGeneralDown_Click);
            // 
            // buttonGeneralLeft
            // 
            this.buttonGeneralLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGeneralLeft.Enabled = false;
            this.buttonGeneralLeft.Location = new System.Drawing.Point(0, 3);
            this.buttonGeneralLeft.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonGeneralLeft.Name = "buttonGeneralLeft";
            this.buttonGeneralLeft.Size = new System.Drawing.Size(44, 34);
            this.buttonGeneralLeft.TabIndex = 6;
            this.buttonGeneralLeft.Text = "←";
            this.buttonGeneralLeft.UseVisualStyleBackColor = true;
            this.buttonGeneralLeft.Click += new System.EventHandler(this.buttonGeneralLeft_Click);
            // 
            // buttonGeneralRight
            // 
            this.buttonGeneralRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGeneralRight.Enabled = false;
            this.buttonGeneralRight.Location = new System.Drawing.Point(0, 43);
            this.buttonGeneralRight.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonGeneralRight.Name = "buttonGeneralRight";
            this.buttonGeneralRight.Size = new System.Drawing.Size(44, 34);
            this.buttonGeneralRight.TabIndex = 5;
            this.buttonGeneralRight.Text = "→";
            this.buttonGeneralRight.UseVisualStyleBackColor = true;
            this.buttonGeneralRight.Click += new System.EventHandler(this.buttonGeneralRight_Click);
            // 
            // buttonGeneralUp
            // 
            this.buttonGeneralUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGeneralUp.Enabled = false;
            this.buttonGeneralUp.Location = new System.Drawing.Point(0, 83);
            this.buttonGeneralUp.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonGeneralUp.Name = "buttonGeneralUp";
            this.buttonGeneralUp.Size = new System.Drawing.Size(44, 34);
            this.buttonGeneralUp.TabIndex = 4;
            this.buttonGeneralUp.Text = "↑";
            this.buttonGeneralUp.UseVisualStyleBackColor = true;
            this.buttonGeneralUp.Click += new System.EventHandler(this.buttonGeneralUp_Click);
            // 
            // tableLayoutPanelSlotButtons
            // 
            this.tableLayoutPanelSlotButtons.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelSlotButtons.ColumnCount = 1;
            this.tableLayoutPanelSlotButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSlotButtons.Controls.Add(this.buttonSlotBottom, 0, 4);
            this.tableLayoutPanelSlotButtons.Controls.Add(this.buttonSlotDown, 0, 3);
            this.tableLayoutPanelSlotButtons.Controls.Add(this.buttonSlotLeft, 0, 0);
            this.tableLayoutPanelSlotButtons.Controls.Add(this.buttonSlotRight, 0, 1);
            this.tableLayoutPanelSlotButtons.Controls.Add(this.buttonSlotUp, 0, 2);
            this.tableLayoutPanelSlotButtons.Location = new System.Drawing.Point(676, 248);
            this.tableLayoutPanelSlotButtons.Name = "tableLayoutPanelSlotButtons";
            this.tableLayoutPanelSlotButtons.RowCount = 5;
            this.tableLayoutPanelSlotButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSlotButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSlotButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSlotButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSlotButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSlotButtons.Size = new System.Drawing.Size(44, 200);
            this.tableLayoutPanelSlotButtons.TabIndex = 9;
            // 
            // buttonSlotBottom
            // 
            this.buttonSlotBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSlotBottom.Enabled = false;
            this.buttonSlotBottom.Location = new System.Drawing.Point(0, 163);
            this.buttonSlotBottom.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonSlotBottom.Name = "buttonSlotBottom";
            this.buttonSlotBottom.Size = new System.Drawing.Size(44, 34);
            this.buttonSlotBottom.TabIndex = 8;
            this.buttonSlotBottom.Text = "↓↓";
            this.buttonSlotBottom.UseVisualStyleBackColor = true;
            this.buttonSlotBottom.Click += new System.EventHandler(this.buttonSlotBottom_Click);
            // 
            // buttonSlotDown
            // 
            this.buttonSlotDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSlotDown.Enabled = false;
            this.buttonSlotDown.Location = new System.Drawing.Point(0, 123);
            this.buttonSlotDown.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonSlotDown.Name = "buttonSlotDown";
            this.buttonSlotDown.Size = new System.Drawing.Size(44, 34);
            this.buttonSlotDown.TabIndex = 7;
            this.buttonSlotDown.Text = "↓";
            this.buttonSlotDown.UseVisualStyleBackColor = true;
            this.buttonSlotDown.Click += new System.EventHandler(this.buttonSlotDown_Click);
            // 
            // buttonSlotLeft
            // 
            this.buttonSlotLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSlotLeft.Enabled = false;
            this.buttonSlotLeft.Location = new System.Drawing.Point(0, 3);
            this.buttonSlotLeft.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonSlotLeft.Name = "buttonSlotLeft";
            this.buttonSlotLeft.Size = new System.Drawing.Size(44, 34);
            this.buttonSlotLeft.TabIndex = 6;
            this.buttonSlotLeft.Text = "←";
            this.buttonSlotLeft.UseVisualStyleBackColor = true;
            this.buttonSlotLeft.Click += new System.EventHandler(this.buttonSlotLeft_Click);
            // 
            // buttonSlotRight
            // 
            this.buttonSlotRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSlotRight.Enabled = false;
            this.buttonSlotRight.Location = new System.Drawing.Point(0, 43);
            this.buttonSlotRight.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonSlotRight.Name = "buttonSlotRight";
            this.buttonSlotRight.Size = new System.Drawing.Size(44, 34);
            this.buttonSlotRight.TabIndex = 5;
            this.buttonSlotRight.Text = "→";
            this.buttonSlotRight.UseVisualStyleBackColor = true;
            this.buttonSlotRight.Click += new System.EventHandler(this.buttonSlotRight_Click);
            // 
            // buttonSlotUp
            // 
            this.buttonSlotUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSlotUp.Enabled = false;
            this.buttonSlotUp.Location = new System.Drawing.Point(0, 83);
            this.buttonSlotUp.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonSlotUp.Name = "buttonSlotUp";
            this.buttonSlotUp.Size = new System.Drawing.Size(44, 34);
            this.buttonSlotUp.TabIndex = 4;
            this.buttonSlotUp.Text = "↑";
            this.buttonSlotUp.UseVisualStyleBackColor = true;
            this.buttonSlotUp.Click += new System.EventHandler(this.buttonSlotUp_Click);
            // 
            // openFileDialogImportZip
            // 
            this.openFileDialogImportZip.Filter = "Zip files|*.zip";
            // 
            // buttonDeleteMod
            // 
            this.buttonDeleteMod.Location = new System.Drawing.Point(725, 3);
            this.buttonDeleteMod.Name = "buttonDeleteMod";
            this.buttonDeleteMod.Size = new System.Drawing.Size(120, 35);
            this.buttonDeleteMod.TabIndex = 8;
            this.buttonDeleteMod.Text = "Delete Mod";
            this.buttonDeleteMod.Click += new System.EventHandler(this.buttonDeleteMod_Click);
            // 
            // CharacterMods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CharacterMods";
            this.Size = new System.Drawing.Size(1396, 1135);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanelGeneralButtons.ResumeLayout(false);
            this.tableLayoutPanelSlotButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxCharacters;
        private System.Windows.Forms.Button buttonTextureIDFixAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGeneralButtons;
        private System.Windows.Forms.Button buttonGeneralDown;
        private System.Windows.Forms.Button buttonGeneralLeft;
        private System.Windows.Forms.Button buttonGeneralRight;
        private System.Windows.Forms.Button buttonGeneralUp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSlotButtons;
        private System.Windows.Forms.Button buttonSlotDown;
        private System.Windows.Forms.Button buttonSlotLeft;
        private System.Windows.Forms.Button buttonSlotRight;
        private System.Windows.Forms.Button buttonSlotUp;
        private System.Windows.Forms.Button buttonSlotBottom;
        private System.Windows.Forms.Button buttonImportSlotMod;
        private System.Windows.Forms.Button buttonImportGeneralMod;
        private System.Windows.Forms.OpenFileDialog openFileDialogImportZip;
        private System.Windows.Forms.Button buttonDeleteMod;
    }
}
