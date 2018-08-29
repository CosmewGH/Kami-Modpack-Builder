using KamiModpackBuilder.DB;
using KamiModpackBuilder.Globals;
using KamiModpackBuilder.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using KamiModpackBuilder.UserControls;
using ZLibNet;

namespace KamiModpackBuilder
{
    public class SmashProjectManager
    {
        #region Members
        private SmashMod _CurrentProject;
        public Config _Config;
        private ResourceCollection[] _resCols;
        private ResourceCollection _resColDataCore;
        private string _ProjectFilePath = string.Empty;
        private RFManager _RfManager;
        private static SmashProjectManager _Instance;
        #endregion

        #region Properties
        /// <summary>
        /// Object representing the game list of resources (all regions, including core data).
        /// </summary>
        public ResourceCollection[] ResourceDataCollection { get { return _resCols; } }

        /// <summary>
        /// Object representing the game list of resources (only core data).
        /// </summary>
        public ResourceCollection ResourceDataCore { get { return _resColDataCore; } }

        /// <summary>
        /// The current project
        /// </summary>
        public SmashMod CurrentProject { get { return _CurrentProject; } }

        public static SmashProjectManager instance { get { return _Instance; } }
        
        public Explorer _ExplorerPage = null;
        public CharacterMods _CharacterModsPage = null;
        public StageMods _StageModsPage = null;
        public GeneralMods _GeneralModsPage = null;
        public Sm4shMusic.Sm4shMusic _Sm4shMusic = null;
        public Sm4shMusic.UserControls.BGMManagement _BGMManagementPage = null;
        public Sm4shMusic.UserControls.MyMusicManagement _MyMusicPage = null;
        #endregion

        #region Project Management
        internal SmashMod CreateNewProject(string projectFilePath, string gamePath)
        {
            LogHelper.Info(string.Format("Creating a new project: '{0}' with GamePath: '{1}'", projectFilePath, gamePath));

            SmashMod newProject = new SmashMod();
            newProject.GamePath = gamePath;
            _CurrentProject = newProject;
            _ProjectFilePath = projectFilePath;

            PathHelper.InitializePathHelper(newProject);

            //Read meta.xml
            try
            {
                string metaFilePath = PathHelper.GetGameFolder(PathHelperEnum.FILE_META);
                XmlDocument fileMeta = new XmlDocument();
                fileMeta.Load(metaFilePath);
                XmlNodeList nodeGameVersion = fileMeta.DocumentElement.SelectNodes("/menu/title_version");
                XmlNodeList nodeGameRegion = fileMeta.DocumentElement.SelectNodes("/menu/region");
                _CurrentProject.GameVersion = Convert.ToInt32(nodeGameVersion[0].InnerText);
                _CurrentProject.GameRegionID = Convert.ToInt32(nodeGameRegion[0].InnerText);
            }
            catch (Exception e)
            {
                LogHelper.Error(string.Format("Error parsing meta.xml. Kami Modpack Builder will assume that you are using the latest patch ({0}) and that your game region is USA. If it isn't the case please update the config file before doing anything. (error: {1})", GlobalConstants.GAME_LAST_PATH_VERSION, e.Message));
                _CurrentProject.GameVersion = GlobalConstants.GAME_LAST_PATH_VERSION;
                _CurrentProject.GameRegionID = 2;
            }

            SaveProject();

            _Config.LastProject = projectFilePath;
            SaveConfig();

            LoadProjectData();

            return newProject;
        }

        internal void SaveProject()
        {
            if (_RfManager != null)
            {
                _RfManager.Debug = _Config.Debug;
                _RfManager.SkipTrashEntries = _CurrentProject.SkipJunkEntries;
                _RfManager.ForceOriginalFlags = _CurrentProject.KeepOriginalFlags;
            }
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(SmashMod));
                using (TextWriter writer = new StreamWriter(_ProjectFilePath, false))
                    ser.Serialize(writer, _CurrentProject);
                LogHelper.Info("Project saved.");
            }
            catch (Exception e)
            {
                LogHelper.Error(string.Format("Error while serializing and saving the project! {0}", e.Message));
            }
        }

        internal void SaveConfig()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(Config));
                using (TextWriter writer = new StreamWriter(UIConstants.CONFIG_FILE, false))
                    ser.Serialize(writer, _Config);
            }
            catch (Exception e)
            {
                LogHelper.Error(string.Format("Error while serializing and saving the config! {0}", e.Message));
            }
        }

        internal SmashMod LoadProject(string projectPathFile)
        {
            LogHelper.Info(String.Format("Loading project \"{0}\"", projectPathFile));

            XmlSerializer ser = new XmlSerializer(typeof(SmashMod));
            SmashMod loadedProject = null;
            using (StreamReader reader = new StreamReader(projectPathFile))
                loadedProject = (SmashMod)(ser.Deserialize(reader));
            _CurrentProject = loadedProject;
            _ProjectFilePath = projectPathFile;

            _Config.LastProject = _ProjectFilePath;
            SaveConfig();

            LoadProjectData();

            return loadedProject;
        }

        internal Config LoadConfig()
        {
            Config config = null;
            if (File.Exists(UIConstants.CONFIG_FILE))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Config));
                using (StreamReader reader = new StreamReader(UIConstants.CONFIG_FILE))
                    config = (Config)(ser.Deserialize(reader));
                LogHelper.Info(string.Format("Loaded config."));
            }
            else
            {
                config = new Config();
            }

            _Config = config;
            PathHelper._Config = config;
            return config;
        }

        internal void InitializeDBs()
        {
            try
            {
                LogHelper.Debug("Initializing general DBs...");
                StagesDB.InitializeStageDB(_CurrentProject.GameVersion, _CurrentProject.IsSwitch);
                IconsDB.InitializeIconsDB(_CurrentProject.GameVersion, _CurrentProject.IsSwitch);
                CharsDB.InitializeCharsDB(_CurrentProject.GameVersion, _CurrentProject.IsSwitch);
                FightersDB.InitializeFightersDB(_CurrentProject.GameVersion, _CurrentProject.IsSwitch);
            }
            catch(Exception e)
            {
                LogHelper.Error(string.Format("Error while loading the DBs: {0}", e.Message));
            }
        }
        #endregion

        #region Loading Data
        internal bool LoadProjectData()
        {
            LogHelper.Debug("Loading data...");

            LogHelper.InitializeLogHelper(_CurrentProject, _Config);
            PathHelper.InitializePathHelper(_CurrentProject);

            LogHelper.Info(string.Format("Loading from game: {0}", _CurrentProject.GamePath));

            if (string.IsNullOrEmpty(_CurrentProject.ProjectExportFolder) || !Directory.Exists(_CurrentProject.ProjectExportFolder))
                _CurrentProject.ProjectExportFolder = PathHelper.FolderExport;
            if (string.IsNullOrEmpty(_CurrentProject.ProjectExportFolder) || !Directory.Exists(_CurrentProject.ProjectExportFolder))
                _CurrentProject.ProjectExtractFolder = PathHelper.FolderExtract;
            if (string.IsNullOrEmpty(_CurrentProject.ProjectTempFolder) || !Directory.Exists(_CurrentProject.ProjectTempFolder))
                _CurrentProject.ProjectTempFolder = PathHelper.FolderTemp;
            if (string.IsNullOrEmpty(_CurrentProject.ProjectExplorerFolder) || !Directory.Exists(_CurrentProject.ProjectExplorerFolder))
                _CurrentProject.ProjectExplorerFolder = PathHelper.FolderExplorer;
            if (string.IsNullOrEmpty(_CurrentProject.ProjectWorkspaceFolder) || !Directory.Exists(_CurrentProject.ProjectWorkspaceFolder))
                _CurrentProject.ProjectWorkspaceFolder = PathHelper.FolderWorkspace;

            if (!PathHelper.IsItSmashFolder(_CurrentProject.GamePath) || !PathHelper.DoesItHavePatchFolder(_CurrentProject.GamePath))
            {
                LogHelper.Error(string.Format("There seems to be a problem with the game folder: '{0}', check your config file and make sure this directory contains the game and is accessible.", _CurrentProject.GamePath));
                return false;
            }

            //Create temp folder
            Directory.CreateDirectory(PathHelper.FolderTemp);

            _RfManager = new RFManager(PathHelper.GetGameFolder(PathHelperEnum.FILE_LS), PathHelper.GetDTFiles(), PathHelper.FolderTemp);
            _RfManager.Debug = _Config.Debug;
            _RfManager.SkipTrashEntries = _CurrentProject.SkipJunkEntries;
            _RfManager.ForceOriginalFlags = _CurrentProject.KeepOriginalFlags;

            string[] rfFiles = null;
            PatchFileItem[] patchFiles = null;
            if (File.Exists(PathHelper.GetGameFolder(PathHelperEnum.FILE_PATCH_RESOURCE)))
            {
                rfFiles = PathHelper.GetResourceFiles(PathHelper.GetGameFolder(PathHelperEnum.FOLDER_PATCH));
                patchFiles = _RfManager.LoadPatchFile(PathHelper.GetGameFolder(PathHelperEnum.FILE_PATCH_PATCHLIST));
            }
            else
            {
                //LS, todo
                LogHelper.Error(string.Format("Loading the game from LS is not supported yet, sm4shexplorer couldn't find the resource file from the patch: '{0}'", PathHelper.GetGameFolder(PathHelperEnum.FILE_PATCH_RESOURCE)));
                return false;
            }
            
            //Load RF Files
            _resCols = _RfManager.LoadRFFiles(rfFiles);
            _resColDataCore = _resCols.LastOrDefault(p => !p.IsRegion);
            if (_resColDataCore == null)
                LogHelper.Error("Missing core data resource.");

            //Check DT files
            _CurrentProject.DTFilesFound = CheckDTFiles();

            //Init DBS
            InitializeDBs();

            return true;
        }

        public void RefreshTabViews()
        {
            _ExplorerPage.RefreshTreeView();
            _CharacterModsPage.RefreshData();
        }
        #endregion

        #region Extract Data
        #region public methods
        /// <summary>
        /// Extract a resource to the "extract" folder
        /// The path should be "data/folder1/folder2/file"
        /// If you wish to extract a folder, don't forget the "/" at the end
        /// </summary>
        /// <param name="absolutePath">Absolute path</param>
        /// <param name="outputFolder">Output folder</param>
        /// <returns>Path to the file or folder extracted</returns>
        public string ExtractResource(string absolutePath, string outputFolder)
        {
            ResourceCollection resCol = GetResourceCollection(absolutePath);
            string relativePath = GetRelativePath(absolutePath);
            return ExtractResource(resCol, relativePath, outputFolder);
        }

        /// <summary>
        /// Extract a resource to the "extract" folder
        /// The path should be "data/folder1/folder2/file"
        /// If you wish to extract a folder, don't forget the "/" at the end
        /// </summary>
        /// <param name="absolutePath">Absolute path</param>
        /// <returns>Path to the file or folder extracted</returns>
        public string ExtractResource(string absolutePath)
        {
            return ExtractResource(absolutePath, PathHelper.FolderExtract);
        }

        /// <summary>
        /// Extract a resource to the "extract" folder
        /// The path should be "folder1/folder2/file"
        /// If you wish to extract a folder, don't forget the "/" at the end
        /// </summary>
        /// <param name="resCol">Resource Collection</param>
        /// <param name="relativePath">Relative path</param>
        /// <param name="outputFolder">Output folder</param>
        /// <returns>Path to the file or folder extracted</returns>
        public string ExtractResource(ResourceCollection resCol, string relativePath, string outputFolder)
        {
            if (!relativePath.EndsWith("/"))
                return ExtractFile(resCol, relativePath, outputFolder);
            else
                ExtractFolder(resCol, relativePath, outputFolder);
            return string.Empty;
        }

        /// <summary>
        /// Extract a resource to the "extract" folder
        /// The path should be "folder1/folder2/file"
        /// If you wish to extract a folder, don't forget the "/" at the end
        /// </summary>
        /// <param name="resCol">Resource Collection</param>
        /// <param name="relativePath">Relative path</param>
        /// <returns>Path to the file or folder extracted</returns>
        public string ExtractResource(ResourceCollection resCol, string relativePath)
        {
            return ExtractResource(resCol, relativePath, PathHelper.FolderExtract);
        }
        #endregion

        #region private methods
        private string ExtractFile(ResourceCollection resCol, string relativePath, string outputFile, bool isFromFolder)
        {
            //Get absolute path
            string absolutePath = GetAbsolutePath(resCol, relativePath);

            //Get workplace file, in case a mod already exist
            string workplaceFile = GetWorkspaceFileFromPath(absolutePath);

            //Get extract path
            string extractFile = outputFile + Path.DirectorySeparatorChar + absolutePath.Replace('/', Path.DirectorySeparatorChar);

            try
            {
                //If file is present in workplace, a mod already exist.
                if (File.Exists(workplaceFile))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(extractFile));
                    File.Copy(workplaceFile, extractFile, true);

                    if (!isFromFolder)
                        LogHelper.Info(string.Format("Extracting file '{0}' from Mod...", absolutePath));

                    return extractFile;
                }

                //If not, we make sure the path given is correct and exist in the resource collection
                ResourceItem rItem = null;
                if (!resCol.Resources.ContainsKey(relativePath))
                {
                    LogHelper.Error(string.Format("Can't find file '{0}' in resource {1}", absolutePath, resCol.PartitionName));
                    return string.Empty;
                }
                rItem = resCol.Resources[relativePath];

                //Special log for single file extraction
                if (!isFromFolder)
                    LogHelper.Info(string.Format("Extracting file '{0}' from {1}...", absolutePath, rItem.Source.ToString()));

                //Extraction method depending of the source
                switch (rItem.Source)
                {
                    case FileSource.LS:
                        //Make sure the DT files were found, warn the user if they couldnt be found
                        if (!_CurrentProject.DTFilesFound)
                            LogHelper.Error(string.Format("Can't extract '{0}' because the DT file(s) could not be found. Please restart Sm4shExplorer with the DT file(s) in the content folder.", absolutePath));
                        else
                            _RfManager.ExtractFileFromLS(rItem, extractFile);
                        break;
                    case FileSource.Patch:
                        _RfManager.ExtractFileFromPatch(rItem, extractFile);
                        break;
                }
            }
            catch (Exception e)
            {
                LogHelper.Error(string.Format("Error extracting file '{0}': {1}", absolutePath, e.Message));
            }

            if (!isFromFolder)
                _RfManager.ClearCachedDataSources();

            return extractFile;
        }

        private string ExtractFile(ResourceCollection resCol, string relativePath, string outputFolder)
        {
            return ExtractFile(resCol, relativePath, outputFolder, false);
        }

        private string ExtractFolder(ResourceCollection resCol, string relativePath, string outputFolder)
        {
            //Get all the existing resource to extract
            List<string> lResources = FilterRelativePath(resCol.CachedFilteredResources.Keys.ToArray(), relativePath);
            //Get all the existing modded files from workspace
            lResources.AddRange(FilterRelativePath(GetAllWorkplaceRelativePaths(resCol.PartitionName), relativePath));
            //Distinct union
            lResources = lResources.Distinct().ToList();

            LogHelper.Info(string.Format("Extracting {0} files from {1}...", lResources.Count, relativePath));

            foreach (string resource in lResources)
            {
                if(!resource.EndsWith("/"))
                    ExtractFile(resCol, resource, outputFolder, true);
            }
            _RfManager.ClearCachedDataSources();

            LogHelper.Info("Done!");
            
            return outputFolder;
        }
        #endregion
        #endregion

        #region Add/Remove Files to Workspace
        /// <summary>
        /// Add a new file/folder into the workspace
        /// </summary>
        /// <param name="inputFile">File/Folder you want to add</param>
        /// <param name="absolutePath">Absolute game path where the file should be copied in the workspace</param>
        public void AddFileToWorkspace(string inputFile, string absolutePath)
        {
            LogHelper.Info(string.Format("Adding mod file '{0}' to resource '{1}'...", inputFile, absolutePath));

            List<string> lFiles = new List<string>();
            lFiles.Add(inputFile);

            if (!File.Exists(inputFile))
            {
                string[] subFiles = Directory.GetFiles(inputFile, "*", SearchOption.AllDirectories);
                lFiles.AddRange(subFiles);
            }
            LogHelper.Debug(string.Format("{0} files to add.", lFiles.Count));

            //Copy
            string baseToExclude = lFiles[0].Substring(0, lFiles[0].LastIndexOf(Path.DirectorySeparatorChar));
            foreach (string fileToProcess in lFiles)
            {
                if (!Utils.IsAnAcceptedExtension(fileToProcess) || fileToProcess.EndsWith("packed"))
                    LogHelper.Error(string.Format("The file '{0}' has a forbidden extension, skipping...", fileToProcess));
                else
                {
                    string newFile = GetWorkspaceFileFromPath(absolutePath) + fileToProcess.Replace(baseToExclude, string.Empty);

                    FileAttributes pathAttrs = File.GetAttributes(fileToProcess);
                    if (!pathAttrs.HasFlag(FileAttributes.Directory))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(newFile));
                        File.Copy(fileToProcess, newFile, true);
                    }
                }
            }

            LogHelper.Info("Done!");
        }

        /// <summary>
        /// Remove a file/folder from the workspace
        /// </summary>
        /// <param name="absolutePath">Absolute game path of the file/folder that must be removed</param>
        /// <returns>true if the file/folder is deleted, otherwise false</returns>
        public bool RemoveFileFromWorkspace(string absolutePath)
        {
            LogHelper.Info(string.Format("Removing mod files from '{0}'...", absolutePath));

            string pathToDelete = GetWorkspaceFileFromPath(absolutePath);

            try
            {
                if (File.Exists(pathToDelete))
                    File.Delete(pathToDelete);
                else if (Directory.Exists(pathToDelete))
                    Directory.Delete(pathToDelete, true);
                else
                    return false;
            }
            catch(Exception e)
            {
                LogHelper.Error(string.Format("Error while deleting '{0}': {1}", pathToDelete, e.Message));
                return false;
            }

            LogHelper.Info("Done!");

            return true;
        }
        #endregion

        #region Unlocalizing Files && Resource Removal
        #region internal methods
        internal bool UnlocalizePath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return false;
            string relativePath = GetRelativePath(absolutePath);
            ResourceCollection resCol = GetResourceCollection(absolutePath);
            return UnlocalizePath(resCol, relativePath);
        }

        internal bool UnlocalizePath(ResourceCollection resCol, string relativePath)
        {
            if (resCol == null || !resCol.IsRegion)
                return false;

            //Get list of files to unlocalize
            List<ResourceItem> lItems = GetResources(resCol, relativePath);
            ResourceCollection dataCol = _resColDataCore;

            LogHelper.Info(string.Format("Unlocalizing {0} resources from {1}...", lItems.Count, relativePath));

            foreach (ResourceItem uItem in lItems)
            {
                if (!dataCol.Resources.ContainsKey(uItem.RelativePath))
                {
                    LogHelper.Warning(string.Format("The resource '{0}' does not exist in the data partition! Skipping it but it might cause an issue.", uItem.RelativePath));
                    continue;
                }

                ResourceItem packedItem = GetPackedPath(uItem.ResourceCollection, uItem.RelativePath);
                if (packedItem != null)
                    _CurrentProject.AddUnlocalized(resCol.PartitionName, packedItem.RelativePath + "packed");
                else
                    _CurrentProject.AddUnlocalized(resCol.PartitionName, uItem.RelativePath);
            }

            SaveProject();

            LogHelper.Info("Done!");

            return true;
        }

        internal bool RemoveUnlocalized(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return false;
            string relativePath = GetRelativePath(absolutePath);
            ResourceCollection resCol = GetResourceCollection(absolutePath);
            return RemoveUnlocalized(resCol, relativePath);
        }

        internal bool RemoveUnlocalized(ResourceCollection resCol, string relativePath)
        {
            if (resCol == null || !resCol.IsRegion)
                return false;

            LogHelper.Info(string.Format("Removing unlocalization for {0}...", relativePath));

            string[] paths = resCol.Resources.Keys.ToArray();
            foreach (string path in paths)
            {
                if (path.StartsWith(relativePath))
                {
                    ResourceItem dItem = resCol.Resources[path];
                    if (dItem.IsAPackage && dItem.IsFolder)
                        CurrentProject.RemoveUnlocalized(resCol.PartitionName, path + "packed");
                    else
                        CurrentProject.RemoveUnlocalized(resCol.PartitionName, path);
                }
            }

            SaveProject();

            LogHelper.Info("Done!");

            return true;
        }


        internal bool RemoveOriginalResource(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return false;
            string relativePath = GetRelativePath(absolutePath);
            ResourceCollection resCol = GetResourceCollection(absolutePath);
            return UnlocalizePath(resCol, relativePath);
        }

        internal bool RemoveOriginalResource(ResourceCollection resCol, string relativePath)
        {
            if (resCol == null)
                return false;

            //Get list of files to unlocalize
            List<ResourceItem> lItems = GetResources(resCol, relativePath);
            ResourceCollection dataCol = _resColDataCore;

            LogHelper.Info(string.Format("Removing {0} original resources from {1}...", lItems.Count, relativePath));

            foreach (ResourceItem uItem in lItems)
                _CurrentProject.RemoveOriginalResource(resCol.PartitionName, uItem.RelativePath);

            SaveProject();

            LogHelper.Info("Done!");

            return true;
        }

        internal bool ReintroduceOriginalResource(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return false;
            string relativePath = GetRelativePath(absolutePath);
            ResourceCollection resCol = GetResourceCollection(absolutePath);
            return ReintroduceOriginalResource(resCol, relativePath);
        }

        internal bool ReintroduceOriginalResource(ResourceCollection resCol, string relativePath)
        {
            if (resCol == null)
                return false;

            LogHelper.Info(string.Format("Reintroducing original resource for {0}...", relativePath));

            string[] paths = resCol.Resources.Keys.ToArray();
            foreach (string path in paths)
            {
                if (path.StartsWith(relativePath))
                {
                    ResourceItem dItem = resCol.Resources[path];
                    if (dItem.IsAPackage && dItem.IsFolder)
                        CurrentProject.ReintroduceOriginalResource(resCol.PartitionName, path + "packed");
                    else
                        CurrentProject.ReintroduceOriginalResource(resCol.PartitionName, path);
                }
            }

            SaveProject();

            LogHelper.Info("Done!");

            return true;
        }
        #endregion
        #endregion

        #region Rebuilding Files
        #region internal methods
        internal void RebuildRFAndPatchlist()
        {
            RebuildRFAndPatchlist(true);
        }

        internal void AddFileToResColFileLists(string fakeFilename, string realFilename, string[] baseFolders, SortedList<string,string>[] lists)
        {
            for (int i = 0; i < lists.Count(); ++i)
            {
                if (!fakeFilename.Contains(baseFolders[i])) continue;
                if (!lists[i].ContainsKey(fakeFilename))
                {
                    if (!Utils.IsAnAcceptedExtension(realFilename))
                        LogHelper.Error(string.Format("The file '{0}' has a forbidden extension, skipping...", realFilename));
                    else
                        lists[i].Add(fakeFilename, realFilename);
                }
                string directoryFake = Path.GetDirectoryName(fakeFilename);
                if (!lists[i].ContainsKey(directoryFake))
                {
                    string directoryReal = Path.GetDirectoryName(realFilename);
                    if (!Utils.IsAnAcceptedExtension(directoryReal))
                        LogHelper.Error(string.Format("The file '{0}' has a forbidden extension, skipping...", directoryReal));
                    else
                        lists[i].Add(directoryFake, directoryReal);
                }
                return;
            }
        }

        internal void RebuildRFAndPatchlist(bool packing)
        {
            LogHelper.Info("----------------------------------------------------------------");
            LogHelper.Info(string.Format("Starting compilation of the mod ({0})", (packing ? "release" : "debug")));

            string exportFolder = PathHelper.FolderExport + (packing ? "release" : "debug") + Path.DirectorySeparatorChar + (_CurrentProject.ExportWithDateFolder ? string.Format("{0:yyyyMMdd-HHmmss}", DateTime.Now) + Path.DirectorySeparatorChar : string.Empty);

            try
            {
                if (Directory.Exists(exportFolder))
                {
                    foreach(string folder in Directory.GetDirectories(exportFolder))
                        Directory.Delete(folder, true);
                    foreach (string file in Directory.GetFiles(exportFolder))
                        File.Delete(file);
                }
            }
            catch
            {
                LogHelper.Error(string.Format("Error deleting '{0}', please delete it manually before attempting to build the mod again.", exportFolder));
            }

            LogHelper.Debug(string.Format("Export folder: '{0}'", exportFolder));
            string exportPatchFolder = exportFolder + "content" + Path.DirectorySeparatorChar + "patch" + Path.DirectorySeparatorChar;

            if (Directory.Exists(exportPatchFolder))
                Directory.Delete(exportPatchFolder, true);

            CharacterDBParam CharDB = new CharacterDBParam(this);
            CharacterStringsMSBT charStringsMSBD = new CharacterStringsMSBT(this);

            string[] baseFolders = new string[_resCols.Count()];
            SortedList<string, string>[] filesLists = new SortedList<string, string>[_resCols.Count()];

            for (int i = 0; i < _resCols.Count(); ++i)
            {
                baseFolders[i] = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH, _resCols[i].PartitionName);
                filesLists[i] = new SortedList<string, string>();
            }
            
            List<string> explorerFiles = new List<string>();
            List<string> fileSearch = new List<string>();
            List<AutoMTBFix.Nus3BankFile> nus3Banks = new List<AutoMTBFix.Nus3BankFile>();

            #region Character Slot Mods
            List<FighterName> fighterNames = new List<FighterName>();
            foreach (CharacterSlotMod mod in _CurrentProject.ActiveCharacterSlotMods)
            {
                DB.Fighter currentFighter = DB.FightersDB.GetFighterFromID(mod.CharacterID);
                string fighterName = currentFighter.name;
                CharacterSlotModXML xml = Utils.OpenCharacterSlotKamiModFile(fighterName, mod.FolderName);
                if (xml == null)
                {
                    LogHelper.Error(string.Format("Mod '{0}' missing, skipping...", mod.FolderName));
                    continue;
                }
                if (_CurrentProject.BuildIsWifiSafe && !xml.WifiSafe)
                {
                    LogHelper.Info(string.Format("Mod '{0}' is not Wifi-Safe, skipping...", mod.FolderName));
                    continue;
                }
                if (_CurrentProject.BuildIsWifiSafe && mod.SlotID >= currentFighter.defaultSlots)
                {
                    LogHelper.Info(string.Format("Mod '{0}' is on an extra slot and therefore not Wifi-Safe, skipping...", mod.FolderName));
                    continue;
                }
                if (_CurrentProject.BuildSafetySetting > 0)
                {
                    switch (_CurrentProject.BuildSafetySetting)
                    {
                        case 1:
                            if (xml.MetalModel == CharacterSlotModXML.MetalModelStatus.Crashes)
                            {
                                LogHelper.Info(string.Format("Mod '{0}' has a crashing metal model, skipping...", mod.FolderName));
                                continue;
                            }
                            break;
                        case 2:
                            if (xml.MetalModel != CharacterSlotModXML.MetalModelStatus.Works)
                            {
                                LogHelper.Info(string.Format("The metal model of mod '{0}' is not confirmed to be working, skipping...", mod.FolderName));
                                continue;
                            }
                            break;
                    }
                }

                CharDB.SetCharacterSlotCount(currentFighter.id, mod.SlotID+1);

                string modFolder = PathHelper.GetCharacterSlotModPath(fighterName, mod.FolderName);
                string explorerFolder = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH);
                string explorerFighterFolder = explorerFolder + "data" + Path.DirectorySeparatorChar + "fighter" + Path.DirectorySeparatorChar + fighterName.ToLower() + Path.DirectorySeparatorChar;
                string explorerChrFolder = explorerFolder + "data" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "replace" + Path.DirectorySeparatorChar + "chr" + Path.DirectorySeparatorChar;
                string explorerChrDlcFolder = explorerFolder + "data" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "replace" + Path.DirectorySeparatorChar + "append" + Path.DirectorySeparatorChar + "chr" + Path.DirectorySeparatorChar;

                #region Models
                if (Directory.Exists(modFolder + "model"))
                {
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "model", "*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "model" + Path.DirectorySeparatorChar, string.Empty);
                        if (explorerFilename.Contains("lxx"))
                        {
                            explorerFilename = "body" + Path.DirectorySeparatorChar + explorerFilename;
                            explorerFilename = explorerFilename.Replace("lxx", "l" + mod.SlotID.ToString("D2"));
                            explorerFilename = explorerFighterFolder + "model" + Path.DirectorySeparatorChar + explorerFilename;
                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        }
                        else if(explorerFilename.Contains("body"))
                        {
                            string[] split = explorerFilename.Split(Path.DirectorySeparatorChar);
                            string slotNum = mod.SlotID.ToString("D2");
                            explorerFilename = split.First() + Path.DirectorySeparatorChar + "c" + slotNum + explorerFilename.Remove(0, split.First().Length);
                            explorerFilename = explorerFighterFolder + "model" + Path.DirectorySeparatorChar + explorerFilename;
                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                            if (_CurrentProject.OverrideLowPolyModels)
                            {
                                if (mod.SlotID < currentFighter.defaultSlots)
                                {
                                    if (!xml.Haslxx && currentFighter.lowPolySlots != Fighter.LowPolySlots.None)
                                    {
                                        {
                                            if ((currentFighter.lowPolySlots == Fighter.LowPolySlots.All) ||
                                                (currentFighter.lowPolySlots == Fighter.LowPolySlots.EvenSlots && mod.SlotID % 2 == 1) ||
                                                (currentFighter.lowPolySlots == Fighter.LowPolySlots.OddSlots && mod.SlotID % 2 == 0))
                                                explorerFilename = explorerFilename.Replace("c" + slotNum, "l" + slotNum);
                                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            string[] split = explorerFilename.Split(Path.DirectorySeparatorChar);
                            string slotNum = mod.SlotID.ToString("D2");
                            explorerFilename = split.First() + Path.DirectorySeparatorChar + "c" + slotNum + explorerFilename.Remove(0, split.First().Length);
                            explorerFilename = explorerFighterFolder + "model" + Path.DirectorySeparatorChar + explorerFilename;
                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        }
                    }
                    fileSearch.Clear();
                }
                #endregion
                #region Sounds
                if (xml.Sound)
                {
                    if (currentFighter.soundPackSlots == Fighter.SoundPackSlots.All)
                    {
                        fileSearch.AddRange(Directory.GetFiles(modFolder + "sound", "*snd_se_*", SearchOption.AllDirectories));
                        if (fileSearch.Count > 0)
                        {
                            string explorerFilename = fileSearch[0].Replace(modFolder + "sound" + Path.DirectorySeparatorChar, string.Empty);
                            if (mod.SlotID > 0) explorerFilename = explorerFilename.Replace("_cxx", "_c" + mod.SlotID.ToString("D2"));
                            else explorerFilename = explorerFilename.Replace("_cxx", "");
                            explorerFilename = explorerFighterFolder + "sound" + Path.DirectorySeparatorChar + explorerFilename;
                            AddFileToResColFileLists(explorerFilename, fileSearch[0], baseFolders, filesLists);
                            nus3Banks.Add(new AutoMTBFix.Nus3BankFile { fighterName = currentFighter.name, fileName = fileSearch[0], cxxNum = (ushort)mod.SlotID });
                        }
                    }
                    else
                    {
                        if (_CurrentProject.GetAudioSlotForFighter(currentFighter.id, false, 0) == mod.SlotID)
                        {
                            fileSearch.AddRange(Directory.GetFiles(modFolder + "sound", "*snd_se_*", SearchOption.AllDirectories));
                            if (fileSearch.Count > 0)
                            {
                                string explorerFilename = fileSearch[0].Replace(modFolder + "sound" + Path.DirectorySeparatorChar, string.Empty);
                                explorerFilename = explorerFilename.Replace("_cxx", "");
                                explorerFilename = explorerFighterFolder + "sound" + Path.DirectorySeparatorChar + explorerFilename;
                                AddFileToResColFileLists(explorerFilename, fileSearch[0], baseFolders, filesLists);
                                nus3Banks.Add(new AutoMTBFix.Nus3BankFile { fighterName = currentFighter.name, fileName = fileSearch[0], cxxNum = 0 });
                            }
                        }
                        else if (currentFighter.soundPackSlots == Fighter.SoundPackSlots.Two && _CurrentProject.GetAudioSlotForFighter(currentFighter.id, false, 1) == mod.SlotID)
                        {
                            fileSearch.AddRange(Directory.GetFiles(modFolder + "sound", "*snd_se_*", SearchOption.AllDirectories));
                            if (fileSearch.Count > 0)
                            {
                                string explorerFilename = fileSearch[0].Replace(modFolder + "sound" + Path.DirectorySeparatorChar, string.Empty);
                                explorerFilename = explorerFilename.Replace("_cxx", "_c01");
                                explorerFilename = explorerFighterFolder + "sound" + Path.DirectorySeparatorChar + explorerFilename;
                                AddFileToResColFileLists(explorerFilename, fileSearch[0], baseFolders, filesLists);
                                nus3Banks.Add(new AutoMTBFix.Nus3BankFile { fighterName = currentFighter.name, fileName = fileSearch[0], cxxNum = 1 });
                            }
                        }
                    }
                    fileSearch.Clear();
                }
                if (xml.Voice)
                {
                    if (currentFighter.voicePackSlots == Fighter.VoicePackSlots.All)
                    {
                        fileSearch.AddRange(Directory.GetFiles(modFolder + "sound", "*snd_vc_*", SearchOption.AllDirectories));
                        if (fileSearch.Count > 0)
                        {
                            string explorerFilename = fileSearch[0].Replace(modFolder + "sound" + Path.DirectorySeparatorChar, string.Empty);
                            if (mod.SlotID > 0) explorerFilename = explorerFilename.Replace("_cxx", "_c" + mod.SlotID.ToString("D2"));
                            else explorerFilename = explorerFilename.Replace("_cxx", "");
                            explorerFilename = explorerFighterFolder + "sound" + Path.DirectorySeparatorChar + explorerFilename;
                            AddFileToResColFileLists(explorerFilename, fileSearch[0], baseFolders, filesLists);
                            nus3Banks.Add(new AutoMTBFix.Nus3BankFile { fighterName = currentFighter.name, fileName = fileSearch[0], cxxNum = (ushort)mod.SlotID });
                        }
                    }
                    else
                    {
                        if (_CurrentProject.GetAudioSlotForFighter(currentFighter.id, true, 0) == mod.SlotID)
                        {
                            fileSearch.AddRange(Directory.GetFiles(modFolder + "sound", "*snd_vc_*", SearchOption.AllDirectories));
                            if (fileSearch.Count > 0)
                            {
                                string explorerFilename = fileSearch[0].Replace(modFolder + "sound" + Path.DirectorySeparatorChar, string.Empty);
                                explorerFilename = explorerFilename.Replace("_cxx", "");
                                explorerFilename = explorerFighterFolder + "sound" + Path.DirectorySeparatorChar + explorerFilename;
                                AddFileToResColFileLists(explorerFilename, fileSearch[0], baseFolders, filesLists);
                                nus3Banks.Add(new AutoMTBFix.Nus3BankFile { fighterName = currentFighter.name, fileName = fileSearch[0], cxxNum = 0 });
                            }
                        }
                        else if (currentFighter.soundPackSlots == Fighter.SoundPackSlots.Two && _CurrentProject.GetAudioSlotForFighter(currentFighter.id, true, 1) == mod.SlotID)
                        {
                            fileSearch.AddRange(Directory.GetFiles(modFolder + "sound", "*snd_vc_*", SearchOption.AllDirectories));
                            if (fileSearch.Count > 0)
                            {
                                string explorerFilename = fileSearch[0].Replace(modFolder + "sound" + Path.DirectorySeparatorChar, string.Empty);
                                explorerFilename = explorerFilename.Replace("_cxx", "_c01");
                                explorerFilename = explorerFighterFolder + "sound" + Path.DirectorySeparatorChar + explorerFilename;
                                AddFileToResColFileLists(explorerFilename, fileSearch[0], baseFolders, filesLists);
                                nus3Banks.Add(new AutoMTBFix.Nus3BankFile { fighterName = currentFighter.name, fileName = fileSearch[0], cxxNum = 1 });
                            }
                        }
                    }
                    fileSearch.Clear();
                }
                #endregion
                #region Chr
                if (xml.chr_00 || xml.chr_11 || xml.chr_13 || xml.stock_90)
                {
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "chr", "*chr_00*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "chr" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerFilename.Replace("XX", (mod.SlotID + 1).ToString("D2"));
                        explorerFilename = explorerChrFolder + "chr_00" + Path.DirectorySeparatorChar + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        if (currentFighter.isDLC)
                        {
                            explorerFilename = explorerFilename.Replace(explorerChrFolder, explorerChrDlcFolder);
                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        }
                    }
                    fileSearch.Clear();
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "chr", "*chr_11*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "chr" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerFilename.Replace("XX", (mod.SlotID + 1).ToString("D2"));
                        explorerFilename = explorerChrFolder + "chr_11" + Path.DirectorySeparatorChar + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        if (currentFighter.isDLC)
                        {
                            explorerFilename = explorerFilename.Replace(explorerChrFolder, explorerChrDlcFolder);
                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        }
                    }
                    fileSearch.Clear();
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "chr", "*chr_13*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "chr" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerFilename.Replace("XX", (mod.SlotID + 1).ToString("D2"));
                        explorerFilename = explorerChrFolder + "chr_13" + Path.DirectorySeparatorChar + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        if (currentFighter.isDLC)
                        {
                            explorerFilename = explorerFilename.Replace(explorerChrFolder, explorerChrDlcFolder);
                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        }
                    }
                    fileSearch.Clear();
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "chr", "*stock_90*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "chr" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerFilename.Replace("XX", (mod.SlotID + 1).ToString("D2"));
                        explorerFilename = explorerChrFolder + "stock_90" + Path.DirectorySeparatorChar + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        if (currentFighter.isDLC)
                        {
                            explorerFilename = explorerFilename.Replace(explorerChrFolder, explorerChrDlcFolder);
                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        }
                    }
                    fileSearch.Clear();
                }
                #endregion
                #region Nameplate
                if (xml.UseCustomName)
                {
                    bool found = false;
                    for (int i = 0; i < fighterNames.Count; ++i)
                    {
                        if (fighterNames[i].CharID == currentFighter.id)
                        {
                            if (fighterNames[i].name.Equals(xml.CharacterName))
                            {
                                found = true;
                                CharDB.SetCharacterSlotNameIndex(currentFighter.id, mod.SlotID, fighterNames[i].nameplateSlot+1);
                            }
                        }
                    }
                    for (int i = 0; i < currentFighter.nameplateSlots.Count; ++i)
                    {
                        if (currentFighter.nameplateSlots[i].Equals(xml.CharacterName))
                        {
                            found = true;
                            CharDB.SetCharacterSlotNameIndex(currentFighter.id, mod.SlotID, i + 1);
                        }
                    }
                    if (!found)
                    {
                        int highestNameSlot = currentFighter.defaultNameplateSlots - 1;

                        for (int i = 0; i < fighterNames.Count; ++i)
                        {
                            if (fighterNames[i].CharID == currentFighter.id)
                            {
                                if (fighterNames[i].nameplateSlot > highestNameSlot) highestNameSlot = fighterNames[i].nameplateSlot;
                            }
                        }
                        ++highestNameSlot;
                        FighterName newName = new FighterName { name = xml.CharacterName, BoxingText = xml.BoxingRingText, CharID = currentFighter.id, nameplateSlot = highestNameSlot };
                        fighterNames.Add(newName);
                        CharDB.SetCharacterSlotNameIndex(currentFighter.id, mod.SlotID, newName.nameplateSlot+1);
                        charStringsMSBD.AddNewCharEntry(currentFighter.name, newName.nameplateSlot + 1, xml.CharacterName, xml.BoxingRingText);

                        if (xml.chrn_11)
                        {
                            fileSearch.AddRange(Directory.GetFiles(modFolder + "chr", "*chrn_11*", SearchOption.AllDirectories));
                            foreach (string f in fileSearch)
                            {
                                string explorerFilename = f.Replace(modFolder + "chr" + Path.DirectorySeparatorChar, string.Empty);
                                explorerFilename = explorerFilename.Replace("XX", (highestNameSlot + 1).ToString("D2"));
                                explorerFilename = explorerChrFolder + "chrn_11" + Path.DirectorySeparatorChar + explorerFilename;
                                AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                                if (currentFighter.isDLC)
                                {
                                    explorerFilename = explorerFilename.Replace(explorerChrFolder, explorerChrDlcFolder);
                                    AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                                }
                            }
                        }
                        fileSearch.Clear();
                    }
                }
                #endregion
            }
            fileSearch.Clear();
            CharDB.SaveFiles();
            charStringsMSBD.SaveFile();
            AutoMTBFix mtb = new AutoMTBFix(nus3Banks, this);
            mtb.Run();
            //TODO: DO MTB Fix with the nus3banks dictionary
            #endregion
            #region Character General Mods
            foreach (CharacterGeneralMod mod in _CurrentProject.ActiveCharacterGeneralMods)
            {
                DB.Fighter currentFighter = DB.FightersDB.GetFighterFromID(mod.CharacterID);
                string fighterName = currentFighter.name;
                CharacterGeneralModXML xml = Utils.OpenCharacterGeneralKamiModFile(fighterName, mod.FolderName);
                if (xml == null)
                {
                    LogHelper.Error(string.Format("Mod '{0}' missing, skipping...", mod.FolderName));
                    continue;
                }
                if (_CurrentProject.BuildIsWifiSafe && !xml.WifiSafe)
                {
                    LogHelper.Info(string.Format("Mod '{0}' is not Wifi-Safe, skipping...", mod.FolderName));
                    continue;
                }
                string modFolder = PathHelper.GetCharacterGeneralModPath(fighterName, mod.FolderName);
                string explorerFolder = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH);
                string explorerFighterFolder = explorerFolder + "data" + Path.DirectorySeparatorChar + "fighter" + Path.DirectorySeparatorChar + fighterName.ToLower() + Path.DirectorySeparatorChar;

                if (Directory.Exists(modFolder + "model")) fileSearch.AddRange(Directory.GetFiles(modFolder + "model", "*", SearchOption.AllDirectories));
                if (Directory.Exists(modFolder + "effect")) fileSearch.AddRange(Directory.GetFiles(modFolder + "effect", "*", SearchOption.AllDirectories));
                if (Directory.Exists(modFolder + "motion")) fileSearch.AddRange(Directory.GetFiles(modFolder + "motion", "*", SearchOption.AllDirectories));
                if (Directory.Exists(modFolder + "script")) fileSearch.AddRange(Directory.GetFiles(modFolder + "script", "*", SearchOption.AllDirectories));
                if (Directory.Exists(modFolder + "sound")) fileSearch.AddRange(Directory.GetFiles(modFolder + "sound", "*", SearchOption.AllDirectories));
                if (Directory.Exists(modFolder + "camera")) fileSearch.AddRange(Directory.GetFiles(modFolder + "camera", "*", SearchOption.AllDirectories));
                foreach (string f in fileSearch)
                {
                    string explorerFilename = f.Replace(modFolder, string.Empty);
                    explorerFilename = explorerFighterFolder + explorerFilename;
                    AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                }
                fileSearch.Clear();
            }
            fileSearch.Clear();
            #endregion
            #region Stage Mods
            foreach (StageSlotMod mod in _CurrentProject.ActiveStageMods)
            {
                DB.Stage currentStage = DB.StagesDB.GetStageFromID(mod.StageID);
                string stageName = currentStage.Label;
                string stagePath = (currentStage.Type == StageType.Melee ? "melee" : "end") + Path.DirectorySeparatorChar + stageName + Path.DirectorySeparatorChar;
                StageModXML xml = Utils.OpenStageKamiModFile(mod.FolderName);
                if (xml == null)
                {
                    LogHelper.Error(string.Format("Mod '{0}' missing, skipping...", mod.FolderName));
                    continue;
                }
                if (_CurrentProject.BuildIsWifiSafe && !xml.WifiSafe)
                {
                    LogHelper.Info(string.Format("Mod '{0}' is not Wifi-Safe, skipping...", mod.FolderName));
                    continue;
                }
                string modFolder = PathHelper.GetStageModPath(mod.FolderName);
                string explorerFolder = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH);
                string explorerStageFolder = explorerFolder + "data" + Path.DirectorySeparatorChar + "stage" + Path.DirectorySeparatorChar + stagePath;
                string explorerStageUIFolder = explorerFolder + "data" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "replace" + Path.DirectorySeparatorChar + "stage" + Path.DirectorySeparatorChar;

                #region Stage Data
                if (Directory.Exists(modFolder + "stage"))
                {
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "stage", "*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "stage" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerStageFolder + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                    }
                    fileSearch.Clear();
                }
                #endregion
                #region UI
                if (Directory.Exists(modFolder + "ui"))
                {
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "ui", "*stage_10*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "ui" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerFilename.Replace("XX", stageName);
                        explorerFilename = explorerStageUIFolder + "stage_10" + Path.DirectorySeparatorChar + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                    }
                    fileSearch.Clear();
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "ui", "*stage_11*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "ui" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerFilename.Replace("XX", stageName);
                        explorerFilename = explorerStageUIFolder + "stage_11" + Path.DirectorySeparatorChar + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                    }
                    fileSearch.Clear();
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "ui", "*stage_12*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "ui" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerFilename.Replace("XX", stageName);
                        explorerFilename = explorerStageUIFolder + "stage_12" + Path.DirectorySeparatorChar + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                    }
                    fileSearch.Clear();
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "ui", "*stage_30*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "ui" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerFilename.Replace("XX", stageName);
                        explorerFilename = explorerStageUIFolder + "stage_30" + Path.DirectorySeparatorChar + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                    }
                    fileSearch.Clear();
                    fileSearch.AddRange(Directory.GetFiles(modFolder + "ui", "*stagen_10*", SearchOption.AllDirectories));
                    foreach (string f in fileSearch)
                    {
                        string explorerFilename = f.Replace(modFolder + "ui" + Path.DirectorySeparatorChar, string.Empty);
                        explorerFilename = explorerFilename.Replace("XX", stageName);
                        explorerFilename = explorerStageUIFolder + "stagen_10" + Path.DirectorySeparatorChar + explorerFilename;
                        AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                    }
                    fileSearch.Clear();
                }
                #endregion

            }
            fileSearch.Clear();
            #endregion
            #region General Mods
            foreach (string mod in _CurrentProject.ActiveGeneralMods)
            {
                GeneralModXML xml = Utils.OpenGeneralKamiModFile(mod);
                if (xml == null)
                {
                    LogHelper.Error(string.Format("Mod '{0}' missing, skipping...", mod));
                    continue;
                }
                if (_CurrentProject.BuildIsWifiSafe && !xml.WifiSafe)
                {
                    LogHelper.Info(string.Format("Mod '{0}' is not Wifi-Safe, skipping...", mod));
                    continue;
                }
                string modFolder = PathHelper.GetGeneralModPath(mod);
                string explorerFolder = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH);

                if (Directory.Exists(modFolder + "data")) fileSearch.AddRange(Directory.GetFiles(modFolder + "data", "*", SearchOption.AllDirectories));
                if (Directory.Exists(modFolder + "data(us_en)")) fileSearch.AddRange(Directory.GetFiles(modFolder + "data(us_en)", "*", SearchOption.AllDirectories));
                if (Directory.Exists(modFolder + "data(us_fr)")) fileSearch.AddRange(Directory.GetFiles(modFolder + "data(us_fr)", "*", SearchOption.AllDirectories));
                if (Directory.Exists(modFolder + "data(us_sp)")) fileSearch.AddRange(Directory.GetFiles(modFolder + "data(us_sp)", "*", SearchOption.AllDirectories));
                foreach (string f in fileSearch)
                {
                    string explorerFilename = f.Replace(modFolder, string.Empty);
                    explorerFilename = explorerFolder + explorerFilename;
                    AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                }
                fileSearch.Clear();
            }
            #endregion
            #region Editor Mods
            if (_CurrentProject.EditorMTBFix)
            {
                fileSearch.Add(PathHelper.FolderEditorMods + "data" + Path.DirectorySeparatorChar + "sound" + Path.DirectorySeparatorChar + "config" + Path.DirectorySeparatorChar + "fightermodelbanktable.mtb");
            }
            if (_CurrentProject.EditorMusicActive)
            {
                fileSearch.Add(PathHelper.FolderEditorMods + "data" + Path.DirectorySeparatorChar + "param" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "ui_sound_db.bin");
                fileSearch.Add(PathHelper.FolderEditorMods + "data(us_en)" + Path.DirectorySeparatorChar + "param" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "ui_sound_db.bin");
                fileSearch.Add(PathHelper.FolderEditorMods + "data(us_fr)" + Path.DirectorySeparatorChar + "param" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "ui_sound_db.bin");
                fileSearch.Add(PathHelper.FolderEditorMods + "data(us_sp)" + Path.DirectorySeparatorChar + "param" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "ui_sound_db.bin");
                fileSearch.Add(PathHelper.FolderEditorMods + "data" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "message" + Path.DirectorySeparatorChar + "sound.msbt");
                fileSearch.Add(PathHelper.FolderEditorMods + "data(us_fr)" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "message" + Path.DirectorySeparatorChar + "sound.msbt");
                fileSearch.Add(PathHelper.FolderEditorMods + "data(us_sp)" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "message" + Path.DirectorySeparatorChar + "sound.msbt");
                fileSearch.Add(PathHelper.FolderEditorMods + "data" + Path.DirectorySeparatorChar + "sound" + Path.DirectorySeparatorChar + "config" + Path.DirectorySeparatorChar + "bgm_mymusic.mmb");
                fileSearch.Add(PathHelper.FolderEditorMods + "data" + Path.DirectorySeparatorChar + "sound" + Path.DirectorySeparatorChar + "config" + Path.DirectorySeparatorChar + "bgm_property.mpb");
            }
            if (_CurrentProject.EditorCharacterMenuDBActive)
            {
                fileSearch.Add(PathHelper.FolderEditorMods + "data" + Path.DirectorySeparatorChar + "param" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "ui_character_db.bin");
                fileSearch.Add(PathHelper.FolderEditorMods + "data(us_en)" + Path.DirectorySeparatorChar + "param" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "ui_character_db.bin");
                fileSearch.Add(PathHelper.FolderEditorMods + "data(us_fr)" + Path.DirectorySeparatorChar + "param" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "ui_character_db.bin");
                fileSearch.Add(PathHelper.FolderEditorMods + "data(us_sp)" + Path.DirectorySeparatorChar + "param" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "ui_character_db.bin");
            }
            if (_CurrentProject.EditorCharacterStringsActive)
            {
                fileSearch.Add(PathHelper.FolderEditorMods + "data" + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "message" + Path.DirectorySeparatorChar + "melee.msbt");
            }
            foreach (string f in fileSearch)
            {
                if (!File.Exists(f)) continue;
                string explorerFilename = f.Replace(PathHelper.FolderEditorMods, string.Empty);
                explorerFilename = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH) + explorerFilename;
                AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
            }
            fileSearch.Clear();
            if (_CurrentProject.YoshiFixActive)
            {
                bool found = true;
                if (!Directory.Exists(PathHelper.FolderWorkspace + "YoshiFix"))
                {
                    if (File.Exists(PathHelper.GetApplicationDirectory() + "tools" + Path.DirectorySeparatorChar + "Yoshi Fix.zip"))
                    {
                        UnZipper unZipper = new UnZipper();
                        unZipper.ZipFile = PathHelper.GetApplicationDirectory() + "tools" + Path.DirectorySeparatorChar + "Yoshi Fix.zip";
                        unZipper.ItemList.Add("*.*");
                        unZipper.Destination = PathHelper.FolderWorkspace + "YoshiFix" + Path.DirectorySeparatorChar;
                        unZipper.Recurse = true;
                        unZipper.UnZip();
                        LogHelper.Debug("Unzipping file...");
                    }
                    else
                    {
                        found = false;
                        LogHelper.Error("Yoshi Fix files not found! Please re-download Kami Modpack Loader and replace 'Yoshi Fix.zip' in the tools folder of the application!");
                    }
                }
                if (found)
                {
                    int slots = CharDB.GetCharacterSlotCount(_CurrentProject.IsSwitch ? 0x07 : 0x07);
                    if (slots > 8)
                    {
                        fileSearch.Add(PathHelper.FolderWorkspace + "YoshiFix" + Path.DirectorySeparatorChar + "effect" + Path.DirectorySeparatorChar + "yoshi" + Path.DirectorySeparatorChar + "yoshi.010d0000.ptcl");
                        for (int i = 8; i < slots; ++i)
                        {
                            fileSearch.AddRange(Directory.GetFiles(PathHelper.FolderWorkspace + "YoshiFix" + Path.DirectorySeparatorChar + "effect" + Path.DirectorySeparatorChar + "yoshi" + Path.DirectorySeparatorChar + "model" + Path.DirectorySeparatorChar + "EffYoshiAirTrace" + slots.ToString("D2"), "*", SearchOption.AllDirectories));
                            fileSearch.AddRange(Directory.GetFiles(PathHelper.FolderWorkspace + "YoshiFix" + Path.DirectorySeparatorChar + "effect" + Path.DirectorySeparatorChar + "yoshi" + Path.DirectorySeparatorChar + "model" + Path.DirectorySeparatorChar + "EffYoshiTamagoKakeraA" + slots, "*", SearchOption.AllDirectories));
                            fileSearch.AddRange(Directory.GetFiles(PathHelper.FolderWorkspace + "YoshiFix" + Path.DirectorySeparatorChar + "effect" + Path.DirectorySeparatorChar + "yoshi" + Path.DirectorySeparatorChar + "model" + Path.DirectorySeparatorChar + "EffYoshiTamagoKakeraB" + slots, "*", SearchOption.AllDirectories));
                            fileSearch.AddRange(Directory.GetFiles(PathHelper.FolderWorkspace + "YoshiFix" + Path.DirectorySeparatorChar + "effect" + Path.DirectorySeparatorChar + "yoshi" + Path.DirectorySeparatorChar + "model" + Path.DirectorySeparatorChar + "EffYoshiTamagoKakeraC" + slots, "*", SearchOption.AllDirectories));
                        }
                        foreach (string f in fileSearch)
                        {
                            string explorerFilename = f.Replace(PathHelper.FolderWorkspace + "YoshiFix" + Path.DirectorySeparatorChar, string.Empty);
                            explorerFilename = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH) + "data" + Path.DirectorySeparatorChar + "fighter" + Path.DirectorySeparatorChar + "yoshi" + Path.DirectorySeparatorChar + explorerFilename;
                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        }
                        fileSearch.Clear();
                        fileSearch.AddRange(Directory.GetFiles(PathHelper.FolderWorkspace + "YoshiFix" + Path.DirectorySeparatorChar + slots + " slots", "*", SearchOption.AllDirectories));
                        foreach (string f in fileSearch)
                        {
                            string explorerFilename = f.Replace(PathHelper.FolderWorkspace + "YoshiFix" + Path.DirectorySeparatorChar + slots + " slots", string.Empty);
                            explorerFilename = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH) + "data" + Path.DirectorySeparatorChar + "fighter" + Path.DirectorySeparatorChar + "yoshi" + explorerFilename;
                            AddFileToResColFileLists(explorerFilename, f, baseFolders, filesLists);
                        }
                    }
                }
            }
            fileSearch.Clear();
            #endregion
            #region Explorer Mods
            if (_CurrentProject.EditorExplorerChanges)
            {
                for (int i = 0; i < _resCols.Count(); ++i)
                {
                    if (Directory.Exists(baseFolders[i]))
                    {
                        explorerFiles.AddRange(Directory.GetDirectories(baseFolders[i], "*", SearchOption.AllDirectories));
                        explorerFiles.AddRange(Directory.GetFiles(baseFolders[i], "*", SearchOption.AllDirectories));
                        foreach (string f in explorerFiles)
                        {
                            AddFileToResColFileLists(f, f, baseFolders, filesLists);
                        }
                        fileSearch.Clear();
                    }
                }
            }
            #endregion

            //Copy resources, pack files if needed
            ResourceCollection dataResCol = null;
            for (int i = 0; i < _resCols.Count(); ++i)
            {
                LogHelper.Debug(string.Format("Rebuilding partition '{0}'...", _resCols[i].PartitionName));

                //Cloning to leave the original resourcecollection untouched
                ResourceCollection newCol = (ResourceCollection)_resCols[i].Clone();

                //Build Export Files
                BuildingExportFiles(newCol, exportPatchFolder, packing, filesLists[i]);

                //Build Resource
                LogHelper.Info(string.Format("Rebuilding '{0}'...", newCol.ResourceName));
                ResourceCollection collection = null;
                if (!newCol.IsRegion) //No region, take the resource file "as it"
                {
                    collection = newCol;
                    dataResCol = newCol;
                }
                else
                    collection = GetMergedRegionResources(dataResCol, newCol);

                RemoveOriginalResourcesFromPackage(newCol);

                LogHelper.Debug(string.Format("Rebuilding resource file '{0}'...", _resCols[i].ResourceName));

                _RfManager.RebuildResourceFile(collection, exportPatchFolder);

                //Save CSV
                ExportCSV(_resCols[i], collection, exportPatchFolder);
            }

            //Patchlist
            LogHelper.Info("Rebuilding 'patchlist'...");
            BuildPatchfile(exportPatchFolder);

            LogHelper.Info(string.Format("Completed compilation of the mod ({0})", (packing ? "release" : "debug")));
            LogHelper.Info("----------------------------------------------------------------");
        }
        #endregion

        #region private methods
        private void RemoveOriginalResourcesFromPackage(ResourceCollection resCol)
        {
            SmashModItem modItem = _CurrentProject.ResourcesToRemove.Find(p => p.Partition == resCol.PartitionName);
            if (modItem == null)
                return;

            foreach(string relativePath in modItem.Paths)
            {
                resCol.Resources.Remove(relativePath);
                LogHelper.Debug(string.Format("Removing resource '{0}' from partition '{1}'", relativePath, resCol.PartitionName));
            }
        }

        private ResourceCollection GetMergedRegionResources(ResourceCollection dataResCol, ResourceCollection resCol)
        {
            if (!resCol.IsRegion)
                throw new Exception("resCol should be a region collection");

            ResourceCollection outputResCol = new ResourceCollection(resCol.PartitionName);
            Dictionary<string, ResourceItem> resources = outputResCol.Resources;

            Dictionary<string, ResourceItem> mainResources = dataResCol.Resources;
            Dictionary<string, ResourceItem> regionResources = resCol.GetFilteredResources();
            string partition = resCol.PartitionName;

            foreach (ResourceItem mainRItem in mainResources.Values)
            {
                if (!resCol.Resources.ContainsKey(mainRItem.RelativePath) && mainRItem.RelativePath.EndsWith("_us_en.flx"))
                    continue;

                ResourceItem rItem = (ResourceItem)mainResources[mainRItem.RelativePath].Clone();
                rItem.ResourceCollection = outputResCol;
                if (regionResources.ContainsKey(mainRItem.RelativePath) && !CurrentProject.IsUnlocalized(partition, mainRItem.RelativePath))
                {
                    resources.Add(mainRItem.RelativePath, regionResources[mainRItem.RelativePath]);
                }
                else
                {
                    rItem.Source = FileSource.NotFound;
                    resources.Add(mainRItem.RelativePath, rItem);
                }
            }

            foreach (ResourceItem regionRItem in regionResources.Values)
            {
                if (!resources.ContainsKey(regionRItem.RelativePath))
                    resources.Add(regionRItem.RelativePath, regionRItem);
            }

            return outputResCol;
        }

        struct FighterName
        {
            public int CharID;
            public int nameplateSlot;
            public string name;
            public string BoxingText;
        }

        private void BuildingExportFiles(ResourceCollection resCol, string exportFolder, bool packing, SortedList<string, string> filesList)
        {
            if (filesList.Count > 0)
            {
                LogHelper.Info(string.Format("Packaging '{0}'... to '{1}'", resCol.PartitionName, exportFolder));
                BuildNewResources(resCol, filesList, exportFolder, packing);
            }
        }

        private void BuildPatchfile(string exportFolder)
        {
            if (Directory.Exists(exportFolder))
            {
                string[] filesInExport = Directory.GetFiles(exportFolder, "*", SearchOption.AllDirectories);

                List<string> filesToAdd = new List<string>();
                List<string> filesToRemove = new List<string>();

                //Files to add
                foreach (string fileInExport in filesInExport)
                {
                    if (!Utils.IsAnAcceptedExtension(fileInExport))
                        continue;
                    string pathToProcess = fileInExport.Replace(exportFolder, string.Empty).Replace(Path.DirectorySeparatorChar, '/');
                    if (!filesToAdd.Contains(pathToProcess))
                        filesToAdd.Add(pathToProcess);
                }

                //Files to remove
                foreach (SmashModItem rModItem in _CurrentProject.UnlocalizationItems)
                {
                    foreach (string pathToRemove in rModItem.Paths)
                        filesToRemove.Add(rModItem.Partition + "/" + pathToRemove);
                }
                _RfManager.RebuildPatchListFile(filesToAdd.ToArray(), filesToRemove.ToArray(), exportFolder);
            }
        }

        private bool BuildNewResources(ResourceCollection resCol, SortedList<string,string> filesToProcess, string exportFolder, bool packing)
        {
            string[] files = filesToProcess.Keys.ToArray();

            string baseToExclude = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH, resCol.PartitionName);
            List<PackageProcessor> packNeedsRepacking = new List<PackageProcessor>();
            foreach (string file in files)
            {
                string relativePath = file.Replace(baseToExclude, string.Empty).Replace(Path.DirectorySeparatorChar, '/');

                //This ensure that we are dealing with a folder or a file.
                FileAttributes pathAttrs = File.GetAttributes(filesToProcess[file]);
                bool isFolder = false;
                if (pathAttrs.HasFlag(FileAttributes.Directory))
                {
                    relativePath += "/";
                    isFolder = true;
                }

                //Checking if part of a pack to repack, if yes, lets process it after with PackageProcessor
                ResourceItem resPacked = GetPackedPath(resCol, relativePath);
                if (resPacked != null && packNeedsRepacking.Find(p => p.PackedRelativePath == resPacked.RelativePath) == null)
                    packNeedsRepacking.Add(new PackageProcessor() { TopResource = resPacked, PackedRelativePath = resPacked.RelativePath, ExportFolder = exportFolder });
                if (resPacked != null)
                {
                    packNeedsRepacking.Find(p => p.PackedRelativePath == resPacked.RelativePath).FilesToAdd.Add(file);
                    continue;
                }

                ResourceItem nItem = GetEditedResource(resCol, relativePath);

                if (!isFolder)
                {
                    uint cmpSize;
                    uint decSize;
                    byte[] fileToWrite;

                    GetFileBinary(nItem, filesToProcess[file], out cmpSize, out decSize, out fileToWrite);

                    nItem.CmpSize = cmpSize;
                    nItem.DecSize = decSize;

                    //Copy to export folder
                    string savePath = exportFolder + resCol.PartitionName + Path.DirectorySeparatorChar + relativePath.Replace('/', Path.DirectorySeparatorChar);
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                    File.WriteAllBytes(savePath, fileToWrite);
                }
            }

            //packed management
            foreach (PackageProcessor package in packNeedsRepacking)
                ProcessPackage(package, packing, filesToProcess);

            return true;
        }

        private ResourceItem GetPackedPath(ResourceCollection resCol, string relativePath)
        {
            if (resCol.Resources.ContainsKey(relativePath) && resCol.Resources[relativePath].IsAPackage && !resCol.Resources[relativePath].IsFolder)
                return null;

            while (!resCol.Resources.ContainsKey(relativePath) || !resCol.Resources[relativePath].IsAPackage)
            {
                int nextPathCalc = relativePath.LastIndexOf("/", relativePath.Length - 2);
                if (nextPathCalc == -1)
                    return null;
                relativePath = relativePath.Substring(0, nextPathCalc) + "/";
            }
            return resCol.Resources[relativePath];
        }

        private ResourceItem GetEditedResource(ResourceCollection resCol, string relativePath)
        {
            ResourceItem nItem = null;
            if (resCol.Resources.ContainsKey(relativePath))
            {
                nItem = resCol.Resources[relativePath];
                nItem.OriginalResourceItem = (ResourceItem)nItem.Clone();
            }
            else
            {
                string fileName = string.Empty;
                if (relativePath.EndsWith("/"))
                {
                    fileName = relativePath.Substring(0, relativePath.Length - 1);
                    fileName = fileName.Substring(fileName.LastIndexOf("/") + 1) + "/";
                }
                else
                    fileName = relativePath.Substring(relativePath.LastIndexOf("/") + 1);
                nItem = new ResourceItem(resCol, fileName, 0, 0, 0, false, relativePath);
                resCol.Resources.Add(relativePath, nItem);
                nItem.ResourceCollection = resCol;
            }

            //Common info
            if (nItem.PatchItem == null)
                nItem.PatchItem = new PatchFileItem(relativePath, resCol.PartitionName + "/" + relativePath, false);
            nItem.Source = FileSource.Mod;

            return nItem;
        }

        private bool ShouldBeCompressed(ResourceItem rItem, byte[] file)
        {
            if (GlobalConstants.FORCE_ORIGINAL_COMPRESSION && rItem.OriginalResourceItem != null && rItem.OriginalResourceItem.CmpSize == rItem.OriginalResourceItem.DecSize)
                return false;

            //More rules to add
            if (rItem.Filename.EndsWith(".nus3bank"))
            {
                if ((rItem.Filename.StartsWith("snd_vc_") || rItem.Filename.StartsWith("snd_se_")) && !rItem.Filename.Contains("_ouen_"))
                    return false;
            }

            if (file.Length >= GlobalConstants.SIZE_FILE_COMPRESSION_MIN)
                return true;

            return false;
        }

        private void GetFileBinary(ResourceItem rItem, string file, out uint cmpSize, out uint decSize, out byte[] fileToWrite)
        {
            byte[] fileBinary = File.ReadAllBytes(file);
            cmpSize = (uint)fileBinary.Length;
            decSize = (uint)fileBinary.Length;
            //If file already uncompressed, check its dec size
            if (Utils.IsCompressed(fileBinary))
            {
                fileBinary = Utils.DeCompress(fileBinary);
                decSize = (uint)fileBinary.Length;
                cmpSize = (uint)fileBinary.Length;
            }
            //Check if it should be compressed, and return its CmpSize
            if (ShouldBeCompressed(rItem, fileBinary))
            {
                fileToWrite = Utils.Compress(fileBinary);
                cmpSize = (uint)fileToWrite.Length;
            }
            else
                fileToWrite = fileBinary;
        }

        private bool CheckIfLastFileInFolder(string[] files, string path, int position)
        {
            for (int i = position + 1; i < files.Length; i++)
            {
                if (files[i].StartsWith(path))
                    return true;
            }
            return false;
        }
        #endregion
        #endregion

        #region Processing packed files
        #region private methods
        private void ProcessPackageUnpacked(ResourceItem rItemTopResource, string[] files, string exportFolder, SortedList<string, string> fileRedirects)
        {
            ResourceCollection resCol = rItemTopResource.ResourceCollection;
            string baseTempFolder = PathHelper.FolderTemp + resCol.PartitionName + Path.DirectorySeparatorChar;

            for (int f = 0; f < files.Length; f++)
            {
                string file = files[f];

                string resPath = file.Replace(baseTempFolder, string.Empty).Replace(Path.DirectorySeparatorChar, '/');
                bool isFolder = false;
                FileAttributes pathAttrs = File.GetAttributes(fileRedirects[file]);
                if (pathAttrs.HasFlag(FileAttributes.Directory))
                {
                    resPath += "/";
                    isFolder = true;
                }

                ResourceItem nItem = GetEditedResource(resCol, resPath);
                nItem.OffInPack = 0;
                uint cmpSize = 0;
                uint decSize = 0;

                string unpackedPath = exportFolder + resCol.PartitionName + Path.DirectorySeparatorChar + file.Replace(baseTempFolder, string.Empty);
                if (!isFolder)
                {
                    byte[] fileToWrite;

                    GetFileBinary(nItem, fileRedirects[file], out cmpSize, out decSize, out fileToWrite);

                    nItem.CmpSize = cmpSize;
                    nItem.DecSize = decSize;
                    nItem.IsAPackage = true;

                    //Saving UnPackedFiles
                    Directory.CreateDirectory(Path.GetDirectoryName(unpackedPath));
                    File.WriteAllBytes(unpackedPath, fileToWrite);
                }
                else
                {
                    nItem.CmpSize = 0;
                    nItem.DecSize = 0;
                    nItem.IsAPackage = false;

                    Directory.CreateDirectory(unpackedPath);
                }
            }
        }

        private void ProcessPackage(PackageProcessor package, bool packing, SortedList<string,string> fileRedirects)
        {
            ResourceCollection resCol = package.TopResource.ResourceCollection;
            CleanTempFolder();

            //Extract all files
            ExtractFolder(package.TopResource.ResourceCollection, package.PackedRelativePath, PathHelper.FolderTemp);

            //Override with new files
            if (package.FilesToAdd != null)
            {
                string baseToExclude = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH);
                foreach (string file in package.FilesToAdd)
                {
                    string savePath = file.Replace(baseToExclude, PathHelper.FolderTemp);//baseTempFolder + newPath.Replace("/", Path.DirectorySeparatorChar);
                    FileAttributes pathAttrs = File.GetAttributes(fileRedirects[file]);
                    if (pathAttrs.HasFlag(FileAttributes.Directory))
                        Directory.CreateDirectory(savePath);
                    else
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                        File.Copy(fileRedirects[file], savePath, true);
                    }
                }
            }

            //Sort files with ordinal comparison
            string baseFolder = PathHelper.FolderTemp + package.PackedAbsolutePath.Replace('/', Path.DirectorySeparatorChar);
            string[] files = Directory.GetFiles(baseFolder, "*", SearchOption.AllDirectories);
            string[] folders = Directory.GetDirectories(baseFolder, "*", SearchOption.AllDirectories);
            int filesLength = files.Length;
            Array.Resize<string>(ref files, filesLength + folders.Length + 1);
            Array.Copy(folders, 0, files, filesLength, folders.Length);
            files[files.Length - 1] = baseFolder.Substring(0, baseFolder.Length - 1);
            Array.Sort(files, new CustomStringComparer());

            if (!packing)
            {
                ProcessPackageUnpacked(package.TopResource, files, package.ExportFolder, fileRedirects);
            }
            else
            {
                LogHelper.Info(string.Format("Packing '{0}' with {1} new files...", package.PackedAbsolutePath, package.FilesToAdd.Count));
                using (MemoryStream streamPackage = new MemoryStream())
                {
                    using (BinaryWriter writerPackage = new BinaryWriter(streamPackage))
                    {
                        ResourceItem[] currentFolder = new ResourceItem[20];
                        string[] currentFolderFile = new string[20];

                        string baseToExclude = PathHelper.FolderTemp + resCol.PartitionName + Path.DirectorySeparatorChar;
                        for (int f = 0; f < files.Length; f++)
                        {
                            uint currentPosition = (uint)streamPackage.Position;
                            string file = files[f];
                            uint paddingNeeded = 0;

                            string resPath = file.Replace(baseToExclude, string.Empty).Replace(Path.DirectorySeparatorChar, '/');
                            string fileName = Path.GetFileName(file);
                            FileAttributes pathAttrs = File.GetAttributes(file);
                            bool isFolder = pathAttrs.HasFlag(FileAttributes.Directory);
                            if (isFolder)
                                resPath += "/";

                            ResourceItem nItem = GetEditedResource(resCol, resPath);
                            int folderDepth = (int)nItem.FolderDepth;
                            uint cmpSize = 0;
                            uint decSize = 0;
                            nItem.IsAPackage = f == 0;

                            if (!isFolder)
                            {
                                byte[] fileToWrite;

                                GetFileBinary(nItem, file, out cmpSize, out decSize, out fileToWrite);

                                nItem.CmpSize = cmpSize;
                                nItem.DecSize = decSize;
                                nItem.OffInPack = (uint)streamPackage.Position;

                                //Write the file
                                if (fileToWrite.Length > 0)
                                    writerPackage.Write(fileToWrite);
                                else
                                {//If empty, must had 0x80 padding
                                    for (int i = 0x00; i < 0x80; i++)
                                        writerPackage.Write((byte)0xCC);
                                    paddingNeeded = 0x80;
                                }

                                if (f < files.Length - 1) //No padding for the last file
                                {
                                    uint fileAndPadding = cmpSize;
                                    while (fileAndPadding % 0x80 != 0 || fileAndPadding - cmpSize < 0x40)
                                    {
                                        writerPackage.Write((byte)0xCC);
                                        fileAndPadding++;
                                        paddingNeeded++;
                                    }
                                }

                                folderDepth--; //Back to folder level
                            }
                            else
                            {
                                nItem.OffInPack = (uint)streamPackage.Position;
                                nItem.CmpSize = 0;
                                nItem.DecSize = 0;
                                cmpSize = decSize = 0x80;

                                //New folder, add proper padding and yada yada
                                for (int i = 0x00; i < 0x80; i++)
                                    writerPackage.Write((byte)0xCC);

                                currentFolder[folderDepth] = nItem;
                                currentFolderFile[folderDepth] = file + Path.DirectorySeparatorChar;
                            }

                            //Update folders size
                            for (int i = folderDepth; i >= 0; i--)
                            {
                                if (currentFolder[i] == null)
                                    break;
                                currentFolder[i].CmpSize += cmpSize;
                                currentFolder[i].DecSize += decSize;
                                if (CheckIfLastFileInFolder(files, currentFolderFile[i], f))
                                {
                                    currentFolder[i].CmpSize += paddingNeeded;
                                    currentFolder[i].DecSize += paddingNeeded;
                                }
                            }
                        }
                    }

                    //Saving PackedFile
                    string packedPath = package.ExportFolder + files[0].Replace(PathHelper.FolderTemp, string.Empty) + Path.DirectorySeparatorChar + "packed";
                    Directory.CreateDirectory(Path.GetDirectoryName(packedPath));
                    File.WriteAllBytes(packedPath, streamPackage.ToArray());
                }
            }

            CleanTempFolder();
        }
        #endregion
        #endregion

        #region Packing Folder
        internal void PackFolder(ResourceCollection resCol, string pathToPack)
        {
            /*if (!resCol.Resources.ContainsKey(pathToPack))
            {
                Console.WriteLine(string.Format("Path {0} not found!", pathToPack));
                return;
            }
            Console.WriteLine(string.Format("Packing resource {0}...", pathToPack));

            ResourceItem rItem = resCol.Resources[pathToPack];

            PackageProcessor processPackage = new PackageProcessor();
            processPackage.TopResource = rItem;
            processPackage.PackedPath = rItem.Path;

            ProcessPackage(processPackage);

            SaveProject();*/
        }
        #endregion

        #region CSV Generation
        #region private methods
        private void ExportCSV(ResourceCollection originalResCol, ResourceCollection newResCol, string exportFolder)
        {
            if (_CurrentProject.ExportCSVList)
            {
                List<string> outputCSV = new List<string>();
                outputCSV.Add(GetHeader());

                foreach (ResourceItem nItem in newResCol.Resources.Values)
                {
                    if (!originalResCol.Resources.ContainsKey(nItem.RelativePath))
                    {
                        outputCSV.Add(GetCSVLine(null, nItem));
                        continue;
                    }

                    ResourceItem oItem = originalResCol.Resources[nItem.RelativePath];

                    //DecSize
                    if (oItem.DecSize != nItem.DecSize)
                    {
                        outputCSV.Add(GetCSVLine(oItem, nItem));
                        continue;
                    }

                    //CmpSize
                    if (!_CurrentProject.ExportCSVIgnoreCompSize && oItem.CmpSize != nItem.CmpSize)
                    {
                        outputCSV.Add(GetCSVLine(oItem, nItem));
                        continue;
                    }

                    //Flags
                    if (!_CurrentProject.ExportCSVIgnoreFlags && oItem.OriginalFlags != nItem.Flags)
                    {
                        outputCSV.Add(GetCSVLine(oItem, nItem));
                        continue;
                    }

                    //OffsetInPack
                    if (!_CurrentProject.ExportCSVIgnorePackOffsets && oItem.OffInPack != nItem.OffInPack)
                    {
                        outputCSV.Add(GetCSVLine(oItem, nItem));
                        continue;
                    }
                }
                if (!Directory.Exists(exportFolder + "~csv"))
                    Directory.CreateDirectory(exportFolder + "~csv");
                File.WriteAllLines(exportFolder + "~csv" + Path.DirectorySeparatorChar + "~" + newResCol.ResourceName + ".csv", outputCSV);
            }
        }

        private string GetHeader()
        {
            List<string> headerParts = new List<string>();
            headerParts.Add("Path");
            headerParts.Add("New decompSize");
            headerParts.Add("Old decompSize");
            headerParts.Add("New compSize");
            if (!_CurrentProject.ExportCSVIgnoreCompSize)
                headerParts.Add("Old compSize");
            headerParts.Add("New flags");
            if (!_CurrentProject.ExportCSVIgnoreFlags)
                headerParts.Add("Old flags");
            if (!_CurrentProject.ExportCSVIgnorePackOffsets)
            {
                headerParts.Add("New offset in pack");
                headerParts.Add("Old offset in pack");
            }
            return string.Join(",", headerParts);
        }

        private string GetCSVLine(ResourceItem oldItem, ResourceItem newItem)
        {
            List<string> lineParts = new List<string>();
            lineParts.Add(newItem.RelativePath);

            //DecSize
            lineParts.Add(newItem.DecSize.ToString());
            if (oldItem == null)
                lineParts.Add(string.Empty);
            else
                lineParts.Add(oldItem.DecSize.ToString());

            //CompSize
            lineParts.Add(newItem.CmpSize.ToString());
            if (!_CurrentProject.ExportCSVIgnoreCompSize)
            {
                if (oldItem == null)
                    lineParts.Add(string.Empty);
                else
                    lineParts.Add(oldItem.CmpSize.ToString());
            }

            //Flags
            lineParts.Add(String.Format("0x{0:X4}", newItem.Flags));
            if (!_CurrentProject.ExportCSVIgnoreFlags)
            {
                if (oldItem == null)
                    lineParts.Add(string.Empty);
                else
                    lineParts.Add(String.Format("0x{0:X4}", oldItem.Flags));
            }

            //PackOffset
            if (!_CurrentProject.ExportCSVIgnorePackOffsets)
            {
                lineParts.Add(String.Format("0x{0:X8}", newItem.OffInPack));
                if (oldItem == null)
                    lineParts.Add(string.Empty);
                else
                    lineParts.Add(String.Format("0x{0:X8}", oldItem.OffInPack));
            }
            return string.Join(",", lineParts);
        }
        #endregion
        #endregion CSV Generation

        #region Tools Methods
        #region public methods
        /// <summary>
        /// Get the reference of the ResourceCollection associated to an absolutepath (data/path/to/resource)
        /// </summary>
        /// <param name="absolutePath">data/path/to/resource</param>
        /// <returns>ResourceCollection instance</returns>
        public ResourceCollection GetResourceCollection(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return null;
            string resColName = absolutePath.Substring(0, absolutePath.IndexOf('/'));
            return _resCols.First(p => p.PartitionName == resColName);
        }

        /// <summary>
        /// Get the reference of the ResourceItem associated to an absolutepath (data/path/to/resource)
        /// </summary>
        /// <param name="absolutePath">data/path/to/resource</param>
        /// <returns>ResourceItem object, if found</returns>
        public ResourceItem GetResource(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return null;
            string relativePath = GetRelativePath(absolutePath);
            ResourceCollection resCol = GetResourceCollection(absolutePath);
            return GetResource(resCol, relativePath);
        }

        /// <summary>
        /// Get the reference of the ResourceItem associated to an relativepath (path/to/resource) and a Resource Collection instance
        /// </summary>
        /// <param name="resCol">ResourceCollection instance</param>
        /// <param name="relativePath">path/to/resource</param>
        /// <returns>ResourceItem object, if found</returns>
        public ResourceItem GetResource(ResourceCollection resCol, string relativePath)
        {
            if (resCol == null)
                return null;
            if (resCol.Resources.ContainsKey(relativePath))
                return resCol.Resources[relativePath];
            return null;
        }

        /// <summary>
        /// Get the reference of the ResourceItems associated to an absolutepath (data/path/to/resource)
        /// </summary>
        /// <param name="absolutePath">data/path/to/resource</param>
        /// <returns>ResourceItem list of objects, if found</returns>
        public List<ResourceItem> GetResources(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return null;
            string relativePath = GetRelativePath(absolutePath);
            ResourceCollection resCol = GetResourceCollection(absolutePath);
            return GetResources(resCol, relativePath);
        }

        /// <summary>
        /// Get the reference of the ResourceItems associated to an relativepath (path/to/resource) and a Resource Collection instance
        /// </summary>
        /// <param name="resCol">ResourceCollection instance</param>
        /// <param name="relativePath">path/to/resource</param>
        /// <returns>ResourceItem list of objects, if found</returns>
        public List<ResourceItem> GetResources(ResourceCollection resCol, string relativePath)
        {
            if (resCol == null)
                return new List<ResourceItem>();
            List<ResourceItem> nItems = new List<ResourceItem>();
            foreach (ResourceItem rItem in resCol.CachedFilteredResources.Values)
            {
                if (rItem.RelativePath.StartsWith(relativePath))
                    nItems.Add(rItem);
            }
            return nItems;
        }

        /// <summary>
        /// Get a physical file to a resource in the workspace, given an absolutepath (data/path/to/resource)
        /// </summary>
        /// <param name="absolutePath">data/path/to/resource</param>
        /// <returns>Path to a file in the filesystem</returns>
        public string GetWorkspaceFileFromPath(string absolutePath)
        {
            return PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH) + absolutePath.Replace('/', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Get a list of resources in the workspace given given an absolutepath (data/path/to/resource), will look for children folders too.
        /// </summary>
        /// <param name="absolutePath">data/path/to/resource</param>
        /// <returns>List of relativepaths of the resources found</returns>
        public string[] GetAllWorkplaceRelativePaths(string absolutePath)
        {
            return GetAllWorkplaceRelativePaths(absolutePath, true);
        }

        /// <summary>
        /// Get a list of resources in the workspace given given an absolutepath (data/path/to/resource), with the option to look for children too.
        /// </summary>
        /// <param name="absolutePath">data/path/to/resource</param>
        /// <param name="recursive">True if you want to look in every level folders</param>
        /// <returns>List of relativepaths of the resources found</returns>
        public string[] GetAllWorkplaceRelativePaths(string absolutePath, bool recursive)
        {
            if (absolutePath.EndsWith("/"))
                absolutePath = absolutePath.Substring(0, absolutePath.Length - 1);
            absolutePath = absolutePath.Replace('/', Path.DirectorySeparatorChar);
            string baseToRemove = PathHelper.GetExplorerFolder(PathHelperEnum.FOLDER_PATCH, absolutePath);
            if (Directory.Exists(baseToRemove))
            {
                List<string> pathResources = new List<string>();
                pathResources.AddRange(Directory.GetFiles(baseToRemove, "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
                pathResources.AddRange(Directory.GetDirectories(baseToRemove, "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
                for (int i = 0; i < pathResources.Count; i++)
                {
                    FileAttributes pathAttrs = File.GetAttributes(pathResources[i]);
                    pathResources[i] = pathResources[i].Replace(baseToRemove, string.Empty).Replace(Path.DirectorySeparatorChar, '/');
                    if (pathAttrs.HasFlag(FileAttributes.Directory) && !pathResources[i].EndsWith("/"))
                        pathResources[i] += "/";
                }
                return pathResources.ToArray();
            }
            return new string[0];
        }

        /// <summary>
        /// Delete everything in the temp folder
        /// </summary>
        public void CleanTempFolder()
        {
            if(_CurrentProject != null)
                if (Directory.Exists(PathHelper.FolderTemp))
                    Directory.Delete(PathHelper.FolderTemp, true);
        }
        #endregion

        #region private methods
        private bool CheckDTFiles()
        {
            if ((!File.Exists(PathHelper.GetGameFolder(PathHelperEnum.FOLDER_CONTENT) + "dt00") ||
                !File.Exists(PathHelper.GetGameFolder(PathHelperEnum.FOLDER_CONTENT) + "dt01")))
            {
                LogHelper.Warning("Missing DT Files, you will not be able to extract from LS.");
                return false;
            }
            FileInfo dt00 = new FileInfo(PathHelper.GetGameFolder(PathHelperEnum.FOLDER_CONTENT) + "dt00");
            FileInfo dt01 = new FileInfo(PathHelper.GetGameFolder(PathHelperEnum.FOLDER_CONTENT) + "dt01");

            if (!_CurrentProject.CheckDTSizeWiiU(dt00.Length, dt01.Length))
            {
                LogHelper.Warning(string.Format("The filesize of dt00/dt01 doesn't match the region set in your config ({0}). You will not be able to extract from LS.", _CurrentProject.GameRegion));
                string guessedRegionName = _CurrentProject.GuessRegionFromDTFiles(dt00.Length, dt01.Length);
                if (!string.IsNullOrEmpty(guessedRegionName))
                    LogHelper.Info(string.Format("It seems that the size of your dt00/dt01 files match the ({0}) region. You might want to edit your config file: 1 is for JPN, 2 is for USA, 4 is for EUR.", guessedRegionName));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get a relative path of a resource given an absolutepath (data/path/to/resource)
        /// </summary>
        /// <param name="absolutePath">data/path/to/resource</param>
        /// <returns>relativepath (path/to/resource)</returns>
        public string GetRelativePath(string absolutePath)
        {
            if (absolutePath.Contains("data"))
                return absolutePath.Substring(absolutePath.IndexOf("/") + 1);
            return absolutePath;
        }

        /// <summary>
        /// Get an absolute path of a resource given a relativepath (path/to/resource) and a ResourceCollection instance
        /// </summary>
        /// <param name="resCol">ResourceCollection instance</param>
        /// <param name="relativePath">path/to/resource</param>
        /// <returns>absolutepath (data/path/to/resource)</returns>
        public string GetAbsolutePath(ResourceCollection resCol, string relativePath)
        {
            return resCol.PartitionName + "/" + relativePath;
        }

        private List<string> FilterRelativePath(string[] paths, string relativePath)
        {
            List<string> files = new List<string>();
            foreach (string path in paths)
            {
                if (path.StartsWith(relativePath))
                    files.Add(path);
            }
            return files;
        }
        #endregion
        #endregion
    }
}
