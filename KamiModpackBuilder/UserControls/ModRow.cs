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
        private ModsList.ModListType _ModListType = ModsList.ModListType.General;
        private SmashProjectManager _SmashProjectManager;
        private DB.Fighter _CurrentFighter;
        private bool m_IsSelected = false;

        private Color colorNormal = Color.White;
        private Color colorNotSelectable = Color.FromArgb(255, 240, 240, 240);
        private Color colorMouseHover = Color.FromArgb(255,220,235,255);
        private Color colorHighlight = Color.LightSkyBlue;
        private bool isSelectable = true;

        private int HoverValue = 0;
        #endregion

        #region Properties
        public int slotNum = 0;
        public string name = "";
        public int textureID = -1;
        public string modFolder = "";
        public bool propertiesEnabled = true;
        public CharacterSlotModXML.MetalModelStatus metal = CharacterSlotModXML.MetalModelStatus.Works;
        public bool missingModel = false;
        public bool missingPortraits = false;
        public bool hasAudio = false;
        public bool hasCustomName = false;
        public bool wifiSafe = true;
        public bool modMissing = false;

        public bool isSelected { get { return m_IsSelected; } }
        public bool isActiveList { get { return _IsActiveList; } }
        public ModsList.ModListType modListType { get { return _ModListType; } }
        public ModsList modsListParent = null;
        #endregion

        #region Constructors
        public ModRow(SmashProjectManager a_smashProjectManager, bool a_isActiveList, ModsList.ModListType a_modListType)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;
            _IsActiveList = a_isActiveList;
            _ModListType = a_modListType;
            switch (_ModListType) {
                case (ModsList.ModListType.CharacterSlots):
                case (ModsList.ModListType.CharacterGeneral):
                    EventManager.OnCharSlotModSelectionChanged += OnModSelectionChanged;
                    EventManager.OnCharGeneralModSelectionChanged += OnModSelectionChanged; break;
                case (ModsList.ModListType.Stage):
                    EventManager.OnStageModSelectionChanged += OnModSelectionChanged; break;
                case (ModsList.ModListType.General):
                    EventManager.OnMiscModSelectionChanged += OnModSelectionChanged; break;
            }
        }
        #endregion

        #region Destructor
        ~ModRow()
        {
            switch (_ModListType)
            {
                case (ModsList.ModListType.CharacterSlots):
                case (ModsList.ModListType.CharacterGeneral):
                    EventManager.OnCharSlotModSelectionChanged -= OnModSelectionChanged;
                    EventManager.OnCharGeneralModSelectionChanged -= OnModSelectionChanged; break;
                case (ModsList.ModListType.Stage):
                    EventManager.OnStageModSelectionChanged -= OnModSelectionChanged; break;
                case (ModsList.ModListType.General):
                    EventManager.OnMiscModSelectionChanged -= OnModSelectionChanged; break;
            }
        }
        #endregion

        #region Public Methods
        public void ChangeSelectedFighter(DB.Fighter a_fighter)
        {
            _CurrentFighter = a_fighter;
        }

        public void UpdateData(ModsList.RowData rowData)
        {
            slotNum = rowData.slotNum;
            name = rowData.name;
            textureID = rowData.textureID;
            modFolder = rowData.modFolder;
            propertiesEnabled = rowData.propertiesEnabled;
            metal = rowData.metal;
            missingModel = rowData.missingModel;
            missingPortraits = rowData.missingPortraits;
            hasAudio = rowData.hasAudio;
            hasCustomName = rowData.hasCustomName;
            wifiSafe = rowData.wifiSafe;
            modMissing = rowData.modMissing;
            UpdateData();
        }

        public void UpdateData()
        {
            if (_IsActiveList && _ModListType == ModsList.ModListType.CharacterSlots)
            {
                labelModName.Text = (slotNum+1).ToString("D2") + "    " + name;
            }
            else labelModName.Text = name;
            HoverValue = 0;
            if (modMissing)
            {
                labelError.Visible = true;
                labelMetal.Visible = false;
                labelWifi.Visible = false;
                labelModel.Visible = false;
                labelPortraits.Visible = false;
                labelAudio.Visible = false;
                labelCustomName.Visible = false;
            }
            else
            {
                switch (metal)
                {
                    case CharacterSlotModXML.MetalModelStatus.Unknown:
                        labelMetal.Image = Resources.icon_metal_unknown;
                        labelMetal.Visible = true;
                        break;
                    case CharacterSlotModXML.MetalModelStatus.Missing:
                        labelMetal.Image = Resources.icon_metal_missing;
                        labelMetal.Visible = true;
                        break;
                    case CharacterSlotModXML.MetalModelStatus.Crashes:
                        labelMetal.Image = Resources.icon_metal_crashes;
                        labelMetal.Visible = true;
                        break;
                    case CharacterSlotModXML.MetalModelStatus.Works:
                        labelMetal.Visible = false;
                        break;
                }
                labelError.Visible = false;
                labelWifi.Visible = !wifiSafe;
                labelModel.Visible = missingModel;
                labelPortraits.Visible = missingPortraits;
                labelAudio.Visible = hasAudio;
                labelCustomName.Visible = hasCustomName;
            }
            if (modFolder.Equals(String.Empty))
            {
                isSelectable = false;
                panelModList.BackColor = colorNotSelectable;
            }
            else
            {
                isSelectable = true;
                panelModList.BackColor = colorNormal;
            }
        }

        public void OpenProperties()
        {
            if (modFolder == String.Empty || !propertiesEnabled) return;
            Forms.SlotModProperties popup;
            Forms.ModProperties popup2;
            switch (_ModListType)
            {
                case ModsList.ModListType.CharacterSlots:
                    popup = new Forms.SlotModProperties(PathHelper.FolderCharSlotsMods + _CurrentFighter.name + Path.DirectorySeparatorChar + modFolder, _CurrentFighter.name, _SmashProjectManager);
                    if (!popup.isInitialized)
                    {
                        MessageBox.Show("The mod properties could not be opened. Is the mod missing?");
                        return;
                    }
                    popup.ShowDialog();
                    break;
                case ModsList.ModListType.CharacterGeneral:
                    popup2 = new Forms.ModProperties(PathHelper.FolderCharGeneralMods + Path.DirectorySeparatorChar + _CurrentFighter.name + Path.DirectorySeparatorChar + modFolder, _ModListType, _CurrentFighter.name);
                    if (!popup2.isInitialized)
                    {
                        MessageBox.Show("The mod properties could not be opened. Is the mod missing?");
                        return;
                    }
                    popup2.ShowDialog();
                    break;
                case ModsList.ModListType.Stage:
                    popup2 = new Forms.ModProperties(PathHelper.FolderStageMods + Path.DirectorySeparatorChar + modFolder, _ModListType);
                    if (!popup2.isInitialized)
                    {
                        MessageBox.Show("The mod properties could not be opened. Is the mod missing?");
                        return;
                    }
                    popup2.ShowDialog();
                    break;
                case ModsList.ModListType.General:
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
                case ModsList.ModListType.CharacterSlots:
                    _SmashProjectManager._CharacterModsPage.RefreshSlotModsLists();

                    _SmashProjectManager._CharacterModsPage.SelectSlotMod(modFolder);
                    break;
                case ModsList.ModListType.CharacterGeneral:
                    _SmashProjectManager._CharacterModsPage.RefreshGeneralModsLists();
                    _SmashProjectManager._CharacterModsPage.SelectGeneralMod(modFolder);
                    break;
                case ModsList.ModListType.Stage:
                    _SmashProjectManager._StageModsPage.RefreshModsLists();
                    _SmashProjectManager._StageModsPage.SelectMod(modFolder);
                    break;
                case ModsList.ModListType.General:
                    _SmashProjectManager._GeneralModsPage.RefreshModsLists();
                    _SmashProjectManager._GeneralModsPage.SelectMod(modFolder);
                    break;
            }
        }
        #endregion

        #region Private Methods

        private void SetModAsDeselected()
        {
            if (!isSelectable) return;
            switch (_ModListType)
            {
                case (ModsList.ModListType.CharacterSlots):
                    EventManager.CharSlotModSelectionChanged(null); break;
                case (ModsList.ModListType.CharacterGeneral):
                    EventManager.CharGeneralModSelectionChanged(null); break;
                case (ModsList.ModListType.Stage):
                    EventManager.StageModSelectionChanged(null); break;
                case (ModsList.ModListType.General):
                    EventManager.MiscModSelectionChanged(null); break;
            }
        }

        private void SetModAsSelected()
        {
            if (!isSelectable) return;
            switch (_ModListType)
            {
                case (ModsList.ModListType.CharacterSlots):
                    EventManager.CharSlotModSelectionChanged(this); break;
                case (ModsList.ModListType.CharacterGeneral):
                    EventManager.CharGeneralModSelectionChanged(this); break;
                case (ModsList.ModListType.Stage):
                    EventManager.StageModSelectionChanged(this); break;
                case (ModsList.ModListType.General):
                    EventManager.MiscModSelectionChanged(this); break;
            }
        }

        private void SelectMod()
        {
            if (!m_IsSelected)
            {
                panelModList.BackColor = colorHighlight;
                m_IsSelected = true;
            }
        }

        private void DeselectMod()
        {
            if (m_IsSelected)
            {
                if (isSelectable) panelModList.BackColor = colorNormal;
                else panelModList.BackColor = colorNotSelectable;
                m_IsSelected = false;
            }
        }
        #endregion

        #region Events
        private void panelModList_Click(object sender, EventArgs e)
        {
            if (m_IsSelected) SetModAsDeselected();
            else SetModAsSelected();
            HoverValue = 0;
        }

        private void label_Click(object sender, EventArgs e)
        {
            panelModList_Click(null, null);
            return;
        }

        private void OnModSelectionChanged(ModRow modRowSelected)
        {
            if (modRowSelected != this) DeselectMod();
            else SelectMod();
        }

        private void panelModList_DoubleClick(object sender, EventArgs e)
        {
            OpenProperties();
        }

        private void label_DoubleClick(object sender, EventArgs e)
        {
            panelModList_DoubleClick(null, null);
            return;
        }

        private void panelModList_MouseEnter(object sender, EventArgs e)
        {
            if (!isSelectable) return;
            if (m_IsSelected) return;
            ++HoverValue;
            if (HoverValue > 2) HoverValue = 2;
            panelModList.BackColor = colorMouseHover;
        }

        private void panelModList_MouseLeave(object sender, EventArgs e)
        {
            if (!isSelectable) return;
            if (m_IsSelected) return;
            --HoverValue;
            if (HoverValue <= 0)
            {
                HoverValue = 0;
                panelModList.BackColor = colorNormal;
            }
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            if (!isSelectable) return;
            if (m_IsSelected) return;
            ++HoverValue;
            if (HoverValue > 2) HoverValue = 2;
            panelModList.BackColor = colorMouseHover;
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            if (!isSelectable) return;
            if (m_IsSelected) return;
            --HoverValue;
            if (HoverValue <= 0)
            {
                HoverValue = 0;
                panelModList.BackColor = colorNormal;
            }
        }

        private void ModRow_DragOver(object sender, DragEventArgs e)
        {
            if (!_IsActiveList)
            {
                e.Effect = DragDropEffects.Link;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void ModRow_DragDrop(object sender, DragEventArgs e)
        {
            if (_IsActiveList) return;
            modsListParent.DoDragDrop((e.Data.GetData(DataFormats.FileDrop)) as string[]);
        }
        #endregion
    }
}
