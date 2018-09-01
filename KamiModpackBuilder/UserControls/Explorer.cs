using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KamiModpackBuilder.Globals;
using KamiModpackBuilder.Objects;
using DamienG.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using KamiModpackBuilder.Properties;

namespace KamiModpackBuilder.UserControls
{
    public partial class Explorer : UserControl
    {

        private SmashProjectManager _ProjectManager;

        public Explorer()
        {
            _ProjectManager = SmashProjectManager.instance;

            InitializeComponent();

            treeView.ImageList = new ImageList();
            treeView.ImageList.Images.Add(UIConstants.ICON_FOLDER, Resources.icon_folder);
            treeView.ImageList.Images.Add(UIConstants.ICON_FILE, Resources.icon_file);
            treeView.ImageList.Images.Add(UIConstants.ICON_PACKED, Resources.icon_packed);

            RefreshTreeView();
        }

        #region TreeView
        public void RefreshTreeView()
        {
            treeView.Nodes.Clear();
            PopulateTreeView(_ProjectManager.ResourceDataCollection);
        }

        private void PopulateTreeView(ResourceCollection[] resourceData)
        {
            if (resourceData == null || resourceData.Length == 0)
            {
                LogHelper.Error("No data was found.");
                return;
            }

            treeView.BeginUpdate();
            LogHelper.Info("Populating Treeview...");
            treeView.Sort();
            treeView.Nodes.Clear();
            foreach (ResourceCollection resourceCollection in resourceData)
            {
                TreeNode rootNode = new TreeNode(resourceCollection.PartitionName, 0, 0);
                rootNode.Name = resourceCollection.PartitionName + "/";
                rootNode.Tag = resourceCollection;
                PopulateTreeViewResources(rootNode, resourceCollection.Nodes, resourceCollection.IsRegion);
                PopulateTreeViewWorkspace(rootNode);
                treeView.Nodes.Add(rootNode);
            }

            treeView.EndUpdate();
            LogHelper.Info("Done.");
        }

        private void PopulateTreeViewResources(TreeNode currentNode, List<ResourceItem> resourceCollection, bool isRegion)
        {
            foreach (ResourceItem resource in resourceCollection)
            {
                if (isRegion && resource.Source == FileSource.NotFound)
                    continue;
                string nodeName = resource.Filename;
                if (nodeName.EndsWith("/"))
                    nodeName = nodeName.Substring(0, nodeName.Length - 1);
                TreeNode subNode = new TreeNode(nodeName);
                subNode.Name = resource.AbsolutePath;
                //PopulateTreeViewResources(subNode, resource.Nodes, isRegion); //LAZY LOADING
                currentNode.Nodes.Add(subNode);
            }
        }

        private void PopulateTreeViewWorkspace(TreeNode currentNode)
        {
            TreeNode rootNode = GetFirstLevelNode(currentNode);
            char[] cachedpathseparator = "/".ToCharArray();
            string[] paths = _ProjectManager.GetAllWorkplaceRelativePaths(currentNode.Name, false);

            foreach (string path in paths)
            {
                if (null == currentNode.Nodes[currentNode.Name + path])
                {
                    string nodeName = path;
                    if (nodeName.EndsWith("/"))
                        nodeName = nodeName.Substring(0, nodeName.Length - 1);
                    currentNode.Nodes.Add(currentNode.Name + path, nodeName);
                }
            }
        }

        private void RefreshTreeNodeStyle(TreeNode node, bool recursive)
        {
            if (node == null)
                return;

            ResourceItem rItem = _ProjectManager.GetResource(node.Name);

            //See for subnodes
            if (rItem != null && node.Nodes.Count == 0 && rItem.Nodes.Count != 0)
                PopulateTreeViewResources(node, rItem.Nodes, rItem.ResourceCollection.IsRegion);
            PopulateTreeViewWorkspace(node);

            //Checking if file is in workspace (mod)
            string modPath = GetWorkspaceFileFromNode(node);
            if (File.Exists(modPath))
            {
                node.ForeColor = UIConstants.NODE_MOD;
                node.SelectedImageKey = UIConstants.ICON_FILE;
                node.ImageKey = UIConstants.ICON_FILE;
            }
            else if (Directory.Exists(modPath))
            {
                node.ForeColor = UIConstants.NODE_MOD;
                node.SelectedImageKey = UIConstants.ICON_FOLDER;
                node.ImageKey = UIConstants.ICON_FOLDER;
            }
            else if (rItem != null)
            {
                if (_ProjectManager.CurrentProject.IsResourceRemoved(rItem.ResourceCollection.PartitionName, rItem.RelativePath))
                    node.ForeColor = UIConstants.NODE_MOD_DELETED;
                else if (_ProjectManager.CurrentProject.IsUnlocalized(rItem.ResourceCollection.PartitionName, rItem.RelativePath))
                    node.ForeColor = UIConstants.NODE_MOD_UNLOCALIZED;
                else if (rItem.Source == FileSource.Patch)
                    node.ForeColor = UIConstants.NODE_PATCH;
                else if (rItem.Source == FileSource.LS || rItem.Source == FileSource.NotFound)
                    node.ForeColor = UIConstants.NODE_LS;
            }
            else
            {
                LogHelper.Warning(string.Format("The node '{0}' could not be found and has been removed.", node.Name));
                node.Remove();
            }
            if (rItem != null)
            {
                if (rItem.IsFolder)
                {
                    if (rItem.IsAPackage)
                    {
                        node.SelectedImageKey = UIConstants.ICON_PACKED;
                        node.ImageKey = UIConstants.ICON_PACKED;
                    }
                    else
                    {
                        node.SelectedImageKey = UIConstants.ICON_FOLDER;
                        node.ImageKey = UIConstants.ICON_FOLDER;
                    }
                }
                else
                {
                    node.SelectedImageKey = UIConstants.ICON_FILE;
                    node.ImageKey = UIConstants.ICON_FILE;
                }
            }

            if (recursive && node.IsExpanded)
                foreach (TreeNode childNode in node.Nodes)
                    RefreshTreeNodeStyle(childNode, recursive);
        }

        private void RemovePathFromTreeView(TreeNode node)
        {
            ResourceItem rItem = _ProjectManager.GetResource(node.Name);
            if (rItem != null)
            {
                for (int i = node.Nodes.Count - 1; i >= 0; i--)
                    RemovePathFromTreeView(node.Nodes[i]);
                return;
            }
            node.Remove();
        }

        private string GetWorkspaceFileFromNode(TreeNode node)
        {
            return PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH) + node.FullPath.Replace('/', Path.DirectorySeparatorChar);
        }

        private TreeNode GetFirstLevelNode(TreeNode node)
        {
            TreeNode rootNode = node;
            while (rootNode.Parent != null)
                rootNode = rootNode.Parent;

            return rootNode;
        }

        private void PopulateGridView(TreeNode node)
        {
            //ResourceCollection
            if (node.Parent == null && node.Tag is ResourceCollection)
            {
                ResourceCollection resCol = (ResourceCollection)node.Tag;
                dataGridView.Rows.Add("Name", resCol.PartitionName);
                dataGridView.Rows.Add("Path", node.Name);
                dataGridView.Rows.Add("Nbr Resources", resCol.Resources.Count);
                return;
            }

            ResourceItem rItem = null;

            //File
            string filePath = GetWorkspaceFileFromNode(node);
            if (File.Exists(filePath) || Directory.Exists(filePath))
            {
                dataGridView.Rows.Add("Name", Path.GetFileName(filePath));
                dataGridView.Rows.Add("Path", node.Name);
                dataGridView.Rows.Add("Source", "Mod");
            }
            else
            {
                //Original Resource
                rItem = _ProjectManager.GetResource(node.Name);
                if (rItem != null)
                {
                    dataGridView.Rows.Add("Name", rItem.Filename);
                    dataGridView.Rows.Add("Path", rItem.AbsolutePath);
                    dataGridView.Rows.Add("Compressed size", rItem.CmpSize);
                    dataGridView.Rows.Add("Decompressed size", rItem.DecSize);
                    dataGridView.Rows.Add("Flags", rItem.Flags);
                    dataGridView.Rows.Add("Package", rItem.IsAPackage ? "Yes" : "No");
                    string source = rItem.Source.ToString();
                    if (source == "NotFound")
                        source = "Folder";
                    dataGridView.Rows.Add("Source", source);
                    dataGridView.Rows.Add("Partition", rItem.ResourceCollection.PartitionName);
                    if (rItem.OffInPack != 0)
                        dataGridView.Rows.Add("Offset in pack", String.Format("0x{0:X8}", rItem.OffInPack));
                }
            }
        }
        #endregion

        #region Add Files
        private void AddOrReplaceFiles(TreeNode node, string[] files)
        {
            if (files == null || files.Length == 0)
                return;

            TreeNode rootNode = GetFirstLevelNode(node);
            if (!string.IsNullOrEmpty(node.Name))
            {
                foreach (string file in files)
                    _ProjectManager.AddFileToWorkspace(file, node.Name);


                //Update Treeview
                PopulateTreeViewWorkspace(node);

                //Update Style
                RefreshTreeNodeStyle(rootNode, true);
            }
        }
        #endregion

        #region treeView Events
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dataGridView.Rows.Clear();
            PopulateGridView(e.Node);
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            foreach (TreeNode childNode in e.Node.Nodes)
                RefreshTreeNodeStyle(childNode, false);
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node != null && node.Parent != null && node.SelectedImageKey != UIConstants.ICON_FOLDER && node.SelectedImageKey != UIConstants.ICON_PACKED)
            {
                string absolutePath = node.Name;
                if (string.IsNullOrEmpty(absolutePath))
                    return;

                //Extract
                string fullExtractedFile = _ProjectManager.ExtractResource(absolutePath);
                uint crcFile = Crc32.Compute(File.ReadAllBytes(fullExtractedFile));
                
                if (string.IsNullOrEmpty(_ProjectManager._Config.ProjectHexEditorFile))
                {
                    LogHelper.Info(UIStrings.INFO_FILE_HEX);
                    return;
                }
                Process process = Process.Start(_ProjectManager._Config.ProjectHexEditorFile, "\"" + fullExtractedFile + "\"");
                process.WaitForExit();

                //Check extract file, if changed, ask to add in workspace
                uint compareCrcFile = Crc32.Compute(File.ReadAllBytes(fullExtractedFile));
                if (crcFile != compareCrcFile)
                {
                    if (MessageBox.Show(string.Format(UIStrings.INFO_FILE_MODIFIED, absolutePath), UIStrings.CAPTION_FILE_MODIFIED, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        AddOrReplaceFiles(treeView.SelectedNode.Parent, new string[] { fullExtractedFile });
                }
            }
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            Point p = treeView.PointToClient(new Point(e.X, e.Y));
            treeView.SelectedNode = treeView.GetNodeAt(p.X, p.Y);
            AddOrReplaceFiles(treeView.SelectedNode, e.Data.GetData(DataFormats.FileDrop) as string[]);
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            Point p = treeView.PointToClient(new Point(e.X, e.Y));
            TreeNode node = treeView.GetNodeAt(p.X, p.Y);
            treeView.SelectedNode = node;
            if (node != null && node.Parent != null && node.SelectedImageKey != UIConstants.ICON_FILE)
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                treeView.SelectedNode = treeView.GetNodeAt(e.X, e.Y);
        }
        #endregion

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node != null)
                _ProjectManager.ExtractResource(node.Name);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node != null)
            {
                string path = node.Name;
                RemovePathFromTreeView(node);
                _ProjectManager.RemoveFileFromWorkspace(path);
                if (node.TreeView != null)
                    RefreshTreeNodeStyle(node, true);
            }
        }

        private void unlocalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node != null)
            {
                ResourceItem rootItem = _ProjectManager.GetResource(node.Name);
                if (rootItem != null)
                {
                    _ProjectManager.UnlocalizePath(rootItem.ResourceCollection, rootItem.RelativePath);
                    if (node.TreeView != null)
                        RefreshTreeNodeStyle(node, true);
                }
            }
        }

        private void removeUnlocalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node != null)
            {
                ResourceItem rootItem = _ProjectManager.GetResource(node.Name);
                if (rootItem != null)
                {
                    _ProjectManager.RemoveUnlocalized(rootItem.ResourceCollection, rootItem.RelativePath);
                    if (node.TreeView != null)
                        RefreshTreeNodeStyle(node, true);
                }
            }
        }

        private void removeResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node != null)
            {
                ResourceItem rootItem = _ProjectManager.GetResource(node.Name);
                if (rootItem != null)
                {
                    _ProjectManager.RemoveOriginalResource(rootItem.ResourceCollection, rootItem.RelativePath);
                    if (node.TreeView != null)
                        RefreshTreeNodeStyle(node, true);
                }
            }
        }

        private void reintroduceResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node != null)
            {
                ResourceItem rootItem = _ProjectManager.GetResource(node.Name);
                if (rootItem != null)
                {
                    _ProjectManager.ReintroduceOriginalResource(rootItem.ResourceCollection, rootItem.RelativePath);
                    if (node.TreeView != null)
                        RefreshTreeNodeStyle(node, true);
                }
            }
        }

        private void refreshTreeviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshTreeView();
        }
    }
}
