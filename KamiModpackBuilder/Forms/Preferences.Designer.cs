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
            this.groupBoxEditorMods.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxEditorMods
            // 
            this.groupBoxEditorMods.Controls.Add(this.label57);
            this.groupBoxEditorMods.Controls.Add(this.checkBoxDebug);
            this.groupBoxEditorMods.Controls.Add(this.label59);
            this.groupBoxEditorMods.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxEditorMods.Location = new System.Drawing.Point(0, 0);
            this.groupBoxEditorMods.Name = "groupBoxEditorMods";
            this.groupBoxEditorMods.Size = new System.Drawing.Size(803, 92);
            this.groupBoxEditorMods.TabIndex = 3;
            this.groupBoxEditorMods.TabStop = false;
            this.groupBoxEditorMods.Text = "Debug";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(220, 44);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(564, 20);
            this.label57.TabIndex = 22;
            this.label57.Text = "More output information to the console, keep .dec versions of rebuilt resources.";
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(168, 44);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(22, 21);
            this.checkBoxDebug.TabIndex = 21;
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(12, 44);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(115, 20);
            this.label59.TabIndex = 1;
            this.label59.Text = "Enable Debug:";
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 98);
            this.Controls.Add(this.groupBoxEditorMods);
            this.Name = "Preferences";
            this.Text = "Application Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Preferences_FormClosed);
            this.groupBoxEditorMods.ResumeLayout(false);
            this.groupBoxEditorMods.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxEditorMods;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.Label label59;
    }
}