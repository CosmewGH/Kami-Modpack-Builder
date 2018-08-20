using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KamiModpackBuilder.UserControls
{
    public partial class StageMods : UserControl
    {
        private SmashProjectManager _SmashProjectManager;
        private DataGridModsList _GridMods;
        private DataGridModsList _GridModsInactive;

        private bool _IsInitialized = false;
        private ModRow SelectedMod = null;

        public StageMods(SmashProjectManager a_smashProjectManager)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;
            CreateDataGrids();
        }

        private void CreateDataGrids()
        {
            _GridMods = new DataGridModsList(_SmashProjectManager, true, DataGridModsList.ModListType.Stage);
            _GridMods.Dock = DockStyle.Fill;

            _GridModsInactive = new DataGridModsList(_SmashProjectManager, false, DataGridModsList.ModListType.Stage);
            _GridModsInactive.Dock = DockStyle.Fill;

            tableLayoutPanel1.Controls.Add(_GridMods, 0, 1);
            tableLayoutPanel1.Controls.Add(_GridModsInactive, 2, 1);
        }
    }
}
