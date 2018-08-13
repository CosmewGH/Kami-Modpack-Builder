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
    public partial class DataGridSlotModList : UserControl
    {

        #region Members
        private SmashProjectManager _SmashProjectManager;
        private SmashMod _Project;
        private DB.Fighter _CurrentFighter;
        private List<SlotRowData> _RowData = new List<SlotRowData>();
        private List<ModRow> _Rows = new List<ModRow>();
        #endregion

        #region Constructors
        public DataGridSlotModList(SmashProjectManager a_smashProjectManager)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;
        }
        #endregion

        public void ChangeSelectedFighter(DB.Fighter a_fighter)
        {
            if (a_fighter == _CurrentFighter) return;
            _CurrentFighter = a_fighter;
            if (_Rows != null) for (int i = 0; i < _Rows.Count; ++i)
                {
                    _Rows[i].ChangeSelectedFighter(_CurrentFighter);
                }
            RefreshRowData();
        }

        private void RefreshRowData()
        {
            _RowData = new List<SlotRowData>();
            _Project = _SmashProjectManager.CurrentProject;

            for (int i = 0; i < _CurrentFighter.maxSlots; ++i)
            {
                SlotRowData row = new SlotRowData();
                row.slotNum = i;
                for (int j = 0; j < _Project.ActiveCharacterSlotMods.Count; ++j)
                {
                    if (_Project.ActiveCharacterSlotMods[j].SlotID == i)
                    {
                        row.modFolder = _Project.ActiveCharacterSlotMods[j].FolderName;

                        CharacterSlotModXML data = Globals.Utils.OpenCharacterSlotKamiModFile(_CurrentFighter.name, row.modFolder);
                        row.name = data.DisplayName;
                        _RowData.Add(row);
                        break;
                    }
                }
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
            for (int i = 0; i < _Rows.Count; ++i)
            {
                _Rows[i].Parent = null;
            }
            _Rows = new List<ModRow>();
            for (int i = _RowData.Count - 1; i > -1; --i)
            {
                ModRow row = new ModRow(_SmashProjectManager, true, DataGridModsList.ModListType.CharacterSlots);
                row.ChangeSelectedFighter(_CurrentFighter);
                row.UpdateData(_RowData[i]);
                row.Dock = DockStyle.Top;
                if (_RowData[i].modFolder == String.Empty) row.isSelectable = false;
                _Rows.Add(row);
                row.Parent = panelModList;
            }
        }

        public class SlotRowData
        {
            public int slotNum = 0;
            public string name = String.Empty;
            public int textureID = -1;
            public string warningText = String.Empty;
            public bool hasWarning = false;
            public string errorText = String.Empty;
            public bool hasError = false;
            public string modFolder = String.Empty;
        }
    }
}
