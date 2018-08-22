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
        public static string FolderTemp { get { return GetProjectTempFolder(); } }
        public static string FolderExplorer { get { return GetProjectExplorerFolder(); } }
        public static string FolderWorkspace { get { return GetProjectWorkspaceFolder(); } }
        //public static string FolderPlugins { get { return GetPluginsFolder(); } }
        public static string FolderExtract { get { return GetProjectExtractFolder(); } }
        public static string FolderExport { get { return GetProjectExportFolder(); } }
        public static string FolderCharMods { get { return GetCharModsFolder(); } }
        public static string FolderCharSlotsMods { get { return GetCharModsSlotsFolder(); } }
        public static string FolderCharGeneralMods { get { return GetCharModsGeneralFolder(); } }
        public static string FolderStageMods { get { return GetStageModsFolder(); } }
        public static string FolderGeneralMods { get { return GetGeneralModsFolder(); } }
        public static string FolderEditorMods { get { return GetEditorModsFolder(); } }

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
            return GetProjectWorkspaceFolder();
        }

        public static string GetExplorerFolder(PathHelperEnum path)
        {
            return GetURI(GetProjectExplorerFolder(), path);
        }

        public static string GetExplorerFolder(PathHelperEnum path, string folder)
        {
            return GetURI(GetProjectExplorerFolder(), path, folder);
        }
        
        public static string GetExtractFolder(PathHelperEnum path)
        {
            return GetURI(GetProjectExtractFolder(), path);
        }

        public static string GetExtractFolder(PathHelperEnum path, string folder)
        {
            return GetURI(GetProjectExtractFolder(), path, folder);
        }

        public static string GetCharModsFolder()
        {
            return FolderWorkspace + "Characters" + Path.DirectorySeparatorChar;
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
            return FolderWorkspace + "Stages" + Path.DirectorySeparatorChar;
        }

        public static string GetGeneralModsFolder()
        {
            return FolderWorkspace + "General" + Path.DirectorySeparatorChar;
        }

        public static string GetEditorModsFolder()
        {
            return FolderWorkspace + "Editor" + Path.DirectorySeparatorChar;
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
            if (string.IsNullOrEmpty(_Project.ProjectTempFolder))
                return Path.GetTempPath() + "kamimodpackbuilder" + Path.DirectorySeparatorChar;
            return _Project.ProjectTempFolder + (!_Project.ProjectTempFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
        }

        private static string GetProjectExplorerFolder()
        {
            if (string.IsNullOrEmpty(_Project.ProjectExplorerFolder))
                return _Config.LastProject.Replace(Path.GetFileName(_Config.LastProject), "") + "Mods" + Path.DirectorySeparatorChar + "Explorer" + Path.DirectorySeparatorChar;
            return _Project.ProjectExplorerFolder + (!_Project.ProjectExplorerFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
        }

        private static string GetProjectWorkspaceFolder()
        {
            if (string.IsNullOrEmpty(_Project.ProjectWorkspaceFolder))
                return _Config.LastProject.Replace(Path.GetFileName(_Config.LastProject), "") + "Mods" + Path.DirectorySeparatorChar;
            return _Project.ProjectWorkspaceFolder + (!_Project.ProjectWorkspaceFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
        }

        private static string GetProjectExportFolder()
        {
            if (string.IsNullOrEmpty(_Project.ProjectExportFolder))
                return _Config.LastProject.Replace(Path.GetFileName(_Config.LastProject),"") + "Export" + Path.DirectorySeparatorChar;
            return _Project.ProjectExportFolder + (!_Project.ProjectExportFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
        }

        private static string GetProjectExtractFolder()
        {
            if (string.IsNullOrEmpty(_Project.ProjectExtractFolder))
                return _Config.LastProject.Replace(Path.GetFileName(_Config.LastProject), "") + "Extract" + Path.DirectorySeparatorChar;
            return _Project.ProjectExtractFolder + (!_Project.ProjectExtractFolder.EndsWith(Path.DirectorySeparatorChar.ToString()) ? Path.DirectorySeparatorChar.ToString() : string.Empty);
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
