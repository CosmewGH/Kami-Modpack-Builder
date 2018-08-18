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
        private ModRow SelectedSlotMod = null;
        private ModRow SelectedGeneralMod = null;
        private DB.Fighter _CurrentFighter = null;

        private List<CharacterSlotMod> CurrentFighterActiveSlotMods = new List<CharacterSlotMod>();
        private List<CharacterGeneralMod> CurrentFighterActiveGeneralMods = new List<CharacterGeneralMod>();

        public DB.Fighter CurrentFighter { get { return _CurrentFighter; } }

        #region Constructors
        public CharacterMods(SmashProjectManager a_smashProjectManager)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;

            InitializeCharactersComboBox();
            CreateDataGrids();

            _IsInitialized = true;

            comboBoxCharacters_SelectedIndexChanged(this, null);
            Globals.EventManager.OnCharSlotModSelectionChanged += CharSlotModSelectionChanged;
            Globals.EventManager.OnCharGeneralModSelectionChanged += CharGeneralModSelectionChanged;
        }
        #endregion

        #region Destructor
        ~CharacterMods()
        {
            Globals.EventManager.OnCharSlotModSelectionChanged -= CharSlotModSelectionChanged;
            Globals.EventManager.OnCharGeneralModSelectionChanged -= CharGeneralModSelectionChanged;
        }
        #endregion

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

        private void GetCurrentCharacterActiveMods()
        {
            CurrentFighterActiveSlotMods.Clear();
            CurrentFighterActiveGeneralMods.Clear();
            for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveCharacterSlotMods.Count; ++i) {
                if (_SmashProjectManager.CurrentProject.ActiveCharacterSlotMods[i].CharacterID == _CurrentFighter.id) CurrentFighterActiveSlotMods.Add(_SmashProjectManager.CurrentProject.ActiveCharacterSlotMods[i]);
            }
            for (int i = 0; i < _SmashProjectManager.CurrentProject.ActiveCharacterGeneralMods.Count; ++i)
            {
                if (_SmashProjectManager.CurrentProject.ActiveCharacterGeneralMods[i].CharacterID == _CurrentFighter.id) CurrentFighterActiveGeneralMods.Add(_SmashProjectManager.CurrentProject.ActiveCharacterGeneralMods[i]);
            }
        }

        private int GetAvailableActiveSlot()
        {
            //First, return the 1st additional added slot available if possible.
            int highestModSlot = GetHighestSlotModSlot();
            if (highestModSlot < _CurrentFighter.defaultSlots - 1)
            {
                if (_CurrentFighter.defaultSlots < _CurrentFighter.maxSlots) return _CurrentFighter.defaultSlots;
            }
            if (highestModSlot < _CurrentFighter.maxSlots - 1) return highestModSlot + 1;
            //If no additional slots are available, find the highest slot number which still has a default skin.
            int highestDefaultSlotAvailable = _CurrentFighter.defaultSlots - 1;
            while (highestDefaultSlotAvailable > -1)
            {
                for (int i = 0; i < CurrentFighterActiveSlotMods.Count; ++i)
                {
                    if (CurrentFighterActiveSlotMods[i].SlotID == highestDefaultSlotAvailable)
                    {
                        --highestDefaultSlotAvailable;
                        break;
                    }
                }
                return highestDefaultSlotAvailable;
            }
            //If no default slots are available, return -1 as no spots are available.
            return -1;
        }

        private int GetHighestSlotModSlot()
        {
            int highestModSlot = -1;
            for (int i = 0; i < CurrentFighterActiveSlotMods.Count; ++i)
            {
                if (CurrentFighterActiveSlotMods[i].SlotID > highestModSlot) highestModSlot = CurrentFighterActiveSlotMods[i].SlotID;
            }
            return highestModSlot;
        }

        private CharacterSlotMod GetActiveModAtSlot(int slot)
        {
            for (int i = 0; i < CurrentFighterActiveSlotMods.Count; ++i)
            {
                if (CurrentFighterActiveSlotMods[i].SlotID == slot) return CurrentFighterActiveSlotMods[i];
            }
            return null;
        }

        public void RefreshSlotModsLists()
        {
            _GridSlots.RefreshRowData();
            _GridSlotsInactive.RefreshRowData();
            Globals.EventManager.CharSlotModSelectionChanged(null);
        }

        public void RefreshGeneralModsLists()
        {
            _GridGeneral.RefreshRowData();
            _GridGeneralInactive.RefreshRowData();
            Globals.EventManager.CharGeneralModSelectionChanged(null);
        }

        #region Events
        private void comboBoxCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsInitialized) return;

            _CurrentFighter = DB.FightersDB.Fighters[comboBoxCharacters.SelectedIndex];
            _GridSlots.ChangeSelectedFighter(_CurrentFighter);
            _GridSlotsInactive.ChangeSelectedFighter(_CurrentFighter);
            _GridGeneral.ChangeSelectedFighter(_CurrentFighter);
            _GridGeneralInactive.ChangeSelectedFighter(_CurrentFighter);
            GetCurrentCharacterActiveMods();
            Globals.EventManager.CharSlotModSelectionChanged(null);
        }

        private void CharSlotModSelectionChanged(ModRow selectedMod)
        {
            SelectedSlotMod = selectedMod;
            if (SelectedSlotMod == null)
            {
                buttonSlotUp.Enabled = false;
                buttonSlotDown.Enabled = false;
                buttonSlotLeft.Enabled = false;
                buttonSlotRight.Enabled = false;
                buttonSlotBottom.Enabled = false;
            }
            else
            {
                if (!SelectedSlotMod.isActiveList)
                {
                    buttonSlotUp.Enabled = false;
                    buttonSlotDown.Enabled = false;
                    buttonSlotRight.Enabled = false;
                    if (GetAvailableActiveSlot() > -1) buttonSlotLeft.Enabled = true;
                    else buttonSlotLeft.Enabled = false;
                    buttonSlotBottom.Enabled = false;
                }
                else
                {
                    buttonSlotRight.Enabled = true;
                    buttonSlotLeft.Enabled = false;
                    if (SelectedSlotMod.slotNum <= 0) buttonSlotUp.Enabled = false;
                    else buttonSlotUp.Enabled = true;
                    if (SelectedSlotMod.slotNum >= _CurrentFighter.maxSlots - 1 || (SelectedSlotMod.slotNum >= GetHighestSlotModSlot() && SelectedSlotMod.slotNum > _CurrentFighter.defaultSlots - 1)) buttonSlotDown.Enabled = false;
                    else buttonSlotDown.Enabled = true;
                    if ((_CurrentFighter.defaultSlots == _CurrentFighter.maxSlots) || (SelectedSlotMod.slotNum >= _CurrentFighter.defaultSlots) || (GetHighestSlotModSlot() >= _CurrentFighter.maxSlots - 1))
                        buttonSlotBottom.Enabled = false;
                    else buttonSlotBottom.Enabled = true;
                }
            }
        }

        private void CharGeneralModSelectionChanged(ModRow selectedMod)
        {
            SelectedGeneralMod = selectedMod;
            if (SelectedGeneralMod == null)
            {
                buttonGeneralUp.Enabled = false;
                buttonGeneralDown.Enabled = false;
                buttonGeneralLeft.Enabled = false;
                buttonGeneralRight.Enabled = false;
            }
            else
            {
                if (!SelectedGeneralMod.isActiveList)
                {
                    buttonGeneralUp.Enabled = false;
                    buttonGeneralDown.Enabled = false;
                    buttonGeneralRight.Enabled = false;
                    buttonGeneralLeft.Enabled = true;
                }
                else
                {
                    buttonGeneralRight.Enabled = true;
                    buttonGeneralLeft.Enabled = false;
                    buttonGeneralUp.Enabled = true;//TODO: Add check to see if it is the top of the list already
                    buttonGeneralDown.Enabled = true;//TODO: Add check to see if it is the bottom of the list already
                }
            }
        }

        private void buttonSlotLeft_Click(object sender, EventArgs e)
        {
            if (SelectedSlotMod == null) return;
            if (SelectedSlotMod.isActiveList) return;

            //Get the slot to place the mod in
            int newSlot = GetAvailableActiveSlot();
            if (newSlot == -1) return;

            CharacterSlotMod newActiveMod = new CharacterSlotMod();
            newActiveMod.CharacterID = _CurrentFighter.id;
            newActiveMod.SlotID = newSlot;
            newActiveMod.FolderName = SelectedSlotMod.modFolder;
            _SmashProjectManager.CurrentProject.ActiveCharacterSlotMods.Add(newActiveMod);
            CurrentFighterActiveSlotMods.Add(newActiveMod);
            RefreshSlotModsLists();
            _GridSlots.SelectMod(newActiveMod.FolderName);
        }

        private void buttonSlotRight_Click(object sender, EventArgs e)
        {
            if (SelectedSlotMod == null) return;
            if (!SelectedSlotMod.isActiveList) return;
            string modFolder = SelectedSlotMod.modFolder;
            int slot = SelectedSlotMod.slotNum;
            for (int i = 0; i < CurrentFighterActiveSlotMods.Count; ++i)
            {
                if (CurrentFighterActiveSlotMods[i].FolderName.Equals(modFolder))
                {
                    _SmashProjectManager.CurrentProject.ActiveCharacterSlotMods.Remove(CurrentFighterActiveSlotMods[i]);
                    CurrentFighterActiveSlotMods.RemoveAt(i);
                    break;
                }
            }
            if (slot > _CurrentFighter.defaultSlots - 1)
            {
                for (int i = 0; i < CurrentFighterActiveSlotMods.Count; ++i)
                {
                    if (CurrentFighterActiveSlotMods[i].SlotID > slot) --CurrentFighterActiveSlotMods[i].SlotID;
                }
            }
            RefreshSlotModsLists();
            _GridSlotsInactive.SelectMod(modFolder);
        }

        private void buttonSlotUp_Click(object sender, EventArgs e)
        {
            if (SelectedSlotMod == null) return;
            if (!SelectedSlotMod.isActiveList) return;
            string modFolder = SelectedSlotMod.modFolder;
            int slot = SelectedSlotMod.slotNum;
            if (slot < 1) return;
            CharacterSlotMod originalSlot = GetActiveModAtSlot(slot);
            CharacterSlotMod newSlot = GetActiveModAtSlot(slot - 1);
            --originalSlot.SlotID;
            if (newSlot != null) ++newSlot.SlotID;
            else if (slot > _CurrentFighter.defaultSlots - 1)
            {
                for (int i = 0; i < CurrentFighterActiveSlotMods.Count; ++i)
                {
                    if (CurrentFighterActiveSlotMods[i].SlotID > slot) --CurrentFighterActiveSlotMods[i].SlotID;
                }
            }

            RefreshSlotModsLists();
            _GridSlots.SelectMod(modFolder);
        }

        private void buttonSlotDown_Click(object sender, EventArgs e)
        {
            if (SelectedSlotMod == null) return;
            if (!SelectedSlotMod.isActiveList) return;
            string modFolder = SelectedSlotMod.modFolder;
            int slot = SelectedSlotMod.slotNum;
            if (slot >= _CurrentFighter.maxSlots - 1) return;
            if (slot >= GetHighestSlotModSlot() && slot > _CurrentFighter.defaultSlots - 1) return;

            CharacterSlotMod originalSlot = GetActiveModAtSlot(slot);
            CharacterSlotMod newSlot = GetActiveModAtSlot(slot + 1);
            ++originalSlot.SlotID;
            if (newSlot != null) --newSlot.SlotID;

            RefreshSlotModsLists();
            _GridSlots.SelectMod(modFolder);
        }

        private void buttonSlotBottom_Click(object sender, EventArgs e)
        {
            if (SelectedSlotMod == null) return;
            if (!SelectedSlotMod.isActiveList) return;
            if (_CurrentFighter.defaultSlots == _CurrentFighter.maxSlots) return;
            int slot = SelectedSlotMod.slotNum;
            if (slot >= _CurrentFighter.defaultSlots) return;
            int newslot = GetHighestSlotModSlot();
            if (newslot >= _CurrentFighter.maxSlots - 1) return;
            if (newslot <= _CurrentFighter.defaultSlots) newslot = _CurrentFighter.defaultSlots;
            else ++newslot;
            CharacterSlotMod originalSlot = GetActiveModAtSlot(slot);
            originalSlot.SlotID = newslot;

            RefreshSlotModsLists();
            _GridSlots.SelectMod(originalSlot.FolderName);
        }

        private void buttonGeneralLeft_Click(object sender, EventArgs e)
        {

        }

        private void buttonGeneralRight_Click(object sender, EventArgs e)
        {

        }

        private void buttonGeneralUp_Click(object sender, EventArgs e)
        {

        }

        private void buttonGeneralDown_Click(object sender, EventArgs e)
        {

        }

        private void buttonImportSlotMod_Click(object sender, EventArgs e)
        {

        }

        private void buttonImportGeneralMod_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
