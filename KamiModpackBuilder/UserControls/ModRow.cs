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
using System.IO;
using ZLibNet;
using KamiModpackBuilder.Globals;
using KamiModpackBuilder.Properties;

namespace KamiModpackBuilder.UserControls
{
    public partial class ModRow : UserControl
    {
        #region Members
        private bool _IsActiveList = false;
        private DataGridModsList.ModListType _ModListType = DataGridModsList.ModListType.General;
        private SmashProjectManager _SmashProjectManager;
        private DB.Fighter _CurrentFighter;
        private bool m_IsSelected = false;

        private Color colorHighlight = Color.LightBlue;
        private Color colorNormal = Color.White;
        private bool isSelectable = true;
        #endregion

        #region Properties
        public int slotNum = 0;
        public string name = "";
        public int textureID = -1;
        public string warningText = "";
        public bool hasWarning = false;
        public string errorText = "";
        public bool hasError = false;
        public string modFolder = "";
        public bool propertiesEnabled = true;

        public bool isSelected { get { return m_IsSelected; } }
        public bool isActiveList { get { return _IsActiveList; } }
        public DataGridModsList.ModListType modListType { get { return _ModListType; } }
        #endregion

        #region Constructors
        public ModRow(SmashProjectManager a_smashProjectManager, bool a_isActiveList, DataGridModsList.ModListType a_modListType)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;
            _IsActiveList = a_isActiveList;
            _ModListType = a_modListType;
            switch (_ModListType) {
                case (DataGridModsList.ModListType.CharacterSlots):
                case (DataGridModsList.ModListType.CharacterGeneral):
                    EventManager.OnCharSlotModSelectionChanged += OnModSelectionChanged;
                    EventManager.OnCharGeneralModSelectionChanged += OnModSelectionChanged; break;
                case (DataGridModsList.ModListType.Stage):
                    EventManager.OnStageModSelectionChanged += OnModSelectionChanged; break;
                case (DataGridModsList.ModListType.General):
                    EventManager.OnMiscModSelectionChanged += OnModSelectionChanged; break;
            }
        }
        #endregion

        #region Destructor
        ~ModRow()
        {
            switch (_ModListType)
            {
                case (DataGridModsList.ModListType.CharacterSlots):
                case (DataGridModsList.ModListType.CharacterGeneral):
                    EventManager.OnCharSlotModSelectionChanged -= OnModSelectionChanged;
                    EventManager.OnCharGeneralModSelectionChanged -= OnModSelectionChanged; break;
                case (DataGridModsList.ModListType.Stage):
                    EventManager.OnStageModSelectionChanged -= OnModSelectionChanged; break;
                case (DataGridModsList.ModListType.General):
                    EventManager.OnMiscModSelectionChanged -= OnModSelectionChanged; break;
            }
        }
        #endregion

        #region Public Methods
        public void ChangeSelectedFighter(DB.Fighter a_fighter)
        {
            _CurrentFighter = a_fighter;
        }

        public void UpdateData(DataGridModsList.RowData rowData)
        {
            name = rowData.name;
            textureID = rowData.textureID;
            warningText = rowData.warningText;
            hasWarning = rowData.hasWarning;
            errorText = rowData.errorText;
            hasError = rowData.hasError;
            modFolder = rowData.modFolder;
            propertiesEnabled = rowData.propertiesEnabled;
            UpdateData();
        }

        public void UpdateData(DataGridSlotModList.SlotRowData rowData)
        {
            slotNum = rowData.slotNum;
            name = rowData.name;
            textureID = rowData.textureID;
            warningText = rowData.warningText;
            hasWarning = rowData.hasWarning;
            errorText = rowData.errorText;
            hasError = rowData.hasError;
            modFolder = rowData.modFolder;
            propertiesEnabled = rowData.propertiesEnabled;
            UpdateData();
        }

        public void UpdateData()
        {
            if (_IsActiveList && _ModListType == DataGridModsList.ModListType.CharacterSlots)
            {
                labelSlotNumber.Text = (slotNum+1).ToString();
            }
            else labelSlotNumber.Text = String.Empty;
            labelModName.Text = name;
            if (hasError)
            {
                buttonError.Visible = true;
                buttonError.Image = Resources.icon_error;
            }
            else if (hasWarning)
            {
                buttonError.Visible = true;
                buttonError.Image = Resources.icon_warning;
            }
            else
            {
                buttonError.Visible = false;
            }
            if (modFolder == String.Empty)
            {
                buttonProperties.Visible = false;
                isSelectable = false;
            }
            else
            {
                buttonProperties.Visible = true;
                if (!propertiesEnabled) buttonProperties.Enabled = false;
                isSelectable = true;
            }
        }
        #endregion

        #region Private Methods

        #endregion

        private void panelModList_Click(object sender, EventArgs e)
        {
            if (m_IsSelected) SetModAsDeselected();
            else SetModAsSelected();
        }

        private void labelModName_Click(object sender, EventArgs e)
        {
            panelModList_Click(null, null);
            return;
        }

        private void labelSlotNumber_Click(object sender, EventArgs e)
        {
            panelModList_Click(null, null);
            return;
        }

        private void SetModAsDeselected()
        {
            if (!isSelectable) return;
            switch (_ModListType)
            {
                case (DataGridModsList.ModListType.CharacterSlots):
                    EventManager.CharSlotModSelectionChanged(null); break;
                case (DataGridModsList.ModListType.CharacterGeneral):
                    EventManager.CharGeneralModSelectionChanged(null); break;
                case (DataGridModsList.ModListType.Stage):
                    EventManager.StageModSelectionChanged(null); break;
                case (DataGridModsList.ModListType.General):
                    EventManager.MiscModSelectionChanged(null); break;
            }
        }

        private void SetModAsSelected()
        {
            if (!isSelectable) return;
            switch (_ModListType)
            {
                case (DataGridModsList.ModListType.CharacterSlots):
                    EventManager.CharSlotModSelectionChanged(this); break;
                case (DataGridModsList.ModListType.CharacterGeneral):
                    EventManager.CharGeneralModSelectionChanged(this); break;
                case (DataGridModsList.ModListType.Stage):
                    EventManager.StageModSelectionChanged(this); break;
                case (DataGridModsList.ModListType.General):
                    EventManager.MiscModSelectionChanged(this); break;
            }
        }

        private void SelectMod()
        {
            if (!m_IsSelected)
            {
                colorNormal = panelModList.BackColor;
                panelModList.BackColor = colorHighlight;
                m_IsSelected = true;
            }
        }

        private void DeselectMod()
        {
            if (m_IsSelected)
            {
                panelModList.BackColor = colorNormal;
                m_IsSelected = false;
            }
        }

        #region Events
        private void OnModSelectionChanged(ModRow modRowSelected)
        {
            if (modRowSelected != this) DeselectMod();
            else SelectMod();
        }
        #endregion

        private void buttonProperties_Click(object sender, EventArgs e)
        {
            Forms.SlotModProperties popup;
            Forms.ModProperties popup2;
            switch (_ModListType)
            {
                case DataGridModsList.ModListType.CharacterSlots:
                    popup = new Forms.SlotModProperties(PathHelper.FolderCharSlotsMods + Path.DirectorySeparatorChar + _CurrentFighter.name + Path.DirectorySeparatorChar + modFolder, _CurrentFighter.name, _SmashProjectManager);
                    if (!popup.isInitialized)
                    {
                        MessageBox.Show("The mod properties could not be opened. Is the mod missing?");
                        return;
                    }
                    popup.ShowDialog();
                    break;
                case DataGridModsList.ModListType.CharacterGeneral:
                    popup2 = new Forms.ModProperties(PathHelper.FolderCharGeneralMods + Path.DirectorySeparatorChar + _CurrentFighter.name + Path.DirectorySeparatorChar + modFolder, _ModListType, _CurrentFighter.name);
                    if (!popup2.isInitialized)
                    {
                        MessageBox.Show("The mod properties could not be opened. Is the mod missing?");
                        return;
                    }
                    popup2.ShowDialog();
                    break;
                case DataGridModsList.ModListType.Stage:
                    popup2 = new Forms.ModProperties(PathHelper.FolderStageMods + Path.DirectorySeparatorChar + modFolder, _ModListType);
                    if (!popup2.isInitialized)
                    {
                        MessageBox.Show("The mod properties could not be opened. Is the mod missing?");
                        return;
                    }
                    popup2.ShowDialog();
                    break;
                case DataGridModsList.ModListType.General:
                    popup2 = new Forms.ModProperties(PathHelper.FolderGeneralMods + Path.DirectorySeparatorChar + modFolder, _ModListType);
                    if (!popup2.isInitialized)
                    {
                        MessageBox.Show("The mod properties could not be opened. Is the mod missing?");
                        return;
                    }
                    popup2.ShowDialog();
                    break;
            }
            switch (_ModListType)
            {
                case DataGridModsList.ModListType.CharacterSlots:
                    _SmashProjectManager._CharacterModsPage.RefreshSlotModsLists();
                    break;
                case DataGridModsList.ModListType.CharacterGeneral:
                    _SmashProjectManager._CharacterModsPage.RefreshGeneralModsLists();
                    break;
                case DataGridModsList.ModListType.Stage:
                    _SmashProjectManager._StageModsPage.RefreshModsLists();
                    break;
                case DataGridModsList.ModListType.General:
                    _SmashProjectManager._GeneralModsPage.RefreshModsLists();
                    break;
            }
        }
    }
}
