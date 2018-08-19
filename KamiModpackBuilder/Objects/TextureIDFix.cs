using System;
using System.Collections.Generic;
using System.IO;

namespace KamiModpackBuilder.Objects
{
    class TextureIDFix
    {
        public class Mod
        {
            public string[] nudFiles;
            public string[] nutFiles;
            public Dictionary<int, int> hashChanges = new Dictionary<int, int>();

            public Mod(string directoryName)
            {
                nutFiles = Directory.GetFiles(directoryName, "*.nut", SearchOption.AllDirectories);
                nudFiles = Directory.GetFiles(directoryName, "*.nud", SearchOption.AllDirectories);
            }
        }

        public List<Mod> modList = new List<Mod>();
        public List<int> usedTextureIDs = new List<int>();

        public void ChangeTextureID(Mod mod, ushort id)
        {
            ushort textureNum = 0;
            List<FileTypes.NUT> nuts = new List<FileTypes.NUT>();
            List<FileTypes.NUD> nuds = new List<FileTypes.NUD>();
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
                    }
                }
            }
            for (int i = 0; i < mod.nudFiles.Length; ++i)
            {
                FileTypes.NUD n = new FileTypes.NUD();
                n.Read(mod.nudFiles[i]);
                nuds.Add(n);/*
                foreach (FileTypes.NUD.MatTexture t in n.allTextures)
                {
                    if (!mod.hashChanges.ContainsKey(t.hash))
                    {
                        mod.hashChanges.Add(t.hash, (int)((t.hash & 0xFFFF0000) | (id << 8) | textureNum));
                        ++textureNum;
                        Globals.LogHelper.Error(String.Format("Texture IDs in NUD files not matching Texture IDs in Nut files!\r\n{0}", mod.nudFiles[i]));
                    }
                }*/
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
        }
    }
}
