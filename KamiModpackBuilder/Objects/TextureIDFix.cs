using System;
using System.Collections.Generic;
using System.IO;
using KamiModpackBuilder.Globals;

namespace KamiModpackBuilder.Objects
{
    class TextureIDFix
    {
        public enum CharacterException { None, Pacman_WiiU, Robin_WiiU}
        
        public class Mod
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

        public List<Mod> modList = new List<Mod>();
        public List<int> usedTextureIDs = new List<int>();

        public void ChangeTextureID(Mod mod, ushort id)
        {
            ushort textureNum = 0;
            int baseID = -1;
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
                        if (baseID == -1) baseID = t.HashId;
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

        public void MassTextureIdFix()
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

                        TextureIDFix.CharacterException exc = TextureIDFix.CharacterException.None;
                        if (fighter.id == 0x32 && !project.IsSwitch) exc = TextureIDFix.CharacterException.Pacman_WiiU;
                        if (fighter.id == 0x2a && !project.IsSwitch) exc = TextureIDFix.CharacterException.Robin_WiiU;

                        TextureIDFix textureIDFix = new TextureIDFix();
                        TextureIDFix.Mod textMod = new TextureIDFix.Mod(ModPath + "model", exc);
                        textureIDFix.ChangeTextureID(textMod, (ushort)xml.TextureID);
                        Globals.Utils.SerializeXMLToFile(xml, PathKami);
                        Globals.LogHelper.Info(String.Format("Changed Texture ID of {0} to {1} successfully.", mod.FolderName, xml.TextureID));
                    }
                    ids.Add((ushort)xml.TextureID);
                }
            }
        }
    }
}
