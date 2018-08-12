using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KamiModpackBuilder.Objects;

namespace KamiModpackBuilder.UserControls
{
    public partial class CharacterMods : UserControl
    {

        private SmashProjectManager _SmashProjectManager;
        private DataGridSlotModList _GridSlots;
        private DataGridModsList _GridSlotsInactive;
        private DataGridModsList _GridGeneral;
        private DataGridModsList _GridGeneralInactive;

        private bool _IsInitialized = false;

        public CharacterMods(SmashProjectManager a_smashProjectManager)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;

            InitializeCharactersComboBox();
            CreateDataGrids();

            _IsInitialized = true;

            comboBoxCharacters_SelectedIndexChanged(this, null);
        }

        public void RefreshData()
        {
            InitializeCharactersComboBox();
            comboBoxCharacters_SelectedIndexChanged(this, null);
        }

        private void InitializeCharactersComboBox()
        {
            int fighterCount = DB.FightersDB.Fighters.Count;
            String[] chars = new String[fighterCount];

            for (int i = 0; i < fighterCount; ++i)
            {
                chars[i] = DB.FightersDB.Fighters[i].nameHuman;
            }

            comboBoxCharacters.DataSource = chars;
        }

        private void CreateDataGrids()
        {
            _GridSlots = new DataGridSlotModList(_SmashProjectManager);
            _GridSlots.Dock = DockStyle.Fill;

            _GridSlotsInactive = new DataGridModsList(_SmashProjectManager, false, DataGridModsList.ModListType.CharacterSlots);
            _GridSlotsInactive.Dock = DockStyle.Fill;

            _GridGeneral = new DataGridModsList(_SmashProjectManager, true, DataGridModsList.ModListType.CharacterGeneral);
            _GridGeneral.Dock = DockStyle.Fill;

            _GridGeneralInactive = new DataGridModsList(_SmashProjectManager, false, DataGridModsList.ModListType.CharacterGeneral);
            _GridGeneralInactive.Dock = DockStyle.Fill;

            tableLayoutPanel1.Controls.Add(_GridSlots, 0, 1);
            tableLayoutPanel1.Controls.Add(_GridSlotsInactive, 2, 1);
            tableLayoutPanel1.Controls.Add(_GridGeneral, 0, 3);
            tableLayoutPanel1.Controls.Add(_GridGeneralInactive, 2, 3);

        }

        private void comboBoxCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsInitialized) return;

            _GridSlotsInactive.ChangeSelectedFighter(DB.FightersDB.Fighters[comboBoxCharacters.SelectedIndex]);
            _GridGeneral.ChangeSelectedFighter(DB.FightersDB.Fighters[comboBoxCharacters.SelectedIndex]);
            _GridGeneralInactive.ChangeSelectedFighter(DB.FightersDB.Fighters[comboBoxCharacters.SelectedIndex]);
        }
    }
}
