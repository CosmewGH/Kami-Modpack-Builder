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
        private List<SlotRowData> _RowData;
        #endregion

        #region Constructors
        public DataGridSlotModList(SmashProjectManager a_smashProjectManager)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;
        }
        #endregion

        public void ChangeCurrentFighter(DB.Fighter a_fighter)
        {
            if (a_fighter == _CurrentFighter) return;
            _CurrentFighter = a_fighter;
            RefreshRowData();
        }

        private void RefreshRowData()
        {
            //TODO: Get rid of datagrid stuff, will use a virtical list of usercontrol rows instead

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
                        row.warningText = "test";
                        row.hasWarning = true;
                    }
                }
            }
        }

        private class SlotRowData
        {
            public int slotNum = 0;
            public string name = "";
            public string warningText = "";
            public bool hasWarning = false;
            public string errorText = "";
            public bool hasError = false;
            public string modFolder = "";
        }
    }
}
