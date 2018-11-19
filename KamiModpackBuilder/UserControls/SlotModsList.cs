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
    public partial class SlotModsList : UserControl
    {

        #region Members
        private SmashProjectManager _SmashProjectManager;
        private SmashMod _Project;
        private DB.Fighter _CurrentFighter;
        private List<ModsList.RowData> _RowData = new List<ModsList.RowData>();
        private List<ModRow> _Rows = new List<ModRow>();
        #endregion

        #region Constructors
        public SlotModsList()
        {
            InitializeComponent();
            InitializeAudioComboBoxes();

            _SmashProjectManager = SmashProjectManager.instance;
        }
        #endregion

        #region Public Methods
        public void ChangeSelectedFighter(DB.Fighter a_fighter)
        {
            if (a_fighter == _CurrentFighter) return;
            _CurrentFighter = a_fighter;
            if (_Rows != null) for (int i = 0; i < _Rows.Count; ++i)
                {
                    _Rows[i].ChangeSelectedFighter(_CurrentFighter);
                }
            switch (_CurrentFighter.voicePackSlots)
            {
                case (DB.Fighter.VoicePackSlots.All): groupBoxVoiceMods.Visible = false; break;
                case (DB.Fighter.VoicePackSlots.One):
                    comboBoxVoiceSlot2.Visible = false;
                    groupBoxVoiceMods.Visible = true;
                    break;
                case (DB.Fighter.VoicePackSlots.Two):
                    comboBoxVoiceSlot2.Visible = true;
                    groupBoxVoiceMods.Visible = true;
                    break;
            }
            switch (_CurrentFighter.soundPackSlots)
            {
                case (DB.Fighter.SoundPackSlots.All): groupBoxSoundSlots.Visible = false; break;
                case (DB.Fighter.SoundPackSlots.One):
                    comboBoxSoundSlot2.Visible = false;
                    groupBoxSoundSlots.Visible = true;
                    break;
                case (DB.Fighter.SoundPackSlots.Two):
                    comboBoxSoundSlot2.Visible = true;
                    groupBoxSoundSlots.Visible = true;
                    break;
            }
            if (!groupBoxVoiceMods.Visible && !groupBoxSoundSlots.Visible) tableLayoutPanel4.SetRowSpan(panelModList, 2);
            else
            {
                CharacterAudioSlotSelection audio = _SmashProjectManager._CharacterModsPage.CurrentFighterAudioSlotSelections;
                if (audio != null) RefreshAudioComboBoxes(audio);

                tableLayoutPanel4.SetRowSpan(panelModList, 1);
                if (groupBoxVoiceMods.Visible)
                {
                    if (groupBoxSoundSlots.Visible)
                    {
                        tableLayoutPanel1.SetColumnSpan(groupBoxVoiceMods, 1);
                        tableLayoutPanel1.SetColumnSpan(groupBoxSoundSlots, 1);
                        tableLayoutPanel1.SetColumn(groupBoxSoundSlots, 1);
                    }
                    else
                    {
                        tableLayoutPanel1.SetColumnSpan(groupBoxVoiceMods, 2);
                    }
                }
                else
                {
                    tableLayoutPanel1.SetColumnSpan(groupBoxSoundSlots, 2);
                    tableLayoutPanel1.SetColumn(groupBoxSoundSlots, 1);
                }
            }
            RefreshRowData();
        }
        #endregion
        
        private void InitializeAudioComboBoxes()
        {
            string[] slots = {
                "Default", "Slot 1", "Slot 2", "Slot 3", "Slot 4",
                "Slot 5", "Slot 6", "Slot 7", "Slot 8",
                "Slot 9", "Slot 10", "Slot 11", "Slot 12",
                "Slot 13", "Slot 14", "Slot 15", "Slot 16",
                "Slot 17", "Slot 18", "Slot 19", "Slot 20",
                "Slot 21", "Slot 22", "Slot 23", "Slot 24",
                "Slot 25", "Slot 26", "Slot 27", "Slot 28",
                "Slot 29", "Slot 30", "Slot 31", "Slot 32"};
            comboBoxVoiceSlot1.DataSource = slots;
            comboBoxVoiceSlot2.DataSource = slots.Clone();
            comboBoxSoundSlot1.DataSource = slots.Clone();
            comboBoxSoundSlot2.DataSource = slots.Clone();
            comboBoxVoiceSlot1.SelectedIndex = 0;
            comboBoxVoiceSlot2.SelectedIndex = 0;
            comboBoxSoundSlot1.SelectedIndex = 0;
            comboBoxSoundSlot2.SelectedIndex = 0;
        }

        private void RefreshAudioComboBoxes(CharacterAudioSlotSelection audio)
        {
            comboBoxVoiceSlot1.SelectedIndex = audio.Voice1;
            comboBoxVoiceSlot2.SelectedIndex = audio.Voice2;
            comboBoxSoundSlot1.SelectedIndex = audio.Sound1;
            comboBoxSoundSlot2.SelectedIndex = audio.Sound2;
        }

        public void RefreshRowData()
        {
            _RowData = new List<ModsList.RowData>();
            _Project = _SmashProjectManager.CurrentProject;
            int maxSlots = _Project.EnableMoreCustomSlots ? _CurrentFighter.unrestrictedSlots : _CurrentFighter.maxSlots;

            for (int i = 0; i < maxSlots; ++i)
            {
                ModsList.RowData row = new ModsList.RowData();
                bool modFound = false;
                row.slotNum = i;
                for (int j = 0; j < _Project.ActiveCharacterSlotMods.Count; ++j)
                {
                    if (_Project.ActiveCharacterSlotMods[j].CharacterID != _CurrentFighter.id) continue;
                    if (_Project.ActiveCharacterSlotMods[j].SlotID != i) continue;
                    row.modFolder = _Project.ActiveCharacterSlotMods[j].FolderName;

                    CharacterSlotModXML data = Globals.Utils.OpenCharacterSlotKamiModFile(_CurrentFighter.name, row.modFolder);
                    if (data != null)
                    {
                        row.name = data.DisplayName;
                        row.missingPortraits = (!data.chr_00 || !data.chr_11 || !data.chr_13 || !data.stock_90);
                        if (data.UseCustomName && !row.missingPortraits)
                        {
                            if (!data.chrn_11 || data.BoxingRingText == null) row.missingPortraits = true;
                            else if (data.BoxingRingText.Equals(string.Empty)) row.missingPortraits = true;
                        }
                        row.metal = data.MetalModel;
                        row.hasAudio = data.Sound || data.Voice;
                        row.hasCustomName = data.UseCustomName;
                        row.wifiSafe = data.WifiSafe;
                    }
                    else
                    {
                        row.name = String.Format("{0} (Mod is missing!)", row.modFolder);
                        row.modMissing = true;
                        row.propertiesEnabled = false;
                    }
                    _RowData.Add(row);
                    modFound = true;
                    break;
                }
                if (modFound) continue;
                if (i < _CurrentFighter.defaultSlots)
                {
                    row.name = "Default";
                    _RowData.Add(row);
                }
                else break;
            }

            PopulateRows();
        }

        private void PopulateRows()
        {
            /*
            for (int i = _Rows.Count - 1; i >= 0; --i)
            {
                _Rows[i].Parent = null;
                _Rows[i].Dispose();
                _Rows.RemoveAt(i);
            }*/
            while (_Rows.Count < _RowData.Count)
            {
                if (_Rows.Count < _RowData.Count)
                {
                    ModRow row = new ModRow(_SmashProjectManager, true, ModsList.ModListType.CharacterSlots);
                    row.Dock = DockStyle.Top;
                    _Rows.Add(row);
                    row.Parent = panelModList;
                }
            }
            for (int i = _RowData.Count - 1; i > -1; --i)
            {
                ModRow myRow = _Rows[_Rows.Count - i - 1];
                myRow.ChangeSelectedFighter(_CurrentFighter);
                myRow.UpdateData(_RowData[i]);
                myRow.Visible = true;
            }
            if (_Rows.Count > _RowData.Count)
            {
                for (int i = 0; i < _Rows.Count - _RowData.Count; ++i)
                {
                    _Rows[i].Visible = false;
                }
            }
        }

        public void SelectMod(string modFolderName)
        {
            for (int i = 0; i < _Rows.Count; ++i) {
                if (_Rows[i].modFolder.Equals(modFolderName) && _Rows[i].Visible)
                {
                    Globals.EventManager.CharSlotModSelectionChanged(_Rows[i]);
                    return;
                }
            }
        }

        private void comboBoxVoiceSlot1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_SmashProjectManager == null) return;

            CharacterAudioSlotSelection audio = _SmashProjectManager._CharacterModsPage.CurrentFighterAudioSlotSelections;
            if (audio == null) return;
            audio.Voice1 = comboBoxVoiceSlot1.SelectedIndex;
        }

        private void comboBoxVoiceSlot2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_SmashProjectManager == null) return;

            CharacterAudioSlotSelection audio = _SmashProjectManager._CharacterModsPage.CurrentFighterAudioSlotSelections;
            if (audio == null) return;
            audio.Voice2 = comboBoxVoiceSlot2.SelectedIndex;
        }

        private void comboBoxSoundSlot1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_SmashProjectManager == null) return;

            CharacterAudioSlotSelection audio = _SmashProjectManager._CharacterModsPage.CurrentFighterAudioSlotSelections;
            if (audio == null) return;
            audio.Sound1 = comboBoxSoundSlot1.SelectedIndex;
        }

        private void comboBoxSoundSlot2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_SmashProjectManager == null) return;

            CharacterAudioSlotSelection audio = _SmashProjectManager._CharacterModsPage.CurrentFighterAudioSlotSelections;
            if (audio == null) return;
            audio.Sound2 = comboBoxSoundSlot2.SelectedIndex;
        }
    }
}
