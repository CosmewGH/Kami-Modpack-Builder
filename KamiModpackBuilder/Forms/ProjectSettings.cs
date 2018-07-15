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

namespace KamiModpackBuilder.Forms
{
    public partial class ProjectSettings : Form
    {

        private SmashMod _Project;

        public ProjectSettings(SmashMod project)
        {
            InitializeComponent();

            _Project = project;

            textBoxTempFolder.Text = _Project.ProjectTempFolder;
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

            checkBoxTextureIDChecking.Checked = _Project.AutoTextureIDCheck;
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

        private void buttonBrowseTempFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBoxTempFolder.Text;
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                textBoxTempFolder.Text = folderBrowserDialog.SelectedPath;
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
            _Project.ProjectTempFolder = textBoxTempFolder.Text;
            _Project.ProjectExtractFolder = textBoxExtractionFolder.Text;
            _Project.ProjectExplorerFolder = textBoxExplorerFolder.Text;
            _Project.ProjectExportFolder = textBoxExportFolder.Text;
            _Project.ProjectWorkspaceFolder = textBoxWorkspaceFolder.Text;

            _Project.KeepOriginalFlags = checkBoxForceOriginalFlags.Checked;
            _Project.SkipJunkEntries = checkBoxSkipJunkEntries.Checked;
            _Project.ExportCSVList = checkBoxExportCSV.Checked;
            _Project.ExportCSVIgnoreCompSize = checkBoxIgnoreCompSize.Checked;
            _Project.ExportCSVIgnoreFlags = checkBoxIgnoreFlags.Checked;
            _Project.ExportCSVIgnorePackOffsets = checkBoxIgnorePackOffsets.Checked;
            _Project.ExportWithDateFolder = checkBoxAddDateToExport.Checked;

            _Project.AutoTextureIDCheck = checkBoxTextureIDChecking.Checked;
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
