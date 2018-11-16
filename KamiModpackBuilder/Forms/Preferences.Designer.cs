namespace KamiModpackBuilder.Forms
{
    partial class Preferences
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
            this.groupBoxEditorMods = new System.Windows.Forms.GroupBox();
            this.label57 = new System.Windows.Forms.Label();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.label59 = new System.Windows.Forms.Label();
            this.groupBoxDirectories = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonBrowseHexEditor = new System.Windows.Forms.Button();
            this.textBoxHexEditor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonResetTips = new System.Windows.Forms.Button();
            this.groupBoxEditorMods.SuspendLayout();
            this.groupBoxDirectories.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxEditorMods
            // 
            this.groupBoxEditorMods.Controls.Add(this.label57);
            this.groupBoxEditorMods.Controls.Add(this.checkBoxDebug);
            this.groupBoxEditorMods.Controls.Add(this.label59);
            this.groupBoxEditorMods.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxEditorMods.Location = new System.Drawing.Point(0, 105);
            this.groupBoxEditorMods.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxEditorMods.Name = "groupBoxEditorMods";
            this.groupBoxEditorMods.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxEditorMods.Size = new System.Drawing.Size(597, 60);
            this.groupBoxEditorMods.TabIndex = 3;
            this.groupBoxEditorMods.TabStop = false;
            this.groupBoxEditorMods.Text = "Debug";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(147, 29);
            this.label57.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(379, 13);
            this.label57.TabIndex = 22;
            this.label57.Text = "More output information to the console, keep .dec versions of rebuilt resources.";
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(112, 29);
            this.checkBoxDebug.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDebug.TabIndex = 21;
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(8, 29);
            this.label59.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(78, 13);
            this.label59.TabIndex = 1;
            this.label59.Text = "Enable Debug:";
            // 
            // groupBoxDirectories
            // 
            this.groupBoxDirectories.Controls.Add(this.buttonResetTips);
            this.groupBoxDirectories.Controls.Add(this.label11);
            this.groupBoxDirectories.Controls.Add(this.buttonBrowseHexEditor);
            this.groupBoxDirectories.Controls.Add(this.textBoxHexEditor);
            this.groupBoxDirectories.Controls.Add(this.label1);
            this.groupBoxDirectories.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxDirectories.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDirectories.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxDirectories.Name = "groupBoxDirectories";
            this.groupBoxDirectories.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxDirectories.Size = new System.Drawing.Size(597, 105);
            this.groupBoxDirectories.TabIndex = 4;
            this.groupBoxDirectories.TabStop = false;
            this.groupBoxDirectories.Text = "Misc";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(371, 29);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(153, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Program to use for Hex Editing.";
            // 
            // buttonBrowseHexEditor
            // 
            this.buttonBrowseHexEditor.Location = new System.Drawing.Point(317, 26);
            this.buttonBrowseHexEditor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonBrowseHexEditor.Name = "buttonBrowseHexEditor";
            this.buttonBrowseHexEditor.Size = new System.Drawing.Size(50, 21);
            this.buttonBrowseHexEditor.TabIndex = 2;
            this.buttonBrowseHexEditor.Text = "Browse";
            this.buttonBrowseHexEditor.UseVisualStyleBackColor = true;
            this.buttonBrowseHexEditor.Click += new System.EventHandler(this.buttonBrowseHexEditor_Click);
            // 
            // textBoxHexEditor
            // 
            this.textBoxHexEditor.Location = new System.Drawing.Point(113, 26);
            this.textBoxHexEditor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxHexEditor.Name = "textBoxHexEditor";
            this.textBoxHexEditor.Size = new System.Drawing.Size(201, 20);
            this.textBoxHexEditor.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hex Editor:";
            // 
            // buttonResetTips
            // 
            this.buttonResetTips.Location = new System.Drawing.Point(267, 66);
            this.buttonResetTips.Name = "buttonResetTips";
            this.buttonResetTips.Size = new System.Drawing.Size(80, 23);
            this.buttonResetTips.TabIndex = 4;
            this.buttonResetTips.Text = "Reset Tips";
            this.buttonResetTips.UseVisualStyleBackColor = true;
            this.buttonResetTips.Click += new System.EventHandler(this.buttonResetTips_Click);
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(597, 167);
            this.Controls.Add(this.groupBoxEditorMods);
            this.Controls.Add(this.groupBoxDirectories);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Preferences";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Preferences_FormClosed);
            this.groupBoxEditorMods.ResumeLayout(false);
            this.groupBoxEditorMods.PerformLayout();
            this.groupBoxDirectories.ResumeLayout(false);
            this.groupBoxDirectories.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxEditorMods;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.GroupBox groupBoxDirectories;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonBrowseHexEditor;
        private System.Windows.Forms.TextBox textBoxHexEditor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonResetTips;
    }
}