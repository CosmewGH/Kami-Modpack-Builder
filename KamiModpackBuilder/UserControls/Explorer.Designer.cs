namespace KamiModpackBuilder.UserControls
{
    partial class Explorer
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
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.unlocalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeUnlocalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.removeResourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reintroduceResourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.gridViewName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridViewValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refreshTreeviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuTreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.ContextMenuStrip = this.contextMenuTreeView;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(946, 802);
            this.treeView.TabIndex = 0;
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // contextMenuTreeView
            // 
            this.contextMenuTreeView.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractToolStripMenuItem,
            this.removeModToolStripMenuItem,
            this.toolStripSeparator1,
            this.unlocalizeToolStripMenuItem,
            this.removeUnlocalizeToolStripMenuItem,
            this.toolStripSeparator2,
            this.removeResourceToolStripMenuItem,
            this.reintroduceResourceToolStripMenuItem,
            this.toolStripSeparator3,
            this.refreshTreeviewToolStripMenuItem});
            this.contextMenuTreeView.Name = "contextMenuTreeView";
            this.contextMenuTreeView.Size = new System.Drawing.Size(251, 232);
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.extractToolStripMenuItem.Text = "Extract";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // removeModToolStripMenuItem
            // 
            this.removeModToolStripMenuItem.Name = "removeModToolStripMenuItem";
            this.removeModToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.removeModToolStripMenuItem.Text = "Remove mod files";
            this.removeModToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(247, 6);
            // 
            // unlocalizeToolStripMenuItem
            // 
            this.unlocalizeToolStripMenuItem.Name = "unlocalizeToolStripMenuItem";
            this.unlocalizeToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.unlocalizeToolStripMenuItem.Text = "Unlocalize";
            this.unlocalizeToolStripMenuItem.Click += new System.EventHandler(this.unlocalizeToolStripMenuItem_Click);
            // 
            // removeUnlocalizeToolStripMenuItem
            // 
            this.removeUnlocalizeToolStripMenuItem.Name = "removeUnlocalizeToolStripMenuItem";
            this.removeUnlocalizeToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.removeUnlocalizeToolStripMenuItem.Text = "Remove unlocalize";
            this.removeUnlocalizeToolStripMenuItem.Click += new System.EventHandler(this.removeUnlocalizeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(247, 6);
            // 
            // removeResourceToolStripMenuItem
            // 
            this.removeResourceToolStripMenuItem.Name = "removeResourceToolStripMenuItem";
            this.removeResourceToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.removeResourceToolStripMenuItem.Text = "Remove resource";
            this.removeResourceToolStripMenuItem.Click += new System.EventHandler(this.removeResourceToolStripMenuItem_Click);
            // 
            // reintroduceResourceToolStripMenuItem
            // 
            this.reintroduceResourceToolStripMenuItem.Name = "reintroduceResourceToolStripMenuItem";
            this.reintroduceResourceToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.reintroduceResourceToolStripMenuItem.Text = "Reintroduce resource";
            this.reintroduceResourceToolStripMenuItem.Click += new System.EventHandler(this.reintroduceResourceToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridViewName,
            this.gridViewValue});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 28;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.Size = new System.Drawing.Size(237, 802);
            this.dataGridView.TabIndex = 0;
            // 
            // gridViewName
            // 
            this.gridViewName.HeaderText = "Name";
            this.gridViewName.Name = "gridViewName";
            this.gridViewName.ReadOnly = true;
            // 
            // gridViewValue
            // 
            this.gridViewValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gridViewValue.HeaderText = "Value";
            this.gridViewValue.Name = "gridViewValue";
            this.gridViewValue.ReadOnly = true;
            // 
            // refreshTreeviewToolStripMenuItem
            // 
            this.refreshTreeviewToolStripMenuItem.Name = "refreshTreeviewToolStripMenuItem";
            this.refreshTreeviewToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.refreshTreeviewToolStripMenuItem.Text = "Refresh Treeview";
            this.refreshTreeviewToolStripMenuItem.Click += new System.EventHandler(this.refreshTreeviewToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(247, 6);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeView);
            this.splitContainer1.Size = new System.Drawing.Size(1187, 802);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.TabIndex = 1;
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Explorer";
            this.Size = new System.Drawing.Size(1187, 802);
            this.contextMenuTreeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridViewName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridViewValue;
        private System.Windows.Forms.ContextMenuStrip contextMenuTreeView;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeModToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem unlocalizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeUnlocalizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem removeResourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reintroduceResourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem refreshTreeviewToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
