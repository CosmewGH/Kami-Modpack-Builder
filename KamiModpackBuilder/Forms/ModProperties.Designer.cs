namespace KamiModpackBuilder.Forms
{
    partial class ModProperties
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDisplayName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.checkBoxWifiSafe = new System.Windows.Forms.CheckBox();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.groupBoxStageData = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelStageName = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBoxPortaits = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.labelImage_stage11 = new System.Windows.Forms.Label();
            this.labelImage_stage12 = new System.Windows.Forms.Label();
            this.labelImage_stage00 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelImage_stage30 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelImage_stagen10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonImport_stage10 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonExport_stage10 = new System.Windows.Forms.Button();
            this.buttonImport_stage11 = new System.Windows.Forms.Button();
            this.buttonExport_stagen10 = new System.Windows.Forms.Button();
            this.buttonExport_stage11 = new System.Windows.Forms.Button();
            this.buttonImport_stagen10 = new System.Windows.Forms.Button();
            this.buttonImport_stage12 = new System.Windows.Forms.Button();
            this.buttonExport_stage30 = new System.Windows.Forms.Button();
            this.buttonExport_stage12 = new System.Windows.Forms.Button();
            this.buttonImport_stage30 = new System.Windows.Forms.Button();
            this.panelNotes = new System.Windows.Forms.Panel();
            this.openFileDialogPortraits = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.labelImage_stage13 = new System.Windows.Forms.Label();
            this.buttonImport_stage13 = new System.Windows.Forms.Button();
            this.buttonExport_stage13 = new System.Windows.Forms.Button();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxStageData.SuspendLayout();
            this.groupBoxPortaits.SuspendLayout();
            this.panelNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Display Name:";
            // 
            // textBoxDisplayName
            // 
            this.textBoxDisplayName.Location = new System.Drawing.Point(174, 40);
            this.textBoxDisplayName.Name = "textBoxDisplayName";
            this.textBoxDisplayName.Size = new System.Drawing.Size(200, 26);
            this.textBoxDisplayName.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "The name shown in Kami Modpack Builder.";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(20, 22);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(55, 20);
            this.label24.TabIndex = 30;
            this.label24.Text = "Notes:";
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Location = new System.Drawing.Point(122, 17);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(818, 96);
            this.textBoxNotes.TabIndex = 29;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 85);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(78, 20);
            this.label20.TabIndex = 46;
            this.label20.Text = "Wifi-Safe:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(225, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(524, 20);
            this.label21.TabIndex = 48;
            this.label21.Text = "If this mod is safe to be used online. (Does not change physics of battles)";
            // 
            // checkBoxWifiSafe
            // 
            this.checkBoxWifiSafe.AutoSize = true;
            this.checkBoxWifiSafe.Location = new System.Drawing.Point(172, 85);
            this.checkBoxWifiSafe.Name = "checkBoxWifiSafe";
            this.checkBoxWifiSafe.Size = new System.Drawing.Size(22, 21);
            this.checkBoxWifiSafe.TabIndex = 47;
            this.checkBoxWifiSafe.UseVisualStyleBackColor = true;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.label1);
            this.groupBoxInfo.Controls.Add(this.checkBoxWifiSafe);
            this.groupBoxInfo.Controls.Add(this.label21);
            this.groupBoxInfo.Controls.Add(this.textBoxDisplayName);
            this.groupBoxInfo.Controls.Add(this.label2);
            this.groupBoxInfo.Controls.Add(this.label20);
            this.groupBoxInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(974, 130);
            this.groupBoxInfo.TabIndex = 49;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Info";
            // 
            // groupBoxStageData
            // 
            this.groupBoxStageData.Controls.Add(this.label12);
            this.groupBoxStageData.Controls.Add(this.labelStageName);
            this.groupBoxStageData.Controls.Add(this.label14);
            this.groupBoxStageData.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxStageData.Location = new System.Drawing.Point(0, 130);
            this.groupBoxStageData.Name = "groupBoxStageData";
            this.groupBoxStageData.Size = new System.Drawing.Size(974, 114);
            this.groupBoxStageData.TabIndex = 50;
            this.groupBoxStageData.TabStop = false;
            this.groupBoxStageData.Text = "Stage Data";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 45);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 20);
            this.label12.TabIndex = 34;
            this.label12.Text = "Stage:";
            // 
            // labelStageName
            // 
            this.labelStageName.AutoSize = true;
            this.labelStageName.Location = new System.Drawing.Point(168, 45);
            this.labelStageName.Name = "labelStageName";
            this.labelStageName.Size = new System.Drawing.Size(94, 20);
            this.labelStageName.TabIndex = 35;
            this.labelStageName.Text = "StageName";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(400, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(547, 60);
            this.label14.TabIndex = 36;
            this.label14.Text = "The stage the mod is over.\r\nTo change the stage, you must re-import the stage wit" +
    "hout the .kamimod file,\r\nand with new file and folder names to match the desired" +
    " stage.";
            // 
            // groupBoxPortaits
            // 
            this.groupBoxPortaits.Controls.Add(this.label3);
            this.groupBoxPortaits.Controls.Add(this.labelImage_stage13);
            this.groupBoxPortaits.Controls.Add(this.buttonImport_stage13);
            this.groupBoxPortaits.Controls.Add(this.buttonExport_stage13);
            this.groupBoxPortaits.Controls.Add(this.label7);
            this.groupBoxPortaits.Controls.Add(this.labelImage_stage11);
            this.groupBoxPortaits.Controls.Add(this.labelImage_stage12);
            this.groupBoxPortaits.Controls.Add(this.labelImage_stage00);
            this.groupBoxPortaits.Controls.Add(this.label11);
            this.groupBoxPortaits.Controls.Add(this.labelImage_stage30);
            this.groupBoxPortaits.Controls.Add(this.label10);
            this.groupBoxPortaits.Controls.Add(this.labelImage_stagen10);
            this.groupBoxPortaits.Controls.Add(this.label9);
            this.groupBoxPortaits.Controls.Add(this.buttonImport_stage10);
            this.groupBoxPortaits.Controls.Add(this.label8);
            this.groupBoxPortaits.Controls.Add(this.buttonExport_stage10);
            this.groupBoxPortaits.Controls.Add(this.buttonImport_stage11);
            this.groupBoxPortaits.Controls.Add(this.buttonExport_stagen10);
            this.groupBoxPortaits.Controls.Add(this.buttonExport_stage11);
            this.groupBoxPortaits.Controls.Add(this.buttonImport_stagen10);
            this.groupBoxPortaits.Controls.Add(this.buttonImport_stage12);
            this.groupBoxPortaits.Controls.Add(this.buttonExport_stage30);
            this.groupBoxPortaits.Controls.Add(this.buttonExport_stage12);
            this.groupBoxPortaits.Controls.Add(this.buttonImport_stage30);
            this.groupBoxPortaits.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPortaits.Location = new System.Drawing.Point(0, 244);
            this.groupBoxPortaits.Name = "groupBoxPortaits";
            this.groupBoxPortaits.Size = new System.Drawing.Size(974, 298);
            this.groupBoxPortaits.TabIndex = 54;
            this.groupBoxPortaits.TabStop = false;
            this.groupBoxPortaits.Text = "Portraits";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "stage_10\r\n";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelImage_stage11
            // 
            this.labelImage_stage11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelImage_stage11.Location = new System.Drawing.Point(148, 112);
            this.labelImage_stage11.Name = "labelImage_stage11";
            this.labelImage_stage11.Size = new System.Drawing.Size(128, 64);
            this.labelImage_stage11.TabIndex = 0;
            // 
            // labelImage_stage12
            // 
            this.labelImage_stage12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelImage_stage12.Location = new System.Drawing.Point(288, 112);
            this.labelImage_stage12.Name = "labelImage_stage12";
            this.labelImage_stage12.Size = new System.Drawing.Size(128, 64);
            this.labelImage_stage12.TabIndex = 1;
            // 
            // labelImage_stage00
            // 
            this.labelImage_stage00.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelImage_stage00.Location = new System.Drawing.Point(8, 102);
            this.labelImage_stage00.Name = "labelImage_stage00";
            this.labelImage_stage00.Size = new System.Drawing.Size(128, 84);
            this.labelImage_stage00.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(632, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(123, 40);
            this.label11.TabIndex = 28;
            this.label11.Text = "stagen_10\r\nSSS Nameplate";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelImage_stage30
            // 
            this.labelImage_stage30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelImage_stage30.Location = new System.Drawing.Point(428, 80);
            this.labelImage_stage30.Name = "labelImage_stage30";
            this.labelImage_stage30.Size = new System.Drawing.Size(128, 128);
            this.labelImage_stage30.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(453, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 20);
            this.label10.TabIndex = 27;
            this.label10.Text = "stage_30\r\n";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelImage_stagen10
            // 
            this.labelImage_stagen10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelImage_stagen10.Location = new System.Drawing.Point(568, 112);
            this.labelImage_stagen10.Name = "labelImage_stagen10";
            this.labelImage_stagen10.Size = new System.Drawing.Size(256, 64);
            this.labelImage_stagen10.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(310, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 20);
            this.label9.TabIndex = 26;
            this.label9.Text = "stage_12\r\n";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonImport_stage10
            // 
            this.buttonImport_stage10.Location = new System.Drawing.Point(34, 215);
            this.buttonImport_stage10.Name = "buttonImport_stage10";
            this.buttonImport_stage10.Size = new System.Drawing.Size(75, 32);
            this.buttonImport_stage10.TabIndex = 5;
            this.buttonImport_stage10.Text = "Import";
            this.buttonImport_stage10.UseVisualStyleBackColor = true;
            this.buttonImport_stage10.Click += new System.EventHandler(this.buttonImport_stage10_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(175, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 20);
            this.label8.TabIndex = 25;
            this.label8.Text = "stage_11\r\n";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonExport_stage10
            // 
            this.buttonExport_stage10.Location = new System.Drawing.Point(34, 249);
            this.buttonExport_stage10.Name = "buttonExport_stage10";
            this.buttonExport_stage10.Size = new System.Drawing.Size(75, 32);
            this.buttonExport_stage10.TabIndex = 6;
            this.buttonExport_stage10.Text = "Export";
            this.buttonExport_stage10.UseVisualStyleBackColor = true;
            this.buttonExport_stage10.Click += new System.EventHandler(this.buttonExport_stage10_Click);
            // 
            // buttonImport_stage11
            // 
            this.buttonImport_stage11.Location = new System.Drawing.Point(176, 215);
            this.buttonImport_stage11.Name = "buttonImport_stage11";
            this.buttonImport_stage11.Size = new System.Drawing.Size(75, 32);
            this.buttonImport_stage11.TabIndex = 7;
            this.buttonImport_stage11.Text = "Import";
            this.buttonImport_stage11.UseVisualStyleBackColor = true;
            this.buttonImport_stage11.Click += new System.EventHandler(this.buttonImport_stage11_Click);
            // 
            // buttonExport_stagen10
            // 
            this.buttonExport_stagen10.Location = new System.Drawing.Point(658, 249);
            this.buttonExport_stagen10.Name = "buttonExport_stagen10";
            this.buttonExport_stagen10.Size = new System.Drawing.Size(75, 32);
            this.buttonExport_stagen10.TabIndex = 14;
            this.buttonExport_stagen10.Text = "Export";
            this.buttonExport_stagen10.UseVisualStyleBackColor = true;
            this.buttonExport_stagen10.Click += new System.EventHandler(this.buttonExport_stagen10_Click);
            // 
            // buttonExport_stage11
            // 
            this.buttonExport_stage11.Location = new System.Drawing.Point(176, 249);
            this.buttonExport_stage11.Name = "buttonExport_stage11";
            this.buttonExport_stage11.Size = new System.Drawing.Size(75, 32);
            this.buttonExport_stage11.TabIndex = 8;
            this.buttonExport_stage11.Text = "Export";
            this.buttonExport_stage11.UseVisualStyleBackColor = true;
            this.buttonExport_stage11.Click += new System.EventHandler(this.buttonExport_stage11_Click);
            // 
            // buttonImport_stagen10
            // 
            this.buttonImport_stagen10.Location = new System.Drawing.Point(658, 215);
            this.buttonImport_stagen10.Name = "buttonImport_stagen10";
            this.buttonImport_stagen10.Size = new System.Drawing.Size(75, 32);
            this.buttonImport_stagen10.TabIndex = 13;
            this.buttonImport_stagen10.Text = "Import";
            this.buttonImport_stagen10.UseVisualStyleBackColor = true;
            this.buttonImport_stagen10.Click += new System.EventHandler(this.buttonImport_stagen10_Click);
            // 
            // buttonImport_stage12
            // 
            this.buttonImport_stage12.Location = new System.Drawing.Point(315, 215);
            this.buttonImport_stage12.Name = "buttonImport_stage12";
            this.buttonImport_stage12.Size = new System.Drawing.Size(75, 32);
            this.buttonImport_stage12.TabIndex = 9;
            this.buttonImport_stage12.Text = "Import";
            this.buttonImport_stage12.UseVisualStyleBackColor = true;
            this.buttonImport_stage12.Click += new System.EventHandler(this.buttonImport_stage12_Click);
            // 
            // buttonExport_stage30
            // 
            this.buttonExport_stage30.Location = new System.Drawing.Point(454, 249);
            this.buttonExport_stage30.Name = "buttonExport_stage30";
            this.buttonExport_stage30.Size = new System.Drawing.Size(75, 32);
            this.buttonExport_stage30.TabIndex = 12;
            this.buttonExport_stage30.Text = "Export";
            this.buttonExport_stage30.UseVisualStyleBackColor = true;
            this.buttonExport_stage30.Click += new System.EventHandler(this.buttonExport_stage30_Click);
            // 
            // buttonExport_stage12
            // 
            this.buttonExport_stage12.Location = new System.Drawing.Point(315, 249);
            this.buttonExport_stage12.Name = "buttonExport_stage12";
            this.buttonExport_stage12.Size = new System.Drawing.Size(75, 32);
            this.buttonExport_stage12.TabIndex = 10;
            this.buttonExport_stage12.Text = "Export";
            this.buttonExport_stage12.UseVisualStyleBackColor = true;
            this.buttonExport_stage12.Click += new System.EventHandler(this.buttonExport_stage12_Click);
            // 
            // buttonImport_stage30
            // 
            this.buttonImport_stage30.Location = new System.Drawing.Point(454, 215);
            this.buttonImport_stage30.Name = "buttonImport_stage30";
            this.buttonImport_stage30.Size = new System.Drawing.Size(75, 32);
            this.buttonImport_stage30.TabIndex = 11;
            this.buttonImport_stage30.Text = "Import";
            this.buttonImport_stage30.UseVisualStyleBackColor = true;
            this.buttonImport_stage30.Click += new System.EventHandler(this.buttonImport_stage30_Click);
            // 
            // panelNotes
            // 
            this.panelNotes.Controls.Add(this.label24);
            this.panelNotes.Controls.Add(this.textBoxNotes);
            this.panelNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNotes.Location = new System.Drawing.Point(0, 542);
            this.panelNotes.Name = "panelNotes";
            this.panelNotes.Size = new System.Drawing.Size(974, 127);
            this.panelNotes.TabIndex = 55;
            // 
            // openFileDialogPortraits
            // 
            this.openFileDialogPortraits.Filter = "nut files|*.nut|PNG files|*.png";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(835, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 40);
            this.label3.TabIndex = 32;
            this.label3.Text = "stage_13\r\nDLC SSS Portrait";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelImage_stage13
            // 
            this.labelImage_stage13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelImage_stage13.Location = new System.Drawing.Point(836, 96);
            this.labelImage_stage13.Name = "labelImage_stage13";
            this.labelImage_stage13.Size = new System.Drawing.Size(128, 96);
            this.labelImage_stage13.TabIndex = 29;
            // 
            // buttonImport_stage13
            // 
            this.buttonImport_stage13.Location = new System.Drawing.Point(862, 215);
            this.buttonImport_stage13.Name = "buttonImport_stage13";
            this.buttonImport_stage13.Size = new System.Drawing.Size(75, 32);
            this.buttonImport_stage13.TabIndex = 30;
            this.buttonImport_stage13.Text = "Import";
            this.buttonImport_stage13.UseVisualStyleBackColor = true;
            this.buttonImport_stage13.Click += new System.EventHandler(this.buttonImport_stage13_Click);
            // 
            // buttonExport_stage13
            // 
            this.buttonExport_stage13.Location = new System.Drawing.Point(862, 249);
            this.buttonExport_stage13.Name = "buttonExport_stage13";
            this.buttonExport_stage13.Size = new System.Drawing.Size(75, 32);
            this.buttonExport_stage13.TabIndex = 31;
            this.buttonExport_stage13.Text = "Export";
            this.buttonExport_stage13.UseVisualStyleBackColor = true;
            this.buttonExport_stage13.Click += new System.EventHandler(this.buttonExport_stage13_Click);
            // 
            // ModProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 670);
            this.Controls.Add(this.panelNotes);
            this.Controls.Add(this.groupBoxPortaits);
            this.Controls.Add(this.groupBoxStageData);
            this.Controls.Add(this.groupBoxInfo);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ModProperties";
            this.Text = "Mod Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModProperties_FormClosing);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxStageData.ResumeLayout(false);
            this.groupBoxStageData.PerformLayout();
            this.groupBoxPortaits.ResumeLayout(false);
            this.groupBoxPortaits.PerformLayout();
            this.panelNotes.ResumeLayout(false);
            this.panelNotes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDisplayName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox checkBoxWifiSafe;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.GroupBox groupBoxStageData;
        private System.Windows.Forms.GroupBox groupBoxPortaits;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelImage_stage11;
        private System.Windows.Forms.Label labelImage_stage12;
        private System.Windows.Forms.Label labelImage_stage00;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelImage_stage30;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelImage_stagen10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonImport_stage10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonExport_stage10;
        private System.Windows.Forms.Button buttonImport_stage11;
        private System.Windows.Forms.Button buttonExport_stagen10;
        private System.Windows.Forms.Button buttonExport_stage11;
        private System.Windows.Forms.Button buttonImport_stagen10;
        private System.Windows.Forms.Button buttonImport_stage12;
        private System.Windows.Forms.Button buttonExport_stage30;
        private System.Windows.Forms.Button buttonExport_stage12;
        private System.Windows.Forms.Button buttonImport_stage30;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelStageName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panelNotes;
        private System.Windows.Forms.OpenFileDialog openFileDialogPortraits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelImage_stage13;
        private System.Windows.Forms.Button buttonImport_stage13;
        private System.Windows.Forms.Button buttonExport_stage13;
    }
}