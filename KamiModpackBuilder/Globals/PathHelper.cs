using KamiModpackBuilder.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace KamiModpackBuilder.Globals
{
    public enum PathHelperEnum
    {
        FOLDER_CODE = 0,
        FOLDER_CONTENT = 1,
        FOLDER_META = 2,
        FOLDER_MOVIE = 3,
        FOLDER_SOUND = 4,
        FOLDER_SOUND_BGM = 5,
        FOLDER_PATCH = 6,
        FOLDER_PATCH_DATA = 7,
        FILE_LS = 8,
        FILE_RPX = 9,
        FILE_META = 10,
        FILE_PATCH_RESOURCE = 11,
        FILE_PATCH_PATCHLIST = 12,
    }

    public static class PathHelper
    {
        private static SmashMod _Project;
        public static Config _Config;
        public static string FolderProject { get { return _Config.LastProject.Replace(Path.GetFileName(_Config.LastProject), ""); } }
        public static string FolderTemp { get { return GetProjectTempFolder(); } }
        public static string FolderExplorerFullPath { get { return GetProjectExplorerFolderFullPath(); } }
        public static string FolderWorkspaceFullPath { get { return GetProjectWorkspaceFolderFullPath(); } }
        public static string FolderExtractFullPath { get { return GetProjectExtractFolderFullPath(); } }
        public static string FolderExportFullPath { get { return GetProjectExportFolderFullPath(); } }
        public static string FolderExplorer { get { return GetProjectExplorerFolder(); } }
        public static string FolderWorkspace { get { return GetProjectWorkspaceFolder(); } }
        public static string FolderExtract { get { return GetProjectExtractFolder(); } }
        public static string FolderExport { get { return GetProjectExportFolder(); } }
        public static string FolderExplorerDefault { get { return GetProjectExplorerFolderDefault(); } }
        public static string FolderWorkspaceDefault { get { return GetProjectWorkspaceFolderDefault(); } }
        public static string FolderExtractDefault { get { return GetProjectExtractFolderDefault(); } }
        public static string FolderExportDefault { get { return GetProjectExportFolderDefault(); } }
        public static string FolderCharMods { get { return GetCharModsFolder(); } }
        public static string FolderCharSlotsMods { get { return GetCharModsSlotsFolder(); } }
        public static string FolderCharGeneralMods { get { return GetCharModsGeneralFolder(); } }
        public static string FolderStageMods { get { return GetStageModsFolder(); } }
        public static string FolderGeneralMods { get { return GetGeneralModsFolder(); } }
        public static string FolderEditorMods { get { return GetEditorModsFolder(); } }
        public static string FolderBGM { get { return GetBGMFolder(); } }
        public static string FolderMovie { get { return GetMovieFolder(); } }

        #region public methods
        public static bool IsItSmashFolder(string folder)
        {
            if (!File.Exists(PathHelper.GetURI(folder, PathHelperEnum.FILE_LS)))// ||
                                                                                      //!File.Exists(PathHelper.GetURI(folder, PathHelperEnum.FILE_META)) ||
                                                                                      //!File.Exists(PathHelper.GetURI(folder, PathHelperEnum.FILE_RPX)))
                return false;
            return true;
        }

        public static bool DoesItHavePatchFolder(string folder)
        {
            if (!File.Exists(PathHelper.GetURI(folder, PathHelperEnum.FILE_PATCH_RESOURCE)))
                return false;
            return true;
        }


        public static string GetGameFolder(PathHelperEnum path)
        {
            return GetURI(_Project.GamePath, path);
        }

        public static string GetGameFolder(PathHelperEnum path, string folder)
        {
            return GetURI(_Project.GamePath, path, folder);
        }

        public static string GetWorkspaceFolder()
        {
            return GetProjectWorkspaceFolderFullPath();
        }

        public static string GetExplorerFolder(PathHelperEnum path)
        {
            return GetURI(GetProjectExplorerFolderFullPath(), path);
        }

        public static string GetExplorerFolder(PathHelperEnum path, string folder)
        {
            return GetURI(GetProjectExplorerFolderFullPath(), path, folder);
        }
        
        public static string GetExtractFolder(PathHelperEnum path)
        {
            return GetURI(GetProjectExtractFolderFullPath(), path);
        }

        public static string GetExtractFolder(PathHelperEnum path, string folder)
        {
            return GetURI(GetProjectExtractFolderFullPath(), path, folder);
        }

        public static string GetCharModsFolder()
        {
            return FolderWorkspaceFullPath + "Characters" + Path.DirectorySeparatorChar;
        }

        public static string GetCharModsSlotsFolder()
        {
            return FolderCharMods + "Slots" + Path.DirectorySeparatorChar;
        }

        public static string GetCharModsGeneralFolder()
        {
            return FolderCharMods + "General" + Path.DirectorySeparatorChar;
        }

        public static string GetStageModsFolder()
        {
            return FolderWorkspaceFullPath + "Stages" + Path.DirectorySeparatorChar;
        }

        public static string GetGeneralModsFolder()
        {
            return FolderWorkspaceFullPath + "General" + Path.DirectorySeparatorChar;
        }

        public static string GetEditorModsFolder()
        {
            return FolderWorkspaceFullPath + "Editor" + Path.DirectorySeparatorChar;
        }

        public static string GetBGMFolder()
        {
            return FolderWorkspaceFullPath + "BGM" + Path.DirectorySeparatorChar;
        }

        public static string GetMovieFolder()
        {
            return FolderWorkspaceFullPath + "Movie" + Path.DirectorySeparatorChar;
        }

        public static string GetCharacterSlotsFolder(string charName)
        {
            return FolderCharSlotsMods + charName + Path.DirectorySeparatorChar;
        }

        public static string GetCharacterSlotModPath(string charName, string modName)
        {
            return GetCharacterSlotsFolder(charName) + modName + Path.DirectorySeparatorChar;
        }

        public static string GetCharacterSlotModKamiPath(string charName, string modName)
        {
            return GetCharacterSlotModPath(charName, modName) + "kamimod.xml";
        }

        public static string GetCharacterGeneralFolder(string charName)
        {
            return FolderCharGeneralMods + charName + Path.DirectorySeparatorChar;
        }

        public static string GetCharacterGeneralModPath(string charName, string modName)
        {
            return GetCharacterGeneralFolder(charName) + modName + Path.DirectorySeparatorChar;
        }

        public static string GetStageModPath(string modName)
        {
            return GetStageModsFolder() + modName + Path.DirectorySeparatorChar;
        }

        public static string GetGeneralModPath(string modName)
        {
            return GetGeneralModsFolder() + modName + Path.DirectorySeparatorChar;
        }

        public static string GetApplicationDirectory()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar;
        }
        #endregion

        #region internal methods
        internal static void InitializePathHelper(SmashMod project)
        {
            if (project == null)
                throw new Exception("Project null");
            _Project = project;
        }

        internal static string[] GetDTFiles()
        {
            List<string> lDtList = new List<string>();
            string contentURI = GetGameFolder(PathHelperEnum.FOLDER_CONTENT);
            int i = 0;
            //3DS
            if (File.Exists(contentURI + "dt"))
            {
                lDtList.Add(contentURI + "dt");
            }
            else
            {
                //WiiU
                while (File.Exists(contentURI + "dt0" + i))
                {
                    lDtList.Add(contentURI + "dt0" + i);
                    i++;
                }
            }
            return lDtList.ToArray();
        }

        internal static string[] GetResourceFiles(string folder)
        {
            List<string> listResourceFiles = new List<string>();
            foreach (string resourceFile in Directory.GetFiles(folder))
            {
                if (!Path.GetFileName(resourceFile).StartsWith("resource") || Path.GetFileName(resourceFile).Contains("."))
                    continue;
                listResourceFiles.Add(resourceFile);
            }
            return listResourceFiles.ToArray();
        }
        #endregion

        #region private methods
        private static string GetProjectTempFolder()
        {
            return Path.GetTempPath() + "kamimodpackbuilder" + Path.DirectorySeparatorChar;
        }

        private static string GetProjectExplorerFolderFullPath()
        {
            if (string.IsNullOrEmpty(_Project.ProjectExplorerFolderFullPath))
            {
                if (Path.IsPathRooted(_Project.ProjectExplorerFolder))
                    _Project.ProjectExplorerFolderFullPath = _Project.ProjectExplorerFolder + (!_Project.ProjectExplorerFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                else
                    _Project.ProjectExplorerFolderFullPath = FolderProject + _Project.ProjectExplorerFolder + (!_Project.ProjectExplorerFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                
                return _Project.ProjectExplorerFolderFullPath;
            }
            return _Project.ProjectExplorerFolderFullPath;
        }

        private static string GetProjectWorkspaceFolderFullPath()
        {
            if (string.IsNullOrEmpty(_Project.ProjectWorkspaceFolderFullPath))
            {
                if (Path.IsPathRooted(_Project.ProjectWorkspaceFolder))
                    _Project.ProjectWorkspaceFolderFullPath = _Project.ProjectWorkspaceFolder + (!_Project.ProjectWorkspaceFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                else
                    _Project.ProjectWorkspaceFolderFullPath = FolderProject + _Project.ProjectWorkspaceFolder + (!_Project.ProjectWorkspaceFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                
                return _Project.ProjectWorkspaceFolderFullPath;
            }
            return _Project.ProjectWorkspaceFolderFullPath;
        }

        private static string GetProjectExportFolderFullPath()
        {
            if (string.IsNullOrEmpty(_Project.ProjectExportFolderFullPath))
            {
                if (Path.IsPathRooted(_Project.ProjectExportFolder))
                    _Project.ProjectExportFolderFullPath = _Project.ProjectExportFolder + (!_Project.ProjectExportFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                else
                    _Project.ProjectExportFolderFullPath = FolderProject + _Project.ProjectExportFolder + (!_Project.ProjectExportFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                
                return _Project.ProjectExportFolderFullPath;
            }
            return _Project.ProjectExportFolderFullPath;
        }

        private static string GetProjectExtractFolderFullPath()
        {
            if (string.IsNullOrEmpty(_Project.ProjectExtractFolderFullPath))
            {
                if (Path.IsPathRooted(_Project.ProjectExtractFolder))
                    _Project.ProjectExtractFolderFullPath = _Project.ProjectExtractFolder + (!_Project.ProjectExtractFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                else
                    _Project.ProjectExtractFolderFullPath = FolderProject + _Project.ProjectExtractFolder + (!_Project.ProjectExtractFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
                
                return _Project.ProjectExtractFolderFullPath;
            }
            return _Project.ProjectExtractFolderFullPath;
        }

        private static string GetProjectExplorerFolder()
        {
            if (string.IsNullOrEmpty(_Project.ProjectExplorerFolder))
                _Project.ProjectExplorerFolder = FolderExplorerDefault;
            return _Project.ProjectExplorerFolder;
        }

        private static string GetProjectWorkspaceFolder()
        {
            if (string.IsNullOrEmpty(_Project.ProjectWorkspaceFolder))
                _Project.ProjectWorkspaceFolder = FolderWorkspaceDefault;
            return _Project.ProjectWorkspaceFolder;
        }

        private static string GetProjectExportFolder()
        {
            if (string.IsNullOrEmpty(_Project.ProjectExportFolder))
                _Project.ProjectExportFolder = FolderExportDefault;
            return _Project.ProjectExportFolder;
        }

        private static string GetProjectExtractFolder()
        {
            if (string.IsNullOrEmpty(_Project.ProjectExtractFolder))
                _Project.ProjectExtractFolder = FolderExtractDefault;
            return _Project.ProjectExtractFolder;
        }

        private static string GetProjectExplorerFolderDefault()
        {
            return "Mods" + Path.DirectorySeparatorChar + "Explorer" + Path.DirectorySeparatorChar;
        }

        private static string GetProjectWorkspaceFolderDefault()
        {
            return "Mods" + Path.DirectorySeparatorChar;
        }

        private static string GetProjectExportFolderDefault()
        {
            return "Export" + Path.DirectorySeparatorChar;
        }

        private static string GetProjectExtractFolderDefault()
        {
            return "Extract" + Path.DirectorySeparatorChar;
        }
        /*
        private static string GetPluginsFolder()
        {
            return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "plugins" + Path.DirectorySeparatorChar;
        }
        */
        private static string GetURI(string rootFolder, PathHelperEnum path)
        {
            switch (path)
            {
                case PathHelperEnum.FOLDER_CODE:
                    return rootFolder + "code" + Path.DirectorySeparatorChar;
                case PathHelperEnum.FOLDER_CONTENT:
                    return rootFolder + "content" + Path.DirectorySeparatorChar;
                case PathHelperEnum.FOLDER_META:
                    return rootFolder + "meta" + Path.DirectorySeparatorChar;
                case PathHelperEnum.FOLDER_MOVIE:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_CONTENT) + "movie" + Path.DirectorySeparatorChar;
                case PathHelperEnum.FOLDER_SOUND:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_CONTENT) + "sound" + Path.DirectorySeparatorChar;
                case PathHelperEnum.FOLDER_SOUND_BGM:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_SOUND) + "bgm" + Path.DirectorySeparatorChar;
                case PathHelperEnum.FOLDER_PATCH:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_CONTENT) + "patch" + Path.DirectorySeparatorChar;
                case PathHelperEnum.FOLDER_PATCH_DATA:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_PATCH) + "data" + Path.DirectorySeparatorChar;
                case PathHelperEnum.FILE_LS:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_CONTENT) + "ls";
                case PathHelperEnum.FILE_RPX:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_CODE) + "cross_f.rpx";
                case PathHelperEnum.FILE_META:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_META) + "meta.xml";
                case PathHelperEnum.FILE_PATCH_RESOURCE:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_PATCH) + "resource";
                case PathHelperEnum.FILE_PATCH_PATCHLIST:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_PATCH) + "patchlist";
                default:
                    return rootFolder;
            }
        }

        private static string GetURI(string rootFolder, PathHelperEnum path, string folder)
        {
            switch (path)
            {
                case PathHelperEnum.FOLDER_PATCH:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_PATCH) + folder + Path.DirectorySeparatorChar;
                case PathHelperEnum.FILE_PATCH_RESOURCE:
                    return GetURI(rootFolder, PathHelperEnum.FOLDER_PATCH) + folder;
                default:
                    return GetURI(rootFolder, path);
            }
        }
        #endregion

        public static string RemoveInvalidFilenameChars(string input)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char invalid in invalidChars)
            {
                input = input.Replace(invalid.ToString(), string.Empty);
            }
            return input;
        }
    }
}
