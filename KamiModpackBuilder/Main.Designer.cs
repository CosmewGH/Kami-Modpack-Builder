namespace KamiModpackBuilder
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutKamiModpackBuilderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCharacterMods = new System.Windows.Forms.TabPage();
            this.tabPageStageMods = new System.Windows.Forms.TabPage();
            this.tabPageGeneralMods = new System.Windows.Forms.TabPage();
            this.tabPageMusic = new System.Windows.Forms.TabPage();
            this.tabPageCSS = new System.Windows.Forms.TabPage();
            this.tabPageSSS = new System.Windows.Forms.TabPage();
            this.tabPageExplorer = new System.Windows.Forms.TabPage();
            this.textConsole = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1291, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.toolStripSeparator1,
            this.preferencesToolStripMenuItem,
            this.projectSettingsToolStripMenuItem,
            this.toolStripSeparator2,
            this.buildToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(255, 30);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(255, 30);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(255, 30);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.SaveProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(252, 6);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(255, 30);
            this.preferencesToolStripMenuItem.Text = "Application Settings";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // projectSettingsToolStripMenuItem
            // 
            this.projectSettingsToolStripMenuItem.Name = "projectSettingsToolStripMenuItem";
            this.projectSettingsToolStripMenuItem.Size = new System.Drawing.Size(255, 30);
            this.projectSettingsToolStripMenuItem.Text = "Project Settings";
            this.projectSettingsToolStripMenuItem.Click += new System.EventHandler(this.projectSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(252, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(255, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutKamiModpackBuilderToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(74, 29);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutKamiModpackBuilderToolStripMenuItem
            // 
            this.aboutKamiModpackBuilderToolStripMenuItem.Name = "aboutKamiModpackBuilderToolStripMenuItem";
            this.aboutKamiModpackBuilderToolStripMenuItem.Size = new System.Drawing.Size(329, 30);
            this.aboutKamiModpackBuilderToolStripMenuItem.Text = "About Kami Modpack Builder";
            this.aboutKamiModpackBuilderToolStripMenuItem.Click += new System.EventHandler(this.aboutKamiModpackBuilderToolStripMenuItem_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 33);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textConsole);
            this.splitContainer1.Size = new System.Drawing.Size(1291, 952);
            this.splitContainer1.SplitterDistance = 828;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCharacterMods);
            this.tabControl.Controls.Add(this.tabPageStageMods);
            this.tabControl.Controls.Add(this.tabPageGeneralMods);
            this.tabControl.Controls.Add(this.tabPageMusic);
            this.tabControl.Controls.Add(this.tabPageCSS);
            this.tabControl.Controls.Add(this.tabPageSSS);
            this.tabControl.Controls.Add(this.tabPageExplorer);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1291, 828);
            this.tabControl.TabIndex = 2;
            // 
            // tabPageCharacterMods
            // 
            this.tabPageCharacterMods.Location = new System.Drawing.Point(4, 29);
            this.tabPageCharacterMods.Name = "tabPageCharacterMods";
            this.tabPageCharacterMods.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCharacterMods.Size = new System.Drawing.Size(1283, 795);
            this.tabPageCharacterMods.TabIndex = 0;
            this.tabPageCharacterMods.Text = "Character Mods";
            this.tabPageCharacterMods.UseVisualStyleBackColor = true;
            // 
            // tabPageStageMods
            // 
            this.tabPageStageMods.Location = new System.Drawing.Point(4, 29);
            this.tabPageStageMods.Name = "tabPageStageMods";
            this.tabPageStageMods.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStageMods.Size = new System.Drawing.Size(1283, 795);
            this.tabPageStageMods.TabIndex = 1;
            this.tabPageStageMods.Text = "Stage Mods";
            this.tabPageStageMods.UseVisualStyleBackColor = true;
            // 
            // tabPageGeneralMods
            // 
            this.tabPageGeneralMods.Location = new System.Drawing.Point(4, 29);
            this.tabPageGeneralMods.Name = "tabPageGeneralMods";
            this.tabPageGeneralMods.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneralMods.Size = new System.Drawing.Size(1283, 795);
            this.tabPageGeneralMods.TabIndex = 2;
            this.tabPageGeneralMods.Text = "General Mods";
            this.tabPageGeneralMods.UseVisualStyleBackColor = true;
            // 
            // tabPageMusic
            // 
            this.tabPageMusic.Location = new System.Drawing.Point(4, 29);
            this.tabPageMusic.Name = "tabPageMusic";
            this.tabPageMusic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMusic.Size = new System.Drawing.Size(1283, 795);
            this.tabPageMusic.TabIndex = 3;
            this.tabPageMusic.Text = "Music";
            this.tabPageMusic.UseVisualStyleBackColor = true;
            // 
            // tabPageCSS
            // 
            this.tabPageCSS.Location = new System.Drawing.Point(4, 29);
            this.tabPageCSS.Name = "tabPageCSS";
            this.tabPageCSS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCSS.Size = new System.Drawing.Size(1283, 795);
            this.tabPageCSS.TabIndex = 4;
            this.tabPageCSS.Text = "CSS";
            this.tabPageCSS.UseVisualStyleBackColor = true;
            // 
            // tabPageSSS
            // 
            this.tabPageSSS.Location = new System.Drawing.Point(4, 29);
            this.tabPageSSS.Name = "tabPageSSS";
            this.tabPageSSS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSSS.Size = new System.Drawing.Size(1283, 795);
            this.tabPageSSS.TabIndex = 5;
            this.tabPageSSS.Text = "SSS";
            this.tabPageSSS.UseVisualStyleBackColor = true;
            // 
            // tabPageExplorer
            // 
            this.tabPageExplorer.Location = new System.Drawing.Point(4, 29);
            this.tabPageExplorer.Name = "tabPageExplorer";
            this.tabPageExplorer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExplorer.Size = new System.Drawing.Size(1283, 795);
            this.tabPageExplorer.TabIndex = 6;
            this.tabPageExplorer.Text = "Explorer";
            this.tabPageExplorer.UseVisualStyleBackColor = true;
            // 
            // textConsole
            // 
            this.textConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textConsole.Location = new System.Drawing.Point(0, 0);
            this.textConsole.Multiline = true;
            this.textConsole.Name = "textConsole";
            this.textConsole.ReadOnly = true;
            this.textConsole.Size = new System.Drawing.Size(1291, 120);
            this.textConsole.TabIndex = 0;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select the game folder";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.FileName = "Project";
            this.saveFileDialog.Filter = "XML files|*.xml";
            this.saveFileDialog.Title = "Create Project File";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "XML files|*.xml";
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(255, 30);
            this.buildToolStripMenuItem.Text = "Build";
            this.buildToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(252, 6);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 985);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Kami Modpack Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem projectSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutKamiModpackBuilderToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCharacterMods;
        private System.Windows.Forms.TabPage tabPageStageMods;
        private System.Windows.Forms.TabPage tabPageGeneralMods;
        private System.Windows.Forms.TabPage tabPageMusic;
        private System.Windows.Forms.TabPage tabPageCSS;
        private System.Windows.Forms.TabPage tabPageSSS;
        private System.Windows.Forms.TextBox textConsole;
        private System.Windows.Forms.TabPage tabPageExplorer;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

