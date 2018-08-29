namespace KamiModpackBuilder.Forms
{
    partial class CreationProjectInfo
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblGameRegion = new System.Windows.Forms.Label();
            this.lblGameVersion = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtGameVersion = new System.Windows.Forms.NumericUpDown();
            this.ddpGameRegion = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtGameVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(18, 14);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(638, 20);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "In order for Sm4shExplorer to work as intended the following information must be " +
    "correct :";
            // 
            // lblGameRegion
            // 
            this.lblGameRegion.AutoSize = true;
            this.lblGameRegion.Location = new System.Drawing.Point(18, 57);
            this.lblGameRegion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGameRegion.Name = "lblGameRegion";
            this.lblGameRegion.Size = new System.Drawing.Size(120, 20);
            this.lblGameRegion.TabIndex = 1;
            this.lblGameRegion.Text = "Game Region : ";
            // 
            // lblGameVersion
            // 
            this.lblGameVersion.AutoSize = true;
            this.lblGameVersion.Location = new System.Drawing.Point(18, 97);
            this.lblGameVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGameVersion.Name = "lblGameVersion";
            this.lblGameVersion.Size = new System.Drawing.Size(123, 20);
            this.lblGameVersion.TabIndex = 2;
            this.lblGameVersion.Text = "Game Version : ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(564, 126);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 35);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtGameVersion
            // 
            this.txtGameVersion.Location = new System.Drawing.Point(150, 94);
            this.txtGameVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtGameVersion.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.txtGameVersion.Name = "txtGameVersion";
            this.txtGameVersion.Size = new System.Drawing.Size(180, 26);
            this.txtGameVersion.TabIndex = 4;
            // 
            // ddpGameRegion
            // 
            this.ddpGameRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpGameRegion.FormattingEnabled = true;
            this.ddpGameRegion.Location = new System.Drawing.Point(148, 52);
            this.ddpGameRegion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ddpGameRegion.Name = "ddpGameRegion";
            this.ddpGameRegion.Size = new System.Drawing.Size(180, 28);
            this.ddpGameRegion.TabIndex = 5;
            this.ddpGameRegion.VisibleChanged += new System.EventHandler(this.ddpGameRegion_VisibleChanged);
            // 
            // CreationProjectInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 168);
            this.ControlBox = false;
            this.Controls.Add(this.ddpGameRegion);
            this.Controls.Add(this.txtGameVersion);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblGameVersion);
            this.Controls.Add(this.lblGameRegion);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreationProjectInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Create Project";
            ((System.ComponentModel.ISupportInitialize)(this.txtGameVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblGameRegion;
        private System.Windows.Forms.Label lblGameVersion;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown txtGameVersion;
        private System.Windows.Forms.ComboBox ddpGameRegion;
    }
}