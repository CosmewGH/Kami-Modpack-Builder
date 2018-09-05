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

namespace KamiModpackBuilder.UserControls
{
    public partial class ModsList : UserControl
    {
        #region Members
        private bool _IsActiveList = false;
        private ModListType _ModListType = ModListType.General;
        private SmashProjectManager _SmashProjectManager;
        private SmashMod _Project;
        private DB.Fighter _CurrentFighter;
        private List<RowData> _RowData = new List<RowData>();
        private List<ModRow> _Rows = new List<ModRow>();
        private string[] DragDropData = null;
        #endregion

        #region Enums
        public enum ModListType { CharacterSlots, CharacterGeneral, Stage, General }
        #endregion

        #region Constructors
        public ModsList(bool a_isActiveList, ModListType a_modListType)
        {
            InitializeComponent();

            _SmashProjectManager = SmashProjectManager.instance;
            _IsActiveList = a_isActiveList;
            _ModListType = a_modListType;
        }
        #endregion

        #region Public Methods
        public void ChangeSelectedFighter(DB.Fighter a_fighter)
        {
            if (_ModListType == ModListType.General || _ModListType == ModListType.Stage) return;
            _CurrentFighter = a_fighter;
            if (_Rows != null) for (int i = 0; i < _Rows.Count;  ++i)
                {
                    _Rows[i].ChangeSelectedFighter(_CurrentFighter);
                }
            RefreshRowData();
        }

        public void BeginImport(string path)
        {
            ParseImportFiles(new string[] { path });
        }

        public void SelectMod(string modFolderName)
        {
            for (int i = 0; i < _Rows.Count; ++i)
            {
                if (_Rows[i].modFolder.Equals(modFolderName) && _Rows[i].Visible)
                {
                    switch (_ModListType)
                    {
                        case (ModsList.ModListType.CharacterSlots):
                            EventManager.CharSlotModSelectionChanged(_Rows[i]); return;
                        case (ModsList.ModListType.CharacterGeneral):
                            EventManager.CharGeneralModSelectionChanged(_Rows[i]); return;
                        case (ModsList.ModListType.Stage):
                            EventManager.StageModSelectionChanged(_Rows[i]); return;
                        case (ModsList.ModListType.General):
                            EventManager.MiscModSelectionChanged(_Rows[i]); return;
                    }
                }
            }
        }
        #endregion

        #region Private Methods

        public void RefreshRowData()
        {
            _RowData = new List<RowData>();
            _Project = _SmashProjectManager.CurrentProject;

            if (_IsActiveList)
            {
                if (_ModListType == ModListType.CharacterGeneral) {
                    for (int j = 0; j < _Project.ActiveCharacterGeneralMods.Count; ++j)
                    {
                        if (_Project.ActiveCharacterGeneralMods[j].CharacterID != _CurrentFighter.id) continue;

                        RowData row = new RowData();
                        row.modFolder = _Project.ActiveCharacterGeneralMods[j].FolderName;

                        CharacterGeneralModXML data = Globals.Utils.OpenCharacterGeneralKamiModFile(_CurrentFighter.name, row.modFolder);
                        if (data != null)
                        {
                            row.name = data.DisplayName;
                            row.wifiSafe = data.WifiSafe;
                        }
                        else
                        {
                            row.name = String.Format("{0} (Mod is missing!)", row.modFolder);
                            row.modMissing = true;
                            row.propertiesEnabled = false;
                        }
                        _RowData.Add(row);
                    }
                }
                else if (_ModListType == ModListType.Stage)
                {
                    for (int j = 0; j < _Project.ActiveStageMods.Count; ++j)
                    {
                        RowData row = new RowData();
                        row.modFolder = _Project.ActiveStageMods[j].FolderName;

                        StageModXML data = Globals.Utils.OpenStageKamiModFile(row.modFolder);
                        if (data != null)
                        {
                            row.name = data.DisplayName;
                            row.wifiSafe = data.WifiSafe;
                        }
                        else
                        {
                            row.name = String.Format("{0} (Mod is missing!)", row.modFolder);
                            row.modMissing = true;
                            row.propertiesEnabled = false;
                        }
                        _RowData.Add(row);
                    }
                }
                else if (_ModListType == ModListType.General)
                {
                    for (int j = 0; j < _Project.ActiveGeneralMods.Count; ++j)
                    {
                        RowData row = new RowData();
                        row.modFolder = _Project.ActiveGeneralMods[j];

                        GeneralModXML data = Globals.Utils.OpenGeneralKamiModFile(row.modFolder);
                        if (data != null)
                        {
                            row.name = data.DisplayName;
                            row.wifiSafe = data.WifiSafe;
                        }
                        else
                        {
                            row.name = String.Format("{0} (Mod is missing!)", row.modFolder);
                            row.modMissing = true;
                            row.propertiesEnabled = false;
                        }
                        _RowData.Add(row);
                    }
                }
            }
            else
            {
                string baseDirectory = String.Empty;
                switch (_ModListType)
                {
                    case (ModListType.CharacterSlots):
                        baseDirectory = PathHelper.FolderCharSlotsMods + _CurrentFighter.name + Path.DirectorySeparatorChar;
                        break;
                    case (ModListType.CharacterGeneral):
                        baseDirectory = PathHelper.FolderCharGeneralMods + _CurrentFighter.name + Path.DirectorySeparatorChar;
                        break;
                    case (ModListType.Stage):
                        baseDirectory = PathHelper.FolderStageMods;
                        break;
                    case (ModListType.General):
                        baseDirectory = PathHelper.FolderGeneralMods;
                        break;
                }
                string[] kamiFiles = Directory.GetFiles(baseDirectory, "kamimod.xml", SearchOption.AllDirectories);

                for (int i = 0; i < kamiFiles.Count(); ++i)
                {
                    string modFolderName = kamiFiles[i].Replace(baseDirectory, String.Empty).Split(Path.DirectorySeparatorChar).First();
                    //Check if the mod is already active. If it is, don't include it in this list.
                    bool modIsActive = false;
                    switch (_ModListType)
                    {
                        case (ModListType.CharacterSlots):
                            for (int j = 0; j < _Project.ActiveCharacterSlotMods.Count; ++j)
                            {
                                if (_Project.ActiveCharacterSlotMods[j].CharacterID == _CurrentFighter.id)
                                {
                                    if (_Project.ActiveCharacterSlotMods[j].FolderName.Equals(modFolderName))
                                    {
                                        modIsActive = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        case (ModListType.CharacterGeneral):
                            for (int j = 0; j < _Project.ActiveCharacterGeneralMods.Count; ++j)
                            {
                                if (_Project.ActiveCharacterGeneralMods[j].FolderName.Equals(modFolderName))
                                {
                                    modIsActive = true;
                                    break;
                                }
                            }
                            break;
                        case (ModListType.Stage):
                            for (int j = 0; j < _Project.ActiveStageMods.Count; ++j)
                            {
                                if (_Project.ActiveStageMods[j].FolderName.Equals(modFolderName))
                                {
                                    modIsActive = true;
                                    break;
                                }
                            }
                            break;
                        case (ModListType.General):
                            for (int j = 0; j < _Project.ActiveGeneralMods.Count; ++j)
                            {
                                if (_Project.ActiveGeneralMods[j].Equals(modFolderName))
                                {
                                    modIsActive = true;
                                    break;
                                }
                            }
                            break;
                    }
                    if (modIsActive) continue;

                    RowData row = new RowData();
                    row.modFolder = modFolderName;
                    switch (_ModListType)
                    {
                        case (ModListType.CharacterSlots):
                            CharacterSlotModXML data = Utils.DeserializeXML<CharacterSlotModXML>(kamiFiles[i]);
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
                            break;
                        case (ModListType.CharacterGeneral):
                            CharacterGeneralModXML data2 = Utils.DeserializeXML<CharacterGeneralModXML>(kamiFiles[i]);
                            row.name = data2.DisplayName;
                            row.wifiSafe = data2.WifiSafe;
                            break;
                        case (ModListType.Stage):
                            StageModXML data3 = Utils.DeserializeXML<StageModXML>(kamiFiles[i]);
                            row.name = data3.DisplayName;
                            row.wifiSafe = data3.WifiSafe;
                            break;
                        case (ModListType.General):
                            GeneralModXML data4 = Utils.DeserializeXML<GeneralModXML>(kamiFiles[i]);
                            row.name = data4.DisplayName;
                            row.wifiSafe = data4.WifiSafe;
                            break;
                    }
                    _RowData.Add(row);
                }
            }

            PopulateRows();
        }

        private void PopulateRows()
        {
            while (_Rows.Count < _RowData.Count)
            {
                if (_Rows.Count < _RowData.Count)
                {
                    ModRow row = new ModRow(_SmashProjectManager, _IsActiveList, _ModListType);
                    row.Dock = DockStyle.Top;
                    _Rows.Add(row);
                    row.Parent = panelModList;
                }
            }
            for (int i = _RowData.Count - 1; i > -1; --i)
            {
                ModRow myRow = _Rows[_Rows.Count - i - 1];
                if (_ModListType == ModListType.CharacterGeneral || _ModListType == ModListType.CharacterSlots)
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

        #region Events
        private void DataGridModsList_DragDrop(object sender, DragEventArgs e)
        {
            if (_IsActiveList) return;
            DragDropData = (e.Data.GetData(DataFormats.FileDrop)) as string[];
            backgroundWorkerDragDrop.RunWorkerAsync();
        }

        private void DataGridModsList_DragOver(object sender, DragEventArgs e)
        {
            if (!_IsActiveList)
            {
                e.Effect = DragDropEffects.Link;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void backgroundWorkerDragDrop_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = true;
        }

        private void backgroundWorkerDragDrop_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ParseImportFiles(DragDropData);
        }
        #endregion

        #region Mod Import Processing
        private void ParseImportFiles(string[] files, bool isZip = true)
        {
            if (files.Length > 1)
            {
                MessageBox.Show("Only drag one zip file or folder!");
                return;
            }

            string fileExt = Path.GetExtension(files[0]);
            if (fileExt.Equals(".zip") || fileExt.Equals(".7z") || fileExt.Equals(".rar"))
            {
                if (!isZip) return;
                CleanUnzipFolder();
                UnZipper unZipper = new UnZipper();
                unZipper.ZipFile = files[0];
                unZipper.ItemList.Add("*.*");
                unZipper.Destination = PathHelper.FolderTemp + "unzip" + Path.DirectorySeparatorChar;
                unZipper.Recurse = true;
                unZipper.UnZip();
                LogHelper.Debug("Unzipping file...");
                ParseImportFiles(new string[1] { PathHelper.FolderTemp + "unzip" + Path.DirectorySeparatorChar });
                return;
            }
            else
            {
                string baseDirectory = files[0] + (!files[0].EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);

                files = ValidateFiles(files);
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        switch (_ModListType)
                        {
                            case (ModListType.CharacterSlots):
                                ProcessCharacterSlotModFiles(files, baseDirectory);
                                break;
                            case (ModListType.CharacterGeneral):
                                ProcessCharacterGeneralModFiles(files, baseDirectory);
                                break;
                            case (ModListType.Stage):
                                ProcessStageModFiles(files, baseDirectory);
                                break;
                            case (ModListType.General):
                                ProcessGeneralModFiles(files, baseDirectory);
                                break;
                        }
                    }
                }
            }
        }

        private string[] ValidateFiles(string[] files)
        {
            List<string> newFiles = new List<string>();
            List<string> newFilesFinal = new List<string>();

            for (int i = 0; i < files.Length; ++i)
            {
                if (!File.Exists(files[i]))
                {
                    string[] subFiles = Directory.GetFiles(files[i], "*", SearchOption.AllDirectories);
                    newFiles.AddRange(subFiles);
                }
            }
            
            foreach (string fileToProcess in newFiles)
            {
                if ((!Utils.IsAnAcceptedExtension(fileToProcess) && !fileToProcess.EndsWith("kamimod.xml")) || fileToProcess.EndsWith("packed"))
                    LogHelper.Error(string.Format("The file '{0}' has a forbidden extension, skipping...", fileToProcess));
                else
                {
                    newFilesFinal.Add(fileToProcess);
                }
            }

            return newFilesFinal.ToArray();
        }

        private void CleanUnzipFolder()
        {
            if (Directory.Exists(PathHelper.FolderTemp + "unzip" + Path.DirectorySeparatorChar))
                Directory.Delete(PathHelper.FolderTemp + "unzip" + Path.DirectorySeparatorChar, true);
        }

        private void ProcessCharacterSlotModFiles(string[] files, string baseDirectory)
        {
            string[] xmlFiles = Utils.FindFilesByFilename(files,"kamimod.xml");

            List<string> XmlModFiles = new List<string>();
            List<CharacterSlotModXML> XmlMods = new List<CharacterSlotModXML>();
            if (xmlFiles != null)
            {
                foreach (string xmlFile in xmlFiles)
                {
                    if (Path.GetFileName(xmlFile) != "kamimod.xml") continue;

                    CharacterSlotModXML mod = Utils.DeserializeXML<CharacterSlotModXML>(xmlFile);
                    if (mod == null) continue;
                    XmlMods.Add(mod);
                    XmlModFiles.Add(xmlFile);
                }
            }

            if (XmlMods.Count > 0)
            {
                for (int i = 0; i < XmlMods.Count; ++i)
                {
                    string oldBasePath = Path.GetDirectoryName(XmlModFiles[0]);

                    string modFolderName = oldBasePath.Split(Path.DirectorySeparatorChar).Last();
                    if (modFolderName == "unzip") modFolderName = XmlMods[i].DisplayName;

                    CharacterSlotModXML xmlTest = Utils.OpenCharacterSlotKamiModFile(_CurrentFighter.name, modFolderName);
                    if (xmlTest != null)
                    {
                        MessageBox.Show(String.Format("Mod already exists under the same folder name '{0}'. Skipping...", modFolderName));
                        return;
                    }

                    oldBasePath = oldBasePath + Path.DirectorySeparatorChar;

                    string newBasePath = PathHelper.FolderCharSlotsMods + _CurrentFighter.name + Path.DirectorySeparatorChar + modFolderName + Path.DirectorySeparatorChar;

                    Utils.CopyAllValidFilesBetweenDirectories(oldBasePath, newBasePath);

                    LogHelper.Info(String.Format("KamiMod {0} imported successfully!", modFolderName));

                    RefreshRowData();
                }
            }
            else
            {
                Forms.ModImportCharacterSlot popup = new Forms.ModImportCharacterSlot(_SmashProjectManager);
                popup._CurrentFighter = _CurrentFighter;
                popup.ModelNutDirectories = Utils.FindFilesByFilename(files, "model.nut");
                popup.Files_chr_00 = Utils.FindFilesByFilename(files, "chr_00");
                popup.Files_stock_90 = Utils.FindFilesByFilename(files, "stock_90");
                popup.Files_chr_11 = Utils.FindFilesByFilename(files, "chr_11");
                popup.Files_chr_13 = Utils.FindFilesByFilename(files, "chr_13");
                popup.Files_chrn_11 = Utils.FindFilesByFilename(files, "chrn_11");
                popup.Files_Sound_Nus3bank = Utils.FindFilesByFilename(files, "snd_se_");
                popup.Files_Voice_Nus3bank = Utils.FindFilesByFilename(files, "snd_vc_");

                popup._BaseFolderPath = baseDirectory;

                popup.Initialize();

                popup.ShowDialog();

                RefreshRowData();
            }
        }

        private void ProcessCharacterGeneralModFiles(string[] files, string baseDirectory)
        {
            string[] xmlFiles = Utils.FindFilesByFilename(files, "kamimod.xml");

            List<string> XmlModFiles = new List<string>();
            List<CharacterGeneralModXML> XmlMods = new List<CharacterGeneralModXML>();
            if (xmlFiles != null)
            {
                foreach (string xmlFile in xmlFiles)
                {
                    if (Path.GetFileName(xmlFile) != "kamimod.xml") continue;

                    CharacterGeneralModXML mod = Utils.DeserializeXML<CharacterGeneralModXML>(xmlFile);
                    if (mod == null) continue;
                    XmlMods.Add(mod);
                    XmlModFiles.Add(xmlFile);
                }
            }

            if (XmlMods.Count > 0)
            {
                for (int i = 0; i < XmlMods.Count; ++i)
                {
                    string oldBasePath = Path.GetDirectoryName(XmlModFiles[0]);

                    string modFolderName = oldBasePath.Split(Path.DirectorySeparatorChar).Last();
                    if (modFolderName == "unzip") modFolderName = XmlMods[i].DisplayName;

                    CharacterGeneralModXML xmlTest = Utils.OpenCharacterGeneralKamiModFile(_CurrentFighter.name, modFolderName);
                    if (xmlTest != null)
                    {
                        MessageBox.Show(String.Format("Mod already exists under the same folder name '{0}'. Skipping...", modFolderName));
                        return;
                    }

                    oldBasePath = oldBasePath + Path.DirectorySeparatorChar;

                    string newBasePath = PathHelper.FolderCharGeneralMods + _CurrentFighter.name + Path.DirectorySeparatorChar + modFolderName + Path.DirectorySeparatorChar;

                    Utils.CopyAllValidFilesBetweenDirectories(oldBasePath, newBasePath);

                    LogHelper.Info(String.Format("KamiMod {0} imported successfully!", modFolderName));

                    RefreshRowData();
                }
            }
            else
            {
                string cameraFolder = Utils.FindDirectoryInFiles(files, "camera", baseDirectory);
                string modelFolder = Utils.FindDirectoryInFiles(files, "model", baseDirectory);
                string effectFolder = Utils.FindDirectoryInFiles(files, "effect", baseDirectory);
                string motionFolder = Utils.FindDirectoryInFiles(files, "motion", baseDirectory);
                string scriptFolder = Utils.FindDirectoryInFiles(files, "script", baseDirectory);
                string soundFolder = Utils.FindDirectoryInFiles(files, "sound", baseDirectory);
                
                string name = baseDirectory.Split(Path.DirectorySeparatorChar).Last();
                Forms.NewModNamePopup popup = new Forms.NewModNamePopup();
                popup.nameText = name;
                bool nameValid = false;

                while (!nameValid)
                {
                    popup.ShowDialog();
                    if (!popup.confirmPressed) return;
                    popup.confirmPressed = false;
                    name = popup.nameText;
                    name = PathHelper.RemoveInvalidFilenameChars(name);
                    if (name.Length < 1)
                    {
                        MessageBox.Show("Invalid mod name given. Please enter a new one.");
                        continue;
                    }
                    CharacterGeneralModXML xmlTest = Utils.OpenCharacterGeneralKamiModFile(_CurrentFighter.name, name);
                    if (xmlTest != null)
                    {
                        MessageBox.Show("Mod already exists under the same folder name. Please enter a new one.");
                        continue;
                    }
                    nameValid = true;
                }
                string newModDirectory = PathHelper.FolderCharGeneralMods + Path.DirectorySeparatorChar + _CurrentFighter.name + Path.DirectorySeparatorChar + name + Path.DirectorySeparatorChar;
                
                #region XML File Creation
                CharacterGeneralModXML xml = new CharacterGeneralModXML();
                xml.DisplayName = name;
                xml.WifiSafe = true; //Assuming wifi-safe
                Utils.SerializeXMLToFile(xml, newModDirectory + "kamimod.xml");
                #endregion

                #region Mod Files
                if (!cameraFolder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(cameraFolder, newModDirectory + "camera" + Path.DirectorySeparatorChar);
                if (!modelFolder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(modelFolder, newModDirectory + "model" + Path.DirectorySeparatorChar);
                if (!effectFolder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(effectFolder, newModDirectory + "effect" + Path.DirectorySeparatorChar);
                if (!motionFolder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(motionFolder, newModDirectory + "motion" + Path.DirectorySeparatorChar);
                if (!scriptFolder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(scriptFolder, newModDirectory + "script" + Path.DirectorySeparatorChar);
                if (!soundFolder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(soundFolder, newModDirectory + "sound" + Path.DirectorySeparatorChar);
                #endregion

                LogHelper.Info(String.Format("Mod {0} imported successfully!", name));

                RefreshRowData();
            }
        }

        private void ProcessStageModFiles(string[] files, string baseDirectory)
        {
            string[] xmlFiles = Utils.FindFilesByFilename(files, "kamimod.xml");

            List<string> XmlModFiles = new List<string>();
            List<StageModXML> XmlMods = new List<StageModXML>();
            if (xmlFiles != null)
            {
                foreach (string xmlFile in xmlFiles)
                {
                    if (Path.GetFileName(xmlFile) != "kamimod.xml") continue;

                    StageModXML mod = Utils.DeserializeXML<StageModXML>(xmlFile);
                    if (mod == null) continue;
                    XmlMods.Add(mod);
                    XmlModFiles.Add(xmlFile);
                }
            }

            if (XmlMods.Count > 0)
            {
                for (int i = 0; i < XmlMods.Count; ++i)
                {
                    string oldBasePath = Path.GetDirectoryName(XmlModFiles[0]);

                    string modFolderName = oldBasePath.Split(Path.DirectorySeparatorChar).Last();
                    if (modFolderName == "unzip") modFolderName = XmlMods[i].DisplayName;

                    StageModXML xmlTest = Utils.OpenStageKamiModFile(modFolderName);
                    if (xmlTest != null)
                    {
                        MessageBox.Show(String.Format("Mod already exists under the same folder name '{0}'. Skipping...", modFolderName));
                        return;
                    }

                    oldBasePath = oldBasePath + Path.DirectorySeparatorChar;

                    string newBasePath = PathHelper.FolderStageMods + modFolderName + Path.DirectorySeparatorChar;

                    Utils.CopyAllValidFilesBetweenDirectories(oldBasePath, newBasePath);

                    LogHelper.Info(String.Format("KamiMod {0} imported successfully!", modFolderName));

                    RefreshRowData();
                }
            }
            else
            {
                string stageFolder = Utils.FindDirectoryInFiles(files, DB.StagesDB.StageNames, baseDirectory);

                //Check if all the folders are either empty, or don't exist
                if (stageFolder.Equals(String.Empty))
                {
                    MessageBox.Show("No valid mod files or directories found. Make sure that the mod files follow the Sm4shExplorer file tree, and have a root folder of 'melee' or 'end' that has a stage folder such as 'BattleField_f'");
                    LogHelper.Error("No valid mod files or directories found. Make sure that the mod files follow the Sm4shExplorer file tree, and have a root folder of 'melee' or 'end' that has a stage folder such as 'BattleField_f'");
                    return;
                }
                
                DB.Stage stage = null;
                string[] folderParts = stageFolder.Split(Path.DirectorySeparatorChar);
                string folder1 = folderParts[folderParts.Length - 2];
                string folder2 = folderParts.Last();
                if (folder1.Equals("melee"))
                {
                    foreach (DB.Stage st in DB.StagesDB.Stages)
                    {
                        if (st.Type != DB.StageType.Melee) continue;
                        if (!folder2.Equals(st.Label)) continue;
                        stage = st;
                        break;
                    }
                }
                else if (folder1.Equals("end"))
                {
                    foreach (DB.Stage st in DB.StagesDB.Stages)
                    {
                        if (st.Type != DB.StageType.End) continue;
                        if (!folder2.Equals(st.Label)) continue;
                        stage = st;
                        break;
                    }
                }

                string name = baseDirectory.Split(Path.DirectorySeparatorChar).Last();
                Forms.NewModNamePopup popup = new Forms.NewModNamePopup();
                popup.nameText = name;
                bool nameValid = false;

                while (!nameValid)
                {
                    popup.ShowDialog();
                    if (!popup.confirmPressed) return;
                    popup.confirmPressed = false;
                    name = popup.nameText;
                    name = PathHelper.RemoveInvalidFilenameChars(name);
                    if (name.Length < 1)
                    {
                        MessageBox.Show("Invalid mod name given. Please enter a new one.");
                        continue;
                    }
                    StageModXML xmlTest = Utils.OpenStageKamiModFile(name);
                    if (xmlTest != null)
                    {
                        MessageBox.Show("Mod already exists under the same folder name. Please enter a new one.");
                        continue;
                    }
                    nameValid = true;
                }
                string newModDirectory = PathHelper.FolderStageMods + Path.DirectorySeparatorChar + name + Path.DirectorySeparatorChar;
                
                string[] Files_stage_10 = Utils.FindFilesByFilename(files, "stage_10");
                string[] Files_stage_11 = Utils.FindFilesByFilename(files, "stage_11");
                string[] Files_stage_12 = Utils.FindFilesByFilename(files, "stage_12");
                string[] Files_stage_13 = Utils.FindFilesByFilename(files, "stage_13");
                string[] Files_stage_30 = Utils.FindFilesByFilename(files, "stage_30");
                string[] Files_stagen_10 = Utils.FindFilesByFilename(files, "stagen_10");

                #region XML File Creation
                StageModXML xml = new StageModXML();
                xml.DisplayName = name;
                xml.WifiSafe = true; //Assuming wifi-safe
                xml.IntendedStage = stage.ID;
                xml.stage_10 = Files_stage_10.Length > 0;
                xml.stage_11 = Files_stage_11.Length > 0;
                xml.stage_12 = Files_stage_12.Length > 0;
                xml.stage_13 = Files_stage_13.Length > 0;
                xml.stage_30 = Files_stage_30.Length > 0;
                xml.stagen_10 = Files_stagen_10.Length > 0;
                Utils.SerializeXMLToFile(xml, newModDirectory + "kamimod.xml");
                #endregion
                
                Utils.CopyAllValidFilesBetweenDirectories(stageFolder, newModDirectory + "stage" + Path.DirectorySeparatorChar);

                if (xml.stage_10 || xml.stage_11 || xml.stage_12 || xml.stage_13 || xml.stage_30 || xml.stagen_10)
                {
                    string baseChrPath = newModDirectory + "ui" + Path.DirectorySeparatorChar;
                    Directory.CreateDirectory(baseChrPath);
                    if (xml.stage_10) File.Copy(Files_stage_10[0], baseChrPath + "stage_10_XX.nut");
                    if (xml.stage_11) File.Copy(Files_stage_11[0], baseChrPath + "stage_11_XX.nut");
                    if (xml.stage_12) File.Copy(Files_stage_12[0], baseChrPath + "stage_12_XX.nut");
                    if (xml.stage_13) File.Copy(Files_stage_13[0], baseChrPath + "stage_13_XX.nut");
                    if (xml.stage_30) File.Copy(Files_stage_30[0], baseChrPath + "stage_30_XX.nut");
                    if (xml.stagen_10) File.Copy(Files_stagen_10[0], baseChrPath + "stagen_10_XX.nut");
                }

                LogHelper.Info(String.Format("Mod {0} imported successfully!", name));

                RefreshRowData();
            }
        }

        private void ProcessGeneralModFiles(string[] files, string baseDirectory)
        {
            string[] xmlFiles = Utils.FindFilesByFilename(files, "kamimod.xml");

            List<string> XmlModFiles = new List<string>();
            List<GeneralModXML> XmlMods = new List<GeneralModXML>();
            if (xmlFiles != null)
            {
                foreach (string xmlFile in xmlFiles)
                {
                    if (Path.GetFileName(xmlFile) != "kamimod.xml") continue;

                    GeneralModXML mod = Utils.DeserializeXML<GeneralModXML>(xmlFile);
                    if (mod == null) continue;
                    XmlMods.Add(mod);
                    XmlModFiles.Add(xmlFile);
                }
            }

            if (XmlMods.Count > 0)
            {
                for (int i = 0; i < XmlMods.Count; ++i)
                {
                    string oldBasePath = Path.GetDirectoryName(XmlModFiles[0]);

                    string modFolderName = oldBasePath.Split(Path.DirectorySeparatorChar).Last();
                    if (modFolderName == "unzip") modFolderName = XmlMods[i].DisplayName;

                    GeneralModXML xmlTest = Utils.OpenGeneralKamiModFile(modFolderName);
                    if (xmlTest != null)
                    {
                        MessageBox.Show(String.Format("Mod already exists under the same folder name '{0}'. Skipping...", modFolderName));
                        return;
                    }

                    oldBasePath = oldBasePath + Path.DirectorySeparatorChar;

                    string newBasePath = PathHelper.FolderGeneralMods + modFolderName + Path.DirectorySeparatorChar;

                    Utils.CopyAllValidFilesBetweenDirectories(oldBasePath, newBasePath);

                    LogHelper.Info(String.Format("KamiMod {0} imported successfully!", modFolderName));

                    RefreshRowData();
                }
            }
            else
            {
                string data_Folder = Utils.FindDirectoryInFiles(files, "data", baseDirectory);
                string data_en_Folder = Utils.FindDirectoryInFiles(files, "data(us_en)", baseDirectory);
                string data_fr_Folder = Utils.FindDirectoryInFiles(files, "data(us_fr)", baseDirectory);
                string data_sp_Folder = Utils.FindDirectoryInFiles(files, "data(us_sp)", baseDirectory);

                //Check if all the folders are either empty, or don't exist
                if (data_Folder.Equals(String.Empty) && data_en_Folder.Equals(String.Empty) && data_fr_Folder.Equals(String.Empty) && data_sp_Folder.Equals(String.Empty))
                {
                    MessageBox.Show("No valid mod files or directories found. Make sure that the mod files follow the Sm4shExplorer file tree, and have a root folder of 'data', 'data(us_en)', 'data(us_fr)' and/or 'data(us_sp).'");
                    LogHelper.Error("No valid mod files or directories found. Make sure that the mod files follow the Sm4shExplorer file tree, and have a root folder of 'data', 'data(us_en)', 'data(us_fr)' and/or 'data(us_sp).'");
                    return;
                }

                string name = baseDirectory.Split(Path.DirectorySeparatorChar).Last();
                Forms.NewModNamePopup popup = new Forms.NewModNamePopup();
                popup.nameText = name;
                bool nameValid = false;
                while (!nameValid)
                {
                    popup.ShowDialog();
                    if (!popup.confirmPressed) return;
                    popup.confirmPressed = false;
                    name = popup.nameText;
                    name = PathHelper.RemoveInvalidFilenameChars(name);
                    if (name.Length < 1)
                    {
                        MessageBox.Show("Invalid mod name given. Please enter a new one.");
                        continue;
                    }
                    GeneralModXML xmlTest = Utils.OpenGeneralKamiModFile(name);
                    if (xmlTest != null)
                    {
                        MessageBox.Show("Mod already exists under the same folder name. Please enter a new one.");
                        continue;
                    }
                    nameValid = true;
                }

                string newModDirectory = PathHelper.FolderGeneralMods + Path.DirectorySeparatorChar + name + Path.DirectorySeparatorChar;

                #region XML File Creation
                GeneralModXML xml = new GeneralModXML();
                xml.DisplayName = name;
                xml.WifiSafe = true; //Assuming wifi-safe
                Utils.SerializeXMLToFile(xml, newModDirectory + "kamimod.xml");
                #endregion

                #region Mod Files
                if (!data_Folder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(data_Folder, newModDirectory + "data" + Path.DirectorySeparatorChar);
                if (!data_en_Folder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(data_en_Folder, newModDirectory + "data(us_en)" + Path.DirectorySeparatorChar);
                if (!data_fr_Folder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(data_fr_Folder, newModDirectory + "data(us_fr)" + Path.DirectorySeparatorChar);
                if (!data_sp_Folder.Equals(String.Empty)) Utils.CopyAllValidFilesBetweenDirectories(data_sp_Folder, newModDirectory + "data(us_sp)" + Path.DirectorySeparatorChar);
                #endregion

                LogHelper.Info(String.Format("Mod {0} imported successfully!", name));

                RefreshRowData();
            }
        }
        #endregion

        #endregion

        public class RowData
        {
            public int slotNum = 0;
            public string name = String.Empty;
            public int textureID = -1;
            public string modFolder = String.Empty;
            public bool propertiesEnabled = true;
            public CharacterSlotModXML.MetalModelStatus metal = CharacterSlotModXML.MetalModelStatus.Works;
            public bool missingModel = false;
            public bool missingPortraits = false;
            public bool hasAudio = false;
            public bool hasCustomName = false;
            public bool wifiSafe = true;
            public bool modMissing = false;
        }
    }
}
