using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KamiModpackBuilder.Objects;

namespace KamiModpackBuilder.UserControls
{
    public partial class GeneralMods : UserControl
    {
        private SmashProjectManager _SmashProjectManager;
        private DataGridModsList _GridMods;
        private DataGridModsList _GridModsInactive;

        private bool _IsInitialized = false;
        private ModRow SelectedMod = null;

        public GeneralMods(SmashProjectManager a_smashProjectManager)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;
            CreateDataGrids();

            _IsInitialized = true;
            Globals.EventManager.OnMiscModSelectionChanged += MiscModSelectionChanged;
            RefreshModsLists();
        }

        ~GeneralMods()
        {
            Globals.EventManager.OnMiscModSelectionChanged -= MiscModSelectionChanged;
        }

        private void CreateDataGrids()
        {
            _GridMods = new DataGridModsList(_SmashProjectManager, true, DataGridModsList.ModListType.General);
            _GridMods.Dock = DockStyle.Fill;

            _GridModsInactive = new DataGridModsList(_SmashProjectManager, false, DataGridModsList.ModListType.General);
            _GridModsInactive.Dock = DockStyle.Fill;

            tableLayoutPanel1.Controls.Add(_GridMods, 0, 1);
            tableLayoutPanel1.Controls.Add(_GridModsInactive, 2, 1);
        }

        public void RefreshModsLists()
        {
            _GridMods.RefreshRowData();
            _GridModsInactive.RefreshRowData();
            Globals.EventManager.MiscModSelectionChanged(null);
        }

        private void MiscModSelectionChanged(ModRow selectedMod)
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
                    for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveGeneralMods.Count; ++i)
                    {
                        if (!SelectedMod.modFolder.Equals(_SmashProjectManager.CurrentProject.ActiveGeneralMods[i])) continue;
                        buttonUp.Enabled = (i > 0);
                        buttonDown.Enabled = (i < _SmashProjectManager.CurrentProject.ActiveGeneralMods.Count - 1);
                        break;
                    }
                }
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (SelectedMod == null) return;
            if (SelectedMod.isActiveList) return;

            string newActiveMod = SelectedMod.modFolder;
            _SmashProjectManager.CurrentProject.ActiveGeneralMods.Add(newActiveMod);
            RefreshModsLists();
            _GridMods.SelectMod(newActiveMod);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (SelectedMod == null) return;
            if (!SelectedMod.isActiveList) return;
            string modFolder = SelectedMod.modFolder;
            for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveGeneralMods.Count; ++i)
            {
                if (_SmashProjectManager.CurrentProject.ActiveGeneralMods[i].Equals(modFolder))
                {
                    _SmashProjectManager.CurrentProject.ActiveGeneralMods.RemoveAt(i);
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
            for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveGeneralMods.Count; ++i)
            {
                if (!_SmashProjectManager.CurrentProject.ActiveGeneralMods[i].Equals(modFolder)) continue;
                string mod = _SmashProjectManager.CurrentProject.ActiveGeneralMods[i];
                int index = _SmashProjectManager.CurrentProject.ActiveGeneralMods.IndexOf(mod);
                _SmashProjectManager.CurrentProject.ActiveGeneralMods.RemoveAt(index);
                _SmashProjectManager.CurrentProject.ActiveGeneralMods.Insert(index - 1, mod);
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
            for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveGeneralMods.Count; ++i)
            {
                if (!_SmashProjectManager.CurrentProject.ActiveGeneralMods[i].Equals(modFolder)) continue;
                string mod = _SmashProjectManager.CurrentProject.ActiveGeneralMods[i];
                int index = _SmashProjectManager.CurrentProject.ActiveGeneralMods.IndexOf(mod);
                _SmashProjectManager.CurrentProject.ActiveGeneralMods.RemoveAt(index);
                _SmashProjectManager.CurrentProject.ActiveGeneralMods.Insert(index + 1, mod);
                break;
            }
            RefreshModsLists();
            _GridMods.SelectMod(modFolder);
        }

        private void buttonImportMod_Click(object sender, EventArgs e)
        {
            Forms.ImportFolderOrZip popup = new Forms.ImportFolderOrZip();
            popup.textInstuctions = "Folder must have a 'data', 'data(us_en)', 'data(us_fr)' and/or 'data(us_sp)'\r\nfolder for the root of the mod files (Based on the Sm4shExplorer hierarchy\r\nin the fighter folder) so Kami Modpack Builder knows where to place\r\nthe files.";
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
    }
}
