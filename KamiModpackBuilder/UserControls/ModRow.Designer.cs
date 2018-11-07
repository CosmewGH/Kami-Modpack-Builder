namespace KamiModpackBuilder.UserControls
{
    partial class ModRow
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
            this.components = new System.ComponentModel.Container();
            this.panelModList = new System.Windows.Forms.Panel();
            this.labelAudio = new System.Windows.Forms.Label();
            this.labelCustomName = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.labelPortraits = new System.Windows.Forms.Label();
            this.labelMetal = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelWifi = new System.Windows.Forms.Label();
            this.labelModName = new System.Windows.Forms.LabelNoCopy();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelModList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelModList
            // 
            this.panelModList.BackColor = System.Drawing.SystemColors.Control;
            this.panelModList.Controls.Add(this.labelAudio);
            this.panelModList.Controls.Add(this.labelCustomName);
            this.panelModList.Controls.Add(this.labelError);
            this.panelModList.Controls.Add(this.labelPortraits);
            this.panelModList.Controls.Add(this.labelMetal);
            this.panelModList.Controls.Add(this.labelModel);
            this.panelModList.Controls.Add(this.labelWifi);
            this.panelModList.Controls.Add(this.labelModName);
            this.panelModList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModList.Location = new System.Drawing.Point(0, 0);
            this.panelModList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelModList.Name = "panelModList";
            this.panelModList.Size = new System.Drawing.Size(892, 28);
            this.panelModList.TabIndex = 0;
            this.panelModList.Click += new System.EventHandler(this.panelModList_Click);
            this.panelModList.DoubleClick += new System.EventHandler(this.panelModList_DoubleClick);
            this.panelModList.MouseEnter += new System.EventHandler(this.panelModList_MouseEnter);
            this.panelModList.MouseLeave += new System.EventHandler(this.panelModList_MouseLeave);
            // 
            // labelAudio
            // 
            this.labelAudio.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelAudio.Image = global::KamiModpackBuilder.Properties.Resources.icon_audio;
            this.labelAudio.Location = new System.Drawing.Point(776, 2);
            this.labelAudio.Name = "labelAudio";
            this.labelAudio.Size = new System.Drawing.Size(24, 24);
            this.labelAudio.TabIndex = 13;
            this.labelAudio.Visible = false;
            this.labelAudio.Click += new System.EventHandler(this.label_Click);
            this.labelAudio.DoubleClick += new System.EventHandler(this.label_DoubleClick);
            this.labelAudio.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelAudio.MouseLeave += new System.EventHandler(this.panelModList_MouseLeave);
            // 
            // labelCustomName
            // 
            this.labelCustomName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCustomName.Image = global::KamiModpackBuilder.Properties.Resources.icon_custom_name;
            this.labelCustomName.Location = new System.Drawing.Point(806, 2);
            this.labelCustomName.Name = "labelCustomName";
            this.labelCustomName.Size = new System.Drawing.Size(24, 24);
            this.labelCustomName.TabIndex = 12;
            this.labelCustomName.Visible = false;
            this.labelCustomName.Click += new System.EventHandler(this.label_Click);
            this.labelCustomName.DoubleClick += new System.EventHandler(this.label_DoubleClick);
            this.labelCustomName.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelCustomName.MouseLeave += new System.EventHandler(this.panelModList_MouseLeave);
            // 
            // labelError
            // 
            this.labelError.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelError.Image = global::KamiModpackBuilder.Properties.Resources.icon_error;
            this.labelError.Location = new System.Drawing.Point(866, 2);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(24, 24);
            this.labelError.TabIndex = 11;
            this.labelError.Visible = false;
            this.labelError.Click += new System.EventHandler(this.label_Click);
            this.labelError.DoubleClick += new System.EventHandler(this.label_DoubleClick);
            this.labelError.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelError.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // labelPortraits
            // 
            this.labelPortraits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelPortraits.Image = global::KamiModpackBuilder.Properties.Resources.icon_portrait_missing;
            this.labelPortraits.Location = new System.Drawing.Point(716, 2);
            this.labelPortraits.Name = "labelPortraits";
            this.labelPortraits.Size = new System.Drawing.Size(24, 24);
            this.labelPortraits.TabIndex = 10;
            this.labelPortraits.Visible = false;
            this.labelPortraits.Click += new System.EventHandler(this.label_Click);
            this.labelPortraits.DoubleClick += new System.EventHandler(this.label_DoubleClick);
            this.labelPortraits.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelPortraits.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // labelMetal
            // 
            this.labelMetal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMetal.Image = global::KamiModpackBuilder.Properties.Resources.icon_metal_unknown;
            this.labelMetal.Location = new System.Drawing.Point(746, 2);
            this.labelMetal.Name = "labelMetal";
            this.labelMetal.Size = new System.Drawing.Size(24, 24);
            this.labelMetal.TabIndex = 9;
            this.labelMetal.Visible = false;
            this.labelMetal.Click += new System.EventHandler(this.label_Click);
            this.labelMetal.DoubleClick += new System.EventHandler(this.label_DoubleClick);
            this.labelMetal.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelMetal.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // labelModel
            // 
            this.labelModel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelModel.Image = global::KamiModpackBuilder.Properties.Resources.icon_models_missing;
            this.labelModel.Location = new System.Drawing.Point(686, 2);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(24, 24);
            this.labelModel.TabIndex = 8;
            this.labelModel.Visible = false;
            this.labelModel.Click += new System.EventHandler(this.label_Click);
            this.labelModel.DoubleClick += new System.EventHandler(this.label_DoubleClick);
            this.labelModel.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelModel.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // labelWifi
            // 
            this.labelWifi.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelWifi.Image = global::KamiModpackBuilder.Properties.Resources.icon_wifi_safe_no;
            this.labelWifi.Location = new System.Drawing.Point(836, 2);
            this.labelWifi.Name = "labelWifi";
            this.labelWifi.Size = new System.Drawing.Size(24, 24);
            this.labelWifi.TabIndex = 7;
            this.labelWifi.Visible = false;
            this.labelWifi.Click += new System.EventHandler(this.label_Click);
            this.labelWifi.DoubleClick += new System.EventHandler(this.label_DoubleClick);
            this.labelWifi.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelWifi.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // labelModName
            // 
            this.labelModName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelModName.AutoSize = true;
            this.labelModName.Location = new System.Drawing.Point(4, 4);
            this.labelModName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelModName.Name = "labelModName";
            this.labelModName.Size = new System.Drawing.Size(86, 20);
            this.labelModName.TabIndex = 2;
            this.labelModName.Text = "Mod Name";
            this.labelModName.Click += new System.EventHandler(this.label_Click);
            this.labelModName.DoubleClick += new System.EventHandler(this.label_DoubleClick);
            this.labelModName.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelModName.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // ModRow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelModList);
            this.Name = "ModRow";
            this.Size = new System.Drawing.Size(892, 28);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModRow_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.ModRow_DragOver);
            this.panelModList.ResumeLayout(false);
            this.panelModList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelModList;
        private System.Windows.Forms.LabelNoCopy labelModName;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelWifi;
        private System.Windows.Forms.Label labelPortraits;
        private System.Windows.Forms.Label labelMetal;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label labelAudio;
        private System.Windows.Forms.Label labelCustomName;
    }
}
