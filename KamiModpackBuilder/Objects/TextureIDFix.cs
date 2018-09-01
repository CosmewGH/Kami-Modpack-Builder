using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KamiModpackBuilder.Globals;

namespace KamiModpackBuilder.Objects
{
    static class TextureIDFix
    {
        public static void ChangeTextureID(string directoryName, int charID, ushort id)
        {
            CharacterException exc = CharacterException.None;
            if (charID == 0x2C && !SmashProjectManager.instance.CurrentProject.IsSwitch) exc = CharacterException.Robin_WiiU;
            if (charID == 0x31 && !SmashProjectManager.instance.CurrentProject.IsSwitch) exc = CharacterException.Pacman_WiiU;
            Mod mod = new Mod(directoryName, exc);
            ChangeTextureID(mod, id);
        }

        public static void MassTextureIdFix()
        {
            SmashMod project = SmashProjectManager.instance.CurrentProject;
            foreach (DB.Fighter fighter in DB.FightersDB.Fighters)
            {
                List<ushort> ids = new List<ushort>();

                foreach (CharacterSlotMod mod in project.ActiveCharacterSlotMods)
                {
                    if (mod.CharacterID != fighter.id) continue;
                    string ModPath = PathHelper.GetCharacterSlotModPath(fighter.name, mod.FolderName);
                    string PathKami = ModPath + "kamimod.xml";

                    CharacterSlotModXML xml = Utils.DeserializeXML<CharacterSlotModXML>(PathKami);
                    if (xml == null) continue;

                    bool hasModels = false;
                    string[] nutFiles = new string[0];
                    if (Directory.Exists(ModPath + Path.DirectorySeparatorChar + "model"))
                    {
                        if (Directory.Exists(ModPath + Path.DirectorySeparatorChar + "model" + Path.DirectorySeparatorChar + "body")) nutFiles = Directory.GetFiles(ModPath + Path.DirectorySeparatorChar + "model" + Path.DirectorySeparatorChar + "body", "*.nut", SearchOption.AllDirectories);
                        if (nutFiles.Length > 0) hasModels = true;
                        else
                        {
                            nutFiles = Directory.GetFiles(ModPath + Path.DirectorySeparatorChar + "model", "*.nut", SearchOption.AllDirectories);
                            if (nutFiles.Length > 0) hasModels = true;
                        }
                    }
                    if (hasModels)
                    {
                        FileTypes.NUT nut = new FileTypes.NUT();
                        nut.Read(nutFiles[0]);
                        if (nut.Textures.Count > 0)
                        {
                            xml.TextureID = (nut.Textures[0].HashId & 0x0000FF00) >> 8;
                        }
                        else continue;
                    }
                    else continue;

                    if ((xml.TextureID % 4 == 0 && xml.TextureID < 128) || ids.Contains((ushort)xml.TextureID))
                    {
                        xml.TextureID = 128;
                        while (ids.Contains((ushort)xml.TextureID)) ++xml.TextureID;

                        ChangeTextureID(ModPath + "model", fighter.id, (ushort)xml.TextureID);
                        Globals.Utils.SerializeXMLToFile(xml, PathKami);
                        Globals.LogHelper.Info(String.Format("Changed Texture ID of {0} to {1} successfully.", mod.FolderName, xml.TextureID));
                    }
                    ids.Add((ushort)xml.TextureID);
                }
            }
        }

        private enum CharacterException { None, Pacman_WiiU, Robin_WiiU}
        
        private class Mod
        {
            public string[] nudFiles;
            public string[] nutFiles;
            public string[] mtaFiles = new string[0];
            public Dictionary<int, int> hashChanges = new Dictionary<int, int>();

            public Mod(string directoryName, CharacterException exception)
            {
                nutFiles = Directory.GetFiles(directoryName, "*.nut", SearchOption.AllDirectories);
                nudFiles = Directory.GetFiles(directoryName, "*.nud", SearchOption.AllDirectories);
                switch (exception) {
                    case CharacterException.Pacman_WiiU:
                        mtaFiles = Directory.GetFiles(directoryName, "model.mta", SearchOption.AllDirectories);
                        break;
                    case CharacterException.Robin_WiiU:
                        mtaFiles = Directory.GetFiles(directoryName, "FitRefletBodyMainbook.mta", SearchOption.AllDirectories);
                        break;
                }
            }
        }

        private static void ChangeTextureID(Mod mod, ushort id)
        {
            ushort textureNum = 0;
            int baseID = 0;
            List<FileTypes.NUT> nuts = new List<FileTypes.NUT>();
            List<FileTypes.NUD> nuds = new List<FileTypes.NUD>();
            List<FileTypes.MTA> mtas = new List<FileTypes.MTA>();
            for (int i = 0; i < mod.nutFiles.Length; ++i)
            {
                FileTypes.NUT n = new FileTypes.NUT();
                n.Read(mod.nutFiles[i]);
                nuts.Add(n);
                foreach (FileTypes.NutTexture t in n.Textures)
                {
                    if (!mod.hashChanges.ContainsKey(t.HashId))
                    {
                        mod.hashChanges.Add(t.HashId, (int)((t.HashId & 0xFFFF0000) | (id << 8) | textureNum));
                        ++textureNum;
                        if (baseID == 0) baseID = t.HashId;
                    }
                }
            }
            for (int i = 0; i < mod.nudFiles.Length; ++i)
            {
                FileTypes.NUD n = new FileTypes.NUD();
                n.Read(mod.nudFiles[i]);
                nuds.Add(n);
            }
            for (int i = 0; i < mod.mtaFiles.Length; ++i)
            {
                FileTypes.MTA m = new FileTypes.MTA();
                m.Read(mod.mtaFiles[i], (short)(baseID >> 16));
                mtas.Add(m);
            }
            foreach (FileTypes.NUT n in nuts)
            {
                List<int> changedOffsets = new List<int>();
                foreach (FileTypes.NutTexture t in n.Textures)
                {
                    if (changedOffsets.Contains(t.HashOffset)) continue;

                    changedOffsets.Add(t.HashOffset);
                    n.Data.seek(t.HashOffset);
                    n.Data.writeInt(mod.hashChanges[t.HashId]);
                }
                n.SaveToFile(n.SourceFile);
            }
            foreach (FileTypes.NUD n in nuds)
            {
                List<int> changedOffsets = new List<int>();
                foreach (FileTypes.NUD.MatTexture t in n.allTextures)
                {
                    if (!mod.hashChanges.ContainsKey(t.hash)) continue;

                    if (changedOffsets.Contains(t.hashPos)) continue;
                    changedOffsets.Add(t.hashPos);

                    n.Data.seek(t.hashPos);
                    n.Data.writeInt(mod.hashChanges[t.hash]);
                }
                n.SaveToFile(n.SourceFile);
            }
            foreach (FileTypes.MTA m in mtas)
            {
                List<int> changedOffsets = new List<int>();
                foreach (FileTypes.MTA.TextureID t in m.ids)
                {
                    if (!mod.hashChanges.ContainsKey(t.id)) continue;
                    if (changedOffsets.Contains(t.id)) continue;

                    changedOffsets.Add(t.offset);
                    m.Data.seek(t.offset);
                    m.Data.writeInt(mod.hashChanges[t.id]);
                    
                }
                m.SaveToFile(m.SourceFile);
            }
        }

        public static void CheckForMissingTextures(string directoryName)
        {
            string modelPath = directoryName + "model" + Path.DirectorySeparatorChar;
            directoryName = directoryName.Remove(directoryName.Length - 1, 1);
            string modName = directoryName.Split(Path.DirectorySeparatorChar).Last();
            if (!Directory.Exists(modelPath))
            {
                LogHelper.Info(string.Format("Mod '{0}' has no models", modName));
                return;
            }
            string[] folders = Directory.GetDirectories(modelPath);
            foreach (string folder in folders)
            {
                string foldername = folder.Split(Path.DirectorySeparatorChar).Last();
                string modelNut = folder + Path.DirectorySeparatorChar + "model.nut";
                string modelNud = folder + Path.DirectorySeparatorChar + "model.nud";
                string metalNud = folder + Path.DirectorySeparatorChar + "metal.nud";
                if (!File.Exists(modelNut))
                {
                    LogHelper.Error(string.Format("Mod '{0}' is missing its {1} model.nut!", modName, foldername));
                    return;
                }
                if (!File.Exists(modelNud))
                {
                    LogHelper.Error(string.Format("Mod '{0}' is missing its {1} model.nud!", modName, foldername));
                    return;
                }
                List<int> textureIDs = new List<int>();
                List<int> missingTextureIDs = new List<int>();
                int[] dummyTextures = { 0x10100000, 0x10080000, 0x10101000 };
                string missingTextures = "";
                FileTypes.NUT nut = new FileTypes.NUT();
                nut.Read(modelNut);
                bool duplicateTextures = false;
                foreach (FileTypes.NutTexture t in nut.Textures)
                {
                    if (!textureIDs.Contains(t.HashId)) textureIDs.Add(t.HashId);
                    else duplicateTextures = true;
                }
                if (duplicateTextures) LogHelper.Warning(string.Format("The model.nut in mod '{0}' part {1} has multiple textures with the same TextureIDs. This is likely not intentional.", modName, foldername));
                FileTypes.NUD nud = new FileTypes.NUD();
                nud.Read(modelNud);
                foreach (FileTypes.NUD.MatTexture t in nud.allTextures)
                {
                    if (!textureIDs.Contains(t.hash))
                    {
                        if (!missingTextureIDs.Contains(t.hash) && !dummyTextures.Contains(t.hash)) missingTextureIDs.Add(t.hash);
                    }
                }
                if (missingTextureIDs.Count > 0)
                {
                    foreach (int i in missingTextureIDs)
                    {
                        if (missingTextures.Length > 0) missingTextures += ", ";
                        missingTextures += "0x" + i.ToString("x8");
                    }
                    LogHelper.Error(string.Format("The model.nud of mod {0} part {1} is missing the following textures: {2}. This may cause errors with the model.", modName, foldername, missingTextures));
                    missingTextures = "";
                    missingTextureIDs.Clear();
                }
                if (!File.Exists(metalNud))
                {
                    LogHelper.Warning(string.Format("Mod '{0}' does not have a {1} metal.nud! If it needs one, it likely wont work properly.", modName, foldername));
                    return;
                }
                nud = new FileTypes.NUD();
                nud.Read(modelNud);
                foreach (FileTypes.NUD.MatTexture t in nud.allTextures)
                {
                    if (!textureIDs.Contains(t.hash))
                    {
                        if (!missingTextureIDs.Contains(t.hash) && !dummyTextures.Contains(t.hash)) missingTextureIDs.Add(t.hash);
                    }
                }
                if (missingTextureIDs.Count > 0)
                {
                    foreach (int i in missingTextureIDs)
                    {
                        if (missingTextures.Length > 0) missingTextures += ", ";
                        missingTextures += "0x" + i.ToString("x8");
                    }
                    LogHelper.Error(string.Format("The metal.nud of mod {0} part {1} is missing the following textures: {2}. This may cause errors with the model.", modName, foldername, missingTextures));
                    missingTextures = "";
                    missingTextureIDs.Clear();
                }
            }
        }
    }
}
