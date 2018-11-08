using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KamiModpackBuilder.Objects;
using KamiModpackBuilder.Globals;
using System.IO;

namespace KamiModpackBuilder.Forms
{
    public partial class ProjectSettings : Form
    {

        private SmashMod _Project;

        public ProjectSettings(SmashMod project)
        {
            InitializeComponent();

            _Project = project;
            
            textBoxExtractionFolder.Text = _Project.ProjectExtractFolder;
            textBoxExplorerFolder.Text = _Project.ProjectExplorerFolder;
            textBoxExportFolder.Text = _Project.ProjectExportFolder;
            textBoxWorkspaceFolder.Text = _Project.ProjectWorkspaceFolder;
            
            checkBoxForceOriginalFlags.Checked = _Project.KeepOriginalFlags;
            checkBoxSkipJunkEntries.Checked = _Project.SkipJunkEntries;
            checkBoxExportCSV.Checked = _Project.ExportCSVList;
            checkBoxIgnoreCompSize.Checked = _Project.ExportCSVIgnoreCompSize;
            checkBoxIgnoreFlags.Checked = _Project.ExportCSVIgnoreFlags;
            checkBoxIgnorePackOffsets.Checked = _Project.ExportCSVIgnorePackOffsets;
            checkBoxAddDateToExport.Checked = _Project.ExportWithDateFolder;
            
            checkBoxTextureIDFixing.Checked = _Project.AutoTextureIDFix;
            checkBoxLxxDuplication.Checked = _Project.OverrideLowPolyModels;
            checkBoxRegionDuplication.Checked = _Project.DuplicateToOtherRegions;
            checkBoxYoshiFix.Checked = _Project.YoshiFixActive;
            checkBoxFighterDB.Checked = _Project.EditorCharacterMenuDBActive;
            checkBoxFighterStrings.Checked = _Project.EditorCharacterStringsActive;
            checkBoxSoundMTB.Checked = _Project.EditorMTBFix;

            checkBoxTextureIDConflicts.Checked = _Project.SupressWarningTextureIDConflicts;
            checkBoxMissingPortraits.Checked = _Project.SupressWarningMissingPortraits;
            checkBoxModFileConflicts.Checked = _Project.SupressWarningModFileConflicts;
            checkBoxModelFilesMissing.Checked = _Project.SupressWarningSlotModModelFilesMissing;

            checkBoxUseCSS.Checked = _Project.EditorCSSActive;
            checkBoxUseSSS.Checked = _Project.EditorSSSActive;
            checkBoxUseMusic.Checked = _Project.EditorMusicActive;
            checkBoxUseExplorer.Checked = _Project.EditorExplorerChanges;
        }

        private void buttonBrowseExtractionFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBoxExtractionFolder.Text;
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                textBoxExtractionFolder.Text = folderBrowserDialog.SelectedPath;
        }

        private void buttonBrowseExplorerFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBoxExplorerFolder.Text;
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                textBoxExplorerFolder.Text = folderBrowserDialog.SelectedPath;
        }

        private void buttonBrowseExportFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBoxExportFolder.Text;
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                textBoxExportFolder.Text = folderBrowserDialog.SelectedPath;
        }

        private void buttonBrowseWorkspaceFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBoxWorkspaceFolder.Text;
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                textBoxWorkspaceFolder.Text = folderBrowserDialog.SelectedPath;
        }

        private void ProjectSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_Project.ProjectExtractFolder.Equals(textBoxExtractionFolder.Text))
            {
                _Project.ProjectExtractFolderFullPath = string.Empty;
                _Project.ProjectExtractFolder = textBoxExtractionFolder.Text + (!textBoxExtractionFolder.Text.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                _Project.ProjectExtractFolder = _Project.ProjectExtractFolder.Replace(PathHelper.FolderProject, "");
            }
            if (!_Project.ProjectExplorerFolder.Equals(textBoxExtractionFolder.Text))
            {
                _Project.ProjectExplorerFolderFullPath = string.Empty;
                _Project.ProjectExplorerFolder = textBoxExplorerFolder.Text + (!textBoxExplorerFolder.Text.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                _Project.ProjectExplorerFolder = _Project.ProjectExplorerFolder.Replace(PathHelper.FolderProject, "");
            }
            if (!_Project.ProjectExportFolder.Equals(textBoxExtractionFolder.Text))
            {
                _Project.ProjectExportFolderFullPath = string.Empty;
                _Project.ProjectExportFolder = textBoxExportFolder.Text + (!textBoxExportFolder.Text.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                _Project.ProjectExportFolder = _Project.ProjectExportFolder.Replace(PathHelper.FolderProject, "");
            }
            if (!_Project.ProjectWorkspaceFolder.Equals(textBoxExtractionFolder.Text))
            {
                _Project.ProjectWorkspaceFolderFullPath = string.Empty;
                _Project.ProjectWorkspaceFolder = textBoxWorkspaceFolder.Text + (!textBoxWorkspaceFolder.Text.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                _Project.ProjectWorkspaceFolder = _Project.ProjectWorkspaceFolder.Replace(PathHelper.FolderProject, "");
            }

            _Project.KeepOriginalFlags = checkBoxForceOriginalFlags.Checked;
            _Project.SkipJunkEntries = checkBoxSkipJunkEntries.Checked;
            _Project.ExportCSVList = checkBoxExportCSV.Checked;
            _Project.ExportCSVIgnoreCompSize = checkBoxIgnoreCompSize.Checked;
            _Project.ExportCSVIgnoreFlags = checkBoxIgnoreFlags.Checked;
            _Project.ExportCSVIgnorePackOffsets = checkBoxIgnorePackOffsets.Checked;
            _Project.ExportWithDateFolder = checkBoxAddDateToExport.Checked;
            
            _Project.AutoTextureIDFix = checkBoxTextureIDFixing.Checked;
            _Project.OverrideLowPolyModels = checkBoxLxxDuplication.Checked;
            _Project.DuplicateToOtherRegions = checkBoxRegionDuplication.Checked;
            _Project.YoshiFixActive = checkBoxYoshiFix.Checked;
            _Project.EditorCharacterMenuDBActive = checkBoxFighterDB.Checked;
            _Project.EditorCharacterStringsActive = checkBoxFighterStrings.Checked;
            _Project.EditorMTBFix = checkBoxSoundMTB.Checked;

            _Project.SupressWarningTextureIDConflicts = checkBoxTextureIDConflicts.Checked;
            _Project.SupressWarningMissingPortraits = checkBoxMissingPortraits.Checked;
            _Project.SupressWarningModFileConflicts = checkBoxModFileConflicts.Checked;
            _Project.SupressWarningSlotModModelFilesMissing = checkBoxModelFilesMissing.Checked;

            _Project.EditorCSSActive = checkBoxUseCSS.Checked;
            _Project.EditorSSSActive = checkBoxUseSSS.Checked;
            _Project.EditorMusicActive = checkBoxUseMusic.Checked;
            _Project.EditorExplorerChanges = checkBoxUseExplorer.Checked;
        }
    }
}
