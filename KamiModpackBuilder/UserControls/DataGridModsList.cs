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
    public partial class DataGridModsList : UserControl
    {
        #region Members
        private bool _IsActiveList = false;
        private ModListType _ModListType = ModListType.General;
        private SmashProjectManager _SmashProjectManager;
        private SmashMod _Project;
        private DB.Fighter _CurrentFighter;
        private List<RowData> _RowData = new List<RowData>();
        private List<ModRow> _Rows = new List<ModRow>();
        #endregion

        #region Enums
        public enum ModListType { CharacterSlots, CharacterGeneral, Stage, General }
        #endregion

        #region Constructors
        public DataGridModsList(SmashProjectManager a_smashProjectManager, bool a_isActiveList, ModListType a_modListType)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;
            _IsActiveList = a_isActiveList;
            _ModListType = a_modListType;
        }
        #endregion

        #region Public Methods
        public void ChangeSelectedFighter(DB.Fighter a_fighter)
        {
            _CurrentFighter = a_fighter;
            if (_Rows != null) for (int i = 0; i < _Rows.Count;  ++i)
                {
                    _Rows[i].ChangeSelectedFighter(_CurrentFighter);
                }
            RefreshRowData();
        }
        #endregion

        #region Private Methods

        public void RefreshRowData()
        {
            _RowData = new List<RowData>();
            _Project = _SmashProjectManager.CurrentProject;

            string baseDirectory = String.Empty;
            switch (_ModListType) {
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
                for (int j = 0; j < _Project.ActiveCharacterSlotMods.Count; ++j)
                {
                    if (_Project.ActiveCharacterSlotMods[j].FolderName.Equals(modFolderName))
                    {
                        modIsActive = true;
                        break;
                    }
                }
                if (modIsActive) continue;

                RowData row = new RowData();
                CharacterSlotModXML data = Utils.DeserializeXML<CharacterSlotModXML>(kamiFiles[i]);
                row.modFolder = modFolderName;
                row.name = data.DisplayName;
                _RowData.Add(row);
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
                ModRow row = new ModRow(_SmashProjectManager, _IsActiveList, _ModListType);
                row.ChangeSelectedFighter(_CurrentFighter);
                row.UpdateData(_RowData[i]);
                row.Dock = DockStyle.Top;
                _Rows.Add(row);
                row.Parent = panelModList;
            }
        }

        public void SelectMod(string modFolderName)
        {
            for (int i = 0; i < _Rows.Count; ++i)
            {
                if (_Rows[i].modFolder.Equals(modFolderName))
                {
                    switch (_ModListType)
                    {
                        case (DataGridModsList.ModListType.CharacterSlots):
                            EventManager.CharSlotModSelectionChanged(_Rows[i]); return;
                        case (DataGridModsList.ModListType.CharacterGeneral):
                            EventManager.CharGeneralModSelectionChanged(_Rows[i]); return;
                        case (DataGridModsList.ModListType.Stage):
                            EventManager.StageModSelectionChanged(_Rows[i]); return;
                        case (DataGridModsList.ModListType.General):
                            EventManager.MiscModSelectionChanged(_Rows[i]); return;
                    }
                }
            }
        }

        #region Events
        private void DataGridModsList_DragDrop(object sender, DragEventArgs e)
        {
            if (!_IsActiveList) ParseImportFiles(e.Data.GetData(DataFormats.FileDrop) as string[]);
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
        #endregion

        #region Mod Import Processing
        private void ParseImportFiles(string[] files, bool isZip = true)
        {
            if (files.Length > 1)
            {
                MessageBox.Show("Only drag one zip file or folder!");
                return;
            }

            if (Path.GetExtension(files[0]) == ".zip")
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

                    oldBasePath = oldBasePath + Path.DirectorySeparatorChar;

                    string newBasePath = PathHelper.FolderCharSlotsMods + _CurrentFighter.name + Path.DirectorySeparatorChar + modFolderName + Path.DirectorySeparatorChar;

                    Utils.CopyAllValidFilesBetweenDirectories(oldBasePath, newBasePath);

                    LogHelper.Info(String.Format("KamiMod {0} imported successfully!", modFolderName));
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

        }

        private void ProcessStageModFiles(string[] files, string baseDirectory)
        {

        }

        private void ProcessGeneralModFiles(string[] files, string baseDirectory)
        {

        }
        #endregion

        #endregion

        public class RowData
        {
            public string name = String.Empty;
            public int textureID = -1;
            public string warningText = String.Empty;
            public bool hasWarning = false;
            public string errorText = String.Empty;
            public bool hasError = false;
            public string modFolder = String.Empty;
            public bool propertiesEnabled = true;
        }
    }
}
