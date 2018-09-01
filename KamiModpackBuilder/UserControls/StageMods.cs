using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KamiModpackBuilder.Objects;
using System.IO;

namespace KamiModpackBuilder.UserControls
{
    public partial class StageMods : UserControl
    {
        private SmashProjectManager _SmashProjectManager;
        private ModsList _GridMods;
        private ModsList _GridModsInactive;

        private bool _IsInitialized = false;
        private ModRow SelectedMod = null;

        public StageMods()
        {
            InitializeComponent();

            _SmashProjectManager = SmashProjectManager.instance;
            CreateDataGrids();

            _IsInitialized = true;
            Globals.EventManager.OnStageModSelectionChanged += StageModSelectionChanged;
            RefreshModsLists();
        }

        ~StageMods()
        {
            Globals.EventManager.OnStageModSelectionChanged -= StageModSelectionChanged;
        }

        private void CreateDataGrids()
        {
            _GridMods = new ModsList(true, ModsList.ModListType.Stage);
            _GridMods.Dock = DockStyle.Fill;

            _GridModsInactive = new ModsList(false, ModsList.ModListType.Stage);
            _GridModsInactive.Dock = DockStyle.Fill;

            tableLayoutPanel1.Controls.Add(_GridMods, 0, 1);
            tableLayoutPanel1.Controls.Add(_GridModsInactive, 2, 1);
        }

        public void RefreshModsLists()
        {
            _GridMods.RefreshRowData();
            _GridModsInactive.RefreshRowData();
            Globals.EventManager.StageModSelectionChanged(null);
        }

        private void StageModSelectionChanged(ModRow selectedMod)
        {
            SelectedMod = selectedMod;
            if (SelectedMod == null)
            {
                buttonUp.Enabled = false;
                buttonDown.Enabled = false;
                buttonLeft.Enabled = false;
                buttonRight.Enabled = false;
            }
            else
            {
                if (!SelectedMod.isActiveList)
                {
                    buttonUp.Enabled = false;
                    buttonDown.Enabled = false;
                    buttonRight.Enabled = false;
                    buttonLeft.Enabled = true;
                }
                else
                {
                    buttonRight.Enabled = true;
                    buttonLeft.Enabled = false;
                    for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveStageMods.Count; ++i)
                    {
                        if (!SelectedMod.modFolder.Equals(_SmashProjectManager.CurrentProject.ActiveStageMods[i].FolderName)) continue;
                        buttonUp.Enabled = (i > 0);
                        buttonDown.Enabled = (i < _SmashProjectManager.CurrentProject.ActiveStageMods.Count - 1);
                        break;
                    }
                }
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (SelectedMod == null) return;
            if (SelectedMod.isActiveList) return;

            StageModXML xml = Globals.Utils.OpenStageKamiModFile(SelectedMod.modFolder);
            if (xml == null) return;
            int id = xml.IntendedStage;

            for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveStageMods.Count; ++i)
            {
                if (id == _SmashProjectManager.CurrentProject.ActiveStageMods[i].StageID)
                {
                    MessageBox.Show(String.Format("Cannot activate mod. Stage mod '{0}' is already occupying that stage slot.", _SmashProjectManager.CurrentProject.ActiveStageMods[i].FolderName));
                    return;
                }
            }

            StageSlotMod newActiveMod = new StageSlotMod();
            newActiveMod.FolderName = SelectedMod.modFolder;
            newActiveMod.StageID = id;
            _SmashProjectManager.CurrentProject.ActiveStageMods.Add(newActiveMod);
            RefreshModsLists();
            _GridMods.SelectMod(newActiveMod.FolderName);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (SelectedMod == null) return;
            if (!SelectedMod.isActiveList) return;
            string modFolder = SelectedMod.modFolder;
            for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveStageMods.Count; ++i)
            {
                if (_SmashProjectManager.CurrentProject.ActiveStageMods[i].FolderName.Equals(modFolder))
                {
                    _SmashProjectManager.CurrentProject.ActiveStageMods.RemoveAt(i);
                    break;
                }
            }
            RefreshModsLists();
            _GridModsInactive.SelectMod(modFolder);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (SelectedMod == null) return;
            if (!SelectedMod.isActiveList) return;
            string modFolder = SelectedMod.modFolder;
            for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveStageMods.Count; ++i)
            {
                if (!_SmashProjectManager.CurrentProject.ActiveStageMods[i].FolderName.Equals(modFolder)) continue;
                StageSlotMod mod = _SmashProjectManager.CurrentProject.ActiveStageMods[i];
                int index = _SmashProjectManager.CurrentProject.ActiveStageMods.IndexOf(mod);
                _SmashProjectManager.CurrentProject.ActiveStageMods.RemoveAt(index);
                _SmashProjectManager.CurrentProject.ActiveStageMods.Insert(index - 1, mod);
                break;
            }
            RefreshModsLists();
            _GridMods.SelectMod(modFolder);
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (SelectedMod == null) return;
            if (!SelectedMod.isActiveList) return;
            string modFolder = SelectedMod.modFolder;
            for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveStageMods.Count; ++i)
            {
                if (!_SmashProjectManager.CurrentProject.ActiveStageMods[i].FolderName.Equals(modFolder)) continue;
                StageSlotMod mod = _SmashProjectManager.CurrentProject.ActiveStageMods[i];
                int index = _SmashProjectManager.CurrentProject.ActiveStageMods.IndexOf(mod);
                _SmashProjectManager.CurrentProject.ActiveStageMods.RemoveAt(index);
                _SmashProjectManager.CurrentProject.ActiveStageMods.Insert(index + 1, mod);
                break;
            }
            RefreshModsLists();
            _GridMods.SelectMod(modFolder);
        }

        private void buttonImportMod_Click(object sender, EventArgs e)
        {
            Forms.ImportFolderOrZip popup = new Forms.ImportFolderOrZip();
            popup.textInstuctions = "Folder must have a 'melee' or 'end' folder with a stage folder inside that. (Based\r\non the Sm4shExplorer hierarchy in the stage folder) so Kami Modpack\r\nBuilder knows where to place the files. Can also have UI files.";
            popup.ShowDialog();
            String path = String.Empty;
            if (popup.choseZip)
            {
                if (openFileDialogImportZip.ShowDialog() == DialogResult.OK)
                {
                    _GridModsInactive.BeginImport(openFileDialogImportZip.FileName);
                }
            }
            else if (popup.choseFolder)
            {
                FolderSelectDialog ofd = new FolderSelectDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _GridModsInactive.BeginImport(ofd.SelectedPath);
                }
            }
        }

        private void buttonDeleteMod_Click(object sender, EventArgs e)
        {
            if (SelectedMod == null) return;
            if (MessageBox.Show(string.Format("Are you sure you want to delete the mod '{0}'?", SelectedMod.name), "Delete Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            if (SelectedMod.isActiveList)
            {
                string modFolder = SelectedMod.modFolder;
                for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveStageMods.Count; ++i)
                {
                    if (_SmashProjectManager.CurrentProject.ActiveStageMods[i].Equals(modFolder))
                    {
                        _SmashProjectManager.CurrentProject.ActiveStageMods.RemoveAt(i);
                        break;
                    }
                }
            }
            string path = Globals.PathHelper.GetStageModPath(SelectedMod.modFolder);
            if (Directory.Exists(path)) Directory.Delete(path, true);
            RefreshModsLists();
        }
    }
}
