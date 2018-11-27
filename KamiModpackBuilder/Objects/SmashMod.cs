using System.Collections.Generic;
using System.Xml.Serialization;

namespace KamiModpackBuilder.Objects
{
    public class SmashMod
    {
        public string GamePath { get; set; }

        public string ProjectExportFolder { get; set; }
        public string ProjectExtractFolder { get; set; }
        public string ProjectExplorerFolder { get; set; }
        public string ProjectWorkspaceFolder { get; set; }

        [XmlIgnore]
        public string ProjectExportFolderFullPath = string.Empty;
        [XmlIgnore]
        public string ProjectExtractFolderFullPath = string.Empty;
        [XmlIgnore]
        public string ProjectExplorerFolderFullPath = string.Empty;
        [XmlIgnore]
        public string ProjectWorkspaceFolderFullPath = string.Empty;

        public List<CharacterSlotMod> ActiveCharacterSlotMods { get; set; }
        public List<StageSlotMod> ActiveStageMods { get; set; }
        public List<CharacterGeneralMod> ActiveCharacterGeneralMods { get; set; }
        public List<string> ActiveGeneralMods { get; set; }
        public List<CharacterAudioSlotSelection> CharacterAudioSlotSelections { get; set; }
        public bool EditorCSSActive { get; set; } //Whether the modified CSS created in the internal editor will be used in the build.
        public bool EditorSSSActive { get; set; } //Whether the modified SSS created in the internal editor will be used in the build.
        public bool EditorCharacterMenuDBActive { get; set; } //Whether the global_menu_parameter database file is updated automatically and used in the build.
        public bool EditorCharacterStringsActive { get; set; } //Whether the Character Nameplate numbers, Name strings, and Boxing ring texts specified by character mods will be used in the build.
        public bool EditorMusicActive { get; set; } //Whether the modified music databases and strings by Sm4shMusic will be used in the build.
        public bool EditorMTBFix { get; set; } //Whether the modified MTB for custom sounds and voices is used when building.
        public bool EditorExplorerChanges { get; set; } //Whether files modified through Explorer Tree (such as deletions) will be included in the build.

        public bool YoshiFixActive { get; set; } //Whether Yoshi Fix (allowing yoshi to have fixed effects on slots higher than 8) will automatically be updated and included in the build.
        public bool DuplicateToOtherRegions { get; set; } //Duplicate applicable mod files to other regions automatically.
        public bool OverrideLowPolyModels { get; set; } //Duplicate character slot mods over the lxx folder when neccessary if an lxx variant isn't included in the mod.
        public bool AutoTextureIDFix { get; set; } //Automatically TextureID fix all character slot mods when conflicts are found before building. Overrides AutoTextureIDCheck.
        
        public bool SupressWarningTextureIDConflicts { get; set; }
        public bool SupressWarningMissingPortraits { get; set; }
        public bool SupressWarningModFileConflicts { get; set; }
        public bool SupressWarningSlotModModelFilesMissing { get; set; } //When a slot mod is missing the model.nud, model.nut or metal.nut files, or when a mod over an added slot is missing other files.

        public bool EnableMoreCustomSlots { get; set; } //Allows custom slot counts to surpass the built-in maximum. (up to 32)

        public bool SkipJunkEntries { get; set; } //Default value is true, false will keep the empty folder and empty files.
        public bool KeepOriginalFlags { get; set; } //Default value is false, true will force the original flags of the resource (if they exist)
        public bool ExportCSVList { get; set; }
        public bool ExportCSVIgnoreFlags { get; set; }
        public bool ExportCSVIgnorePackOffsets { get; set; }
        public bool ExportCSVIgnoreCompSize { get; set; }
        public bool ExportWithDateFolder { get; set; }

        public bool BuildIsWifiSafe { get; set; }
        public int BuildSafetySetting { get; set; }

        public List<SmashModItem> UnlocalizationItems { get; set; }
        public List<SmashModItem> ResourcesToRemove { get; set; }
        public List<SmashModItem> ResourcesToPack { get; set; }
        //public List<string> PluginsOrder { get; set; }

        public bool IsSwitch { get; set; } //TODO SUPPORT
        public int GameVersion { get; set; }
        public int GameRegionID { get; set; }

        [XmlIgnore]
        public string GameRegion { get { return GetRegionName(); } }
        [XmlIgnore]
        public string GameID { get { return GetGameID(); } }
        [XmlIgnore]
        public bool DTFilesFound { get; internal set; }

        #region Constructors
        public SmashMod()
        {
            ActiveCharacterSlotMods = new List<CharacterSlotMod>();
            ActiveStageMods = new List<StageSlotMod>();
            ActiveCharacterGeneralMods = new List<CharacterGeneralMod>();
            ActiveGeneralMods = new List<string>();
            EditorCSSActive = true;
            EditorSSSActive = true;
            EditorCharacterMenuDBActive = true;
            EditorCharacterStringsActive = true;
            EditorMusicActive = true;
            EditorMTBFix = true;
            EditorExplorerChanges = true;

            ExportWithDateFolder = true;

            YoshiFixActive = true;
            DuplicateToOtherRegions = true;
            OverrideLowPolyModels = true;
            AutoTextureIDFix = false;

            SupressWarningTextureIDConflicts = false;
            SupressWarningMissingPortraits = false;
            SupressWarningModFileConflicts = false;
            SupressWarningSlotModModelFilesMissing = false;

            EnableMoreCustomSlots = false;

            SkipJunkEntries = true;
            ExportCSVList = false;
            ExportCSVIgnoreCompSize = true;
            ExportCSVIgnoreFlags = true;
            ExportCSVIgnorePackOffsets = true;

            BuildIsWifiSafe = false;
            BuildSafetySetting = 0;
    }
        #endregion

        #region Unlocalize
        internal void AddUnlocalized(string partition, string relativePath)
        {
            AddSmashModItem(UnlocalizationItems, partition, relativePath);
        }

        internal void RemoveUnlocalized(string partition, string relativePath)
        {
            RemoveSmashModItem(UnlocalizationItems, partition, relativePath);
        }

        internal bool IsUnlocalized(string partition, string relativePath)
        {
            if (partition == "data")
                return false;
            return IsSmashModItem(UnlocalizationItems, partition, relativePath);
        }
        #endregion

        #region Resource Removal
        internal void RemoveOriginalResource(string partition, string relativePath)
        {
            AddSmashModItem(ResourcesToRemove, partition, relativePath);
        }

        internal void ReintroduceOriginalResource(string partition, string relativePath)
        {
            RemoveSmashModItem(ResourcesToRemove, partition, relativePath);
        }

        internal bool IsResourceRemoved(string partition, string relativePath)
        {
            return IsSmashModItem(ResourcesToRemove, partition, relativePath);
        }
        #endregion

        #region Resource Pack
        internal void PackResource(string partition, string relativePath)
        {
            AddSmashModItem(ResourcesToPack, partition, relativePath);
        }

        internal void RemovePackResource(string partition, string relativePath)
        {
            RemoveSmashModItem(ResourcesToPack, partition, relativePath);
        }

        internal bool IsResourceToBePacked(string partition, string relativePath)
        {
            return IsSmashModItem(ResourcesToPack, partition, relativePath);
        }

        internal bool IsResourceInPackage(string partition, string relativePath)
        {
            if (ResourcesToPack == null)
                ResourcesToPack = new List<SmashModItem>();

            SmashModItem resCol = ResourcesToPack.Find(p => p.Partition == partition);
            if (resCol == null)
                return false;

            string pathFound = resCol.Paths.Find(p => relativePath.StartsWith(p));
            if (!string.IsNullOrEmpty(pathFound))
                return true;
            return false;
        }
        #endregion

        #region Generic
        private void AddSmashModItem(List<SmashModItem> list, string partition, string relativePath)
        {
            if (list == null)
                list = new List<SmashModItem>();

            SmashModItem resCol = list.Find(p => p.Partition == partition);
            if (resCol == null)
            {
                resCol = new SmashModItem() { Partition = partition, Paths = new List<string>() };
                list.Add(resCol);
            }
            string checkPathExist = resCol.Paths.Find(p => p == relativePath);
            if (string.IsNullOrEmpty(checkPathExist))
                resCol.Paths.Add(relativePath);
        }

        private void RemoveSmashModItem(List<SmashModItem> list, string partition, string relativePath)
        {
            if (list == null)
                list = new List<SmashModItem>();

            SmashModItem resCol = list.Find(p => p.Partition == partition);
            if (resCol == null)
                return;
            resCol.Paths.Remove(relativePath);
        }

        private bool IsSmashModItem(List<SmashModItem> list, string partition, string relativePath)
        {
            if (list == null)
                list = new List<SmashModItem>();

            SmashModItem resCol = list.Find(p => p.Partition == partition);
            if (resCol == null)
                return false;

            string pathFound = resCol.Paths.Find(p => (p.EndsWith("packed") && relativePath.StartsWith(p.Substring(0, p.LastIndexOf("packed"))) || p == relativePath));
            if (!string.IsNullOrEmpty(pathFound))
                return true;
            return false;
        }
        #endregion

        private string GetRegionName()
        {
            return GetRegionName(GameRegionID);
        }

        private string GetRegionName(int regionID)
        {
            switch (GameRegionID)
            {
                case 1:
                    return "JAP";
                case 2:
                    return "USA";
                case 3:
                    return "???";
                case 4:
                    return "EUR";
            }
            return "???";
        }

        private string GetGameID()
        {
            switch (GameRegionID)
            {
                case 1:
                    return "AXFJ01";
                case 2:
                    return "AXFE01";
                case 3:
                    return "????01";
                case 4:
                    return "AXFP01";
            }
            return "???";
        }

        /// <summary>
        /// Checks to see if the Region set in the project matches the region of the DT files.
        /// </summary>
        internal bool CheckDTSizeWiiU(long dt00size, long dt01size)
        {
            return CheckDTSizeWiiU(dt00size, dt01size, GameRegionID);
        }

        /// <summary>
        /// Used to guess the game region. Returns true if the determined region matches the given regionID.
        /// </summary>
        internal bool CheckDTSizeWiiU(long dt00size, long dt01size, int regionID)
        {
            switch (regionID)
            {
                case 1:
                    if(dt00size == 4082912512 && dt01size == 2398254253) //JAP
                        return true;
                    break;
                case 2:
                    if(dt00size == 4083470592 && dt01size == 2409697069) //USA
                        return true;
                    break;
                case 3:
                    return false;//???
                case 4:
                    if (dt00size == 4085073920 && dt01size == 4038462509) //EUR
                        return true;
                    break;
            }
            return false;
        }

        /// <summary>
        /// Returns the region the game files are for based on the DT Files.
        /// </summary>
        internal string GuessRegionFromDTFiles(long dt00size, long dt01size)
        {
            if (CheckDTSizeWiiU(dt00size, dt01size, 1))
                return GetRegionName(1);
            if (CheckDTSizeWiiU(dt00size, dt01size, 2))
                return GetRegionName(2);
            if (CheckDTSizeWiiU(dt00size, dt01size, 3))
                return GetRegionName(3);
            if (CheckDTSizeWiiU(dt00size, dt01size, 4))
                return GetRegionName(4);
            return string.Empty;
        }

        public int GetAudioSlotForFighter(int fighterID, bool voice, int index)
        {
            for (int i = 0; i < CharacterAudioSlotSelections.Count; ++i)
            {
                if (fighterID == CharacterAudioSlotSelections[i].CharacterID)
                {
                    if (voice)
                    {
                        if (index == 0) return CharacterAudioSlotSelections[i].Voice1;
                        else return CharacterAudioSlotSelections[i].Voice2;
                    }
                    else
                    {
                        if (index == 0) return CharacterAudioSlotSelections[i].Sound1;
                        else return CharacterAudioSlotSelections[i].Sound2;
                    }
                }
            }
            return -1;
        }
    }
    
    /// <summary>
    /// Sm4shExplorer Mod node
    /// </summary>
    public class SmashModItem
    {
        public string Partition { get; set; }
        public List<string> Paths { get; set; }
    }

    /// <summary>
    /// Character Slot Mod Savedata
    /// </summary>
    public class CharacterSlotMod
    {
        public int CharacterID { get; set; }
        public string FolderName { get; set; }
        public int SlotID { get; set; }
    }

    /// <summary>
    /// Character General Mod Savedata
    /// </summary>
    public class CharacterGeneralMod
    {
        public int CharacterID { get; set; }
        public string FolderName { get; set; }
    }

    /// <summary>
    /// Stage Mod Savedata
    /// </summary>
    public class StageSlotMod
    {
        public int StageID { get; set; }
        public string FolderName { get; set; }
    }

    /// <summary>
    /// XML Data for Character Slot Mods
    /// </summary>
    public class CharacterSlotModXML
    {

        public string DisplayName
        {
            get { return m_DisplayName; }
            set
            {
                if (m_DisplayName != null)
                    if (!m_DisplayName.Equals(value))
                        isDirty = true;
                m_DisplayName = value;
            }
        }
        public int TextureID
        {
            get { return m_TextureID; }
            set
            {
                if (m_TextureID != value)
                {
                    isDirty = true;
                }
                m_TextureID = value;
            }
        }
        public bool Haslxx
        {
            get { return m_Haslxx; }
            set
            {
                if (m_Haslxx != value)
                {
                    isDirty = true;
                }
                m_Haslxx = value;
            }
        }
        public bool chr_00
        {
            get { return m_chr_00; }
            set
            {
                if (m_chr_00 != value)
                {
                    isDirty = true;
                }
                m_chr_00 = value;
            }
        }
        public bool chr_11
        {
            get { return m_chr_11; }
            set
            {
                if (m_chr_11 != value)
                {
                    isDirty = true;
                }
                m_chr_11 = value;
            }
        }
        public bool chr_13
        {
            get { return m_chr_13; }
            set
            {
                if (m_chr_13 != value)
                {
                    isDirty = true;
                }
                m_chr_13 = value;
            }
        }
        public bool stock_90
        {
            get { return m_stock_90; }
            set
            {
                if (m_stock_90 != value)
                {
                    isDirty = true;
                }
                m_stock_90 = value;
            }
        }
        public bool chrn_11
        {
            get { return m_chrn_11; }
            set
            {
                if (m_chrn_11 != value)
                {
                    isDirty = true;
                }
                m_chrn_11 = value;
            }
        }
        public bool Sound
        {
            get { return m_Sound; }
            set
            {
                if (m_Sound != value)
                {
                    isDirty = true;
                }
                m_Sound = value;
            }
        }
        public bool Voice
        {
            get { return m_Voice; }
            set
            {
                if (m_Voice != value)
                {
                    isDirty = true;
                }
                m_Voice = value;
            }
        }
        public bool UseCustomName
        {
            get { return m_UseCustomName; }
            set
            {
                if (m_UseCustomName != value)
                {
                    isDirty = true;
                }
                m_UseCustomName = value;
            }
        }
        public string CharacterName
        {
            get { return m_CharacterName; }
            set
            {
                if (m_CharacterName != null)
                    if (!m_CharacterName.Equals(value))
                        isDirty = true;
                m_CharacterName = value;
            }
        }
        public string BoxingRingText
        {
            get { return m_BoxingRingText; }
            set
            {
                if (m_BoxingRingText != null)
                    if (!m_BoxingRingText.Equals(value))
                        isDirty = true;
                m_BoxingRingText = value;
            }
        }
        public bool WifiSafe
        {
            get { return m_WifiSafe; }
            set
            {
                if (m_WifiSafe != value)
                {
                    isDirty = true;
                }
                m_WifiSafe = value;
            }
        }
        public MetalModelStatus MetalModel
        {
            get { return m_MetalModel; }
            set
            {
                if (m_MetalModel != value)
                {
                    isDirty = true;
                }
                m_MetalModel = value;
            }
        }
        public string Notes
        {
            get { return m_Notes; }
            set
            {
                if (m_Notes != null)
                    if (!m_Notes.Equals(value))
                        isDirty = true;
                m_Notes = value;
            }
        }

        [XmlIgnore] public bool isDirty = false;

        [XmlIgnore] private string m_DisplayName;
        [XmlIgnore] private int m_TextureID;
        [XmlIgnore] private bool m_Haslxx;
        [XmlIgnore] private bool m_chr_00;
        [XmlIgnore] private bool m_chr_11;
        [XmlIgnore] private bool m_chr_13;
        [XmlIgnore] private bool m_stock_90;
        [XmlIgnore] private bool m_chrn_11;
        [XmlIgnore] private bool m_Sound;
        [XmlIgnore] private bool m_Voice;
        [XmlIgnore] private bool m_UseCustomName;
        [XmlIgnore] private string m_CharacterName;
        [XmlIgnore] private string m_BoxingRingText;
        [XmlIgnore] private bool m_WifiSafe;
        [XmlIgnore] private MetalModelStatus m_MetalModel;
        [XmlIgnore] private string m_Notes;

        public enum MetalModelStatus { Unknown, Works, Missing, Crashes}

        public CharacterSlotModXML()
        {
            
        }
    }

    /// <summary>
    /// XML Data for Character General Mods
    /// </summary>
    public class CharacterGeneralModXML
    {
        public string DisplayName
        {
            get { return m_DisplayName; }
            set
            {
                if (m_DisplayName != null)
                    if (!m_DisplayName.Equals(value))
                        isDirty = true;
                m_DisplayName = value;
            }
        }
        public bool WifiSafe
        {
            get { return m_WifiSafe; }
            set
            {
                if (m_WifiSafe != value)
                    isDirty = true;
                m_WifiSafe = value;
            }
        }
        public string Notes
        {
            get { return m_Notes; }
            set
            {
                if (m_Notes != null)
                    if (!m_Notes.Equals(value))
                        isDirty = true;
                m_Notes = value;
            }
        }

        [XmlIgnore] public bool isDirty = false;

        [XmlIgnore] private string m_DisplayName;
        [XmlIgnore] private bool m_WifiSafe;
        [XmlIgnore] private string m_Notes;

        public CharacterGeneralModXML()
        {

        }
    }

    /// <summary>
    /// XML Data for Stage Mods
    /// </summary>
    public class StageModXML
    {
        public string DisplayName
        {
            get { return m_DisplayName; }
            set
            {
                if (m_DisplayName != null)
                    if (!m_DisplayName.Equals(value))
                        isDirty = true;
                m_DisplayName = value;
            }
        }
        public int IntendedStage
        {
            get { return m_IntendedStage; }
            set
            {
                if (m_IntendedStage != value)
                    isDirty = true;
                m_IntendedStage = value;
            }
        }
        public bool WifiSafe
        {
            get { return m_WifiSafe; }
            set
            {
                if (m_WifiSafe != value)
                    isDirty = true;
                m_WifiSafe = value;
            }
        }
        public string Notes
        {
            get { return m_Notes; }
            set
            {
                if (m_Notes != null)
                    if (!m_Notes.Equals(value))
                        isDirty = true;
                m_Notes = value;
            }
        }
        public bool stage_10
        {
            get { return m_stage_10; }
            set
            {
                if (m_stage_10 != value)
                    isDirty = true;
                m_stage_10 = value;
            }
        }
        public bool stage_11
        {
            get { return m_stage_11; }
            set
            {
                if (m_stage_11 != value)
                    isDirty = true;
                m_stage_11 = value;
            }
        }
        public bool stage_12
        {
            get { return m_stage_12; }
            set
            {
                if (m_stage_12 != value)
                    isDirty = true;
                m_stage_12 = value;
            }
        }
        public bool stage_13
        {
            get { return m_stage_13; }
            set
            {
                if (m_stage_13 != value)
                    isDirty = true;
                m_stage_13 = value;
            }
        }
        public bool stage_30
        {
            get { return m_stage_30; }
            set
            {
                if (m_stage_30 != value)
                    isDirty = true;
                m_stage_30 = value;
            }
        }
        public bool stagen_10
        {
            get { return m_stagen_10; }
            set
            {
                if (m_stagen_10 != value)
                    isDirty = true;
                m_stagen_10 = value;
            }
        }

        [XmlIgnore] public bool isDirty = false;

        [XmlIgnore] private string m_DisplayName;
        [XmlIgnore] private int m_IntendedStage;
        [XmlIgnore] private bool m_WifiSafe;
        [XmlIgnore] private string m_Notes;
        [XmlIgnore] private bool m_stage_10;
        [XmlIgnore] private bool m_stage_11;
        [XmlIgnore] private bool m_stage_12;
        [XmlIgnore] private bool m_stage_13;
        [XmlIgnore] private bool m_stage_30;
        [XmlIgnore] private bool m_stagen_10;

        public StageModXML()
        {

        }
    }

    /// <summary>
    /// XML Data for General Mods
    /// </summary>
    public class GeneralModXML
    {
        public string DisplayName
        {
            get { return m_DisplayName; }
            set
            {
                if (m_DisplayName != null)
                    if (!m_DisplayName.Equals(value))
                        isDirty = true;
                m_DisplayName = value;
            }
        }
        public bool WifiSafe
        {
            get { return m_WifiSafe; }
            set
            {
                if (m_WifiSafe != value)
                    isDirty = true;
                m_WifiSafe = value;
            }
        }
        public string Notes
        {
            get { return m_Notes; }
            set
            {
                if (m_Notes != null)
                    if (!m_Notes.Equals(value))
                        isDirty = true;
                m_Notes = value;
            }
        }

        [XmlIgnore] public bool isDirty = false;

        [XmlIgnore] private string m_DisplayName;
        [XmlIgnore] private bool m_WifiSafe;
        [XmlIgnore] private string m_Notes;

        public GeneralModXML()
        {

        }
    }
    
    public class CharacterAudioSlotSelection
    {
        public int CharacterID { get; set; }
        public int Voice1 { get; set; }
        public int Voice2 { get; set; }
        public int Sound1 { get; set; }
        public int Sound2 { get; set; }

        public CharacterAudioSlotSelection()
        {
            Voice1 = 0;
            Voice2 = 0;
            Sound1 = 0;
            Sound2 = 0;
        }
    }
}
