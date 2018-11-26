using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using KamiModpackBuilder.Globals;
using KamiModpackBuilder.FileTypes.MSBT;

namespace KamiModpackBuilder.Objects
{
    class CharacterStringsMSBT
    {
        private MSBT msbt;
        private SmashProjectManager _SmashProjectManager;

        public CharacterStringsMSBT(SmashProjectManager project, bool createNew = true)
        {
            if (createNew || !File.Exists(PathHelper.FolderEditorMods + "data/ui/message/melee.msbt"))
            {
                _SmashProjectManager = project;
                _SmashProjectManager.ExtractResource("data/ui/message/melee.msbt", PathHelper.FolderEditorMods, true);
            }
            msbt = new MSBT(PathHelper.FolderEditorMods + "data/ui/message/melee.msbt");
        }

        public void AddNewCharEntry(string charName, int slotIndex, string CustomName, string BoxingText)
        {
            string labelA = "MmelCharaA_" + slotIndex.ToString("D2") + "_" + charName;
            string labelC = "MmelCharaC_" + slotIndex.ToString("D2") + "_" + charName;
            string labelN = "MmelCharaN_" + slotIndex.ToString("D2") + "_" + charName;
            string labelR = "MmelCharaR_" + slotIndex.ToString("D2") + "_" + charName;
            string stringA = BoxingText.Replace(@"\0", "\0") + "\0";
            string stringC = CustomName.ToUpper().Replace(@"\0", "\0") + "\0";
            string stringN = CustomName.Replace(@"\0", "\0") + "\0";
            string stringR = CustomName.ToUpper().Replace(@"\0", "\0") + "\0";
            if (stringR.Length > 14)
            {
                string[] split = stringR.Split(' ');
                int length = stringR.Length;
                stringR = "";
                for (int i = 0; i < split.Length - 1; ++i)
                {
                    stringR += split[i];
                    if (stringR.Length + split[i + 1].Length > (length / 2) + 2)
                    {
                        stringR += "\n" + split[i + 1];
                        for (; i < split.Length - 2; ++i)
                        {
                            stringR += " " + split[i + 2];
                        }
                    }
                    else stringR += " ";
                }
            }
            Label label;
            int index;
            IEntry entry;
            label = msbt.AddLabel(labelA);
            index = msbt.LBL1.Labels.IndexOf(label);
            entry = (IEntry)msbt.TXT2.Strings[index];
            entry.Value = msbt.FileEncoding.GetBytes(stringA);
            label = msbt.AddLabel(labelC);
            index = msbt.LBL1.Labels.IndexOf(label);
            entry = (IEntry)msbt.TXT2.Strings[index];
            entry.Value = msbt.FileEncoding.GetBytes(stringC);
            label = msbt.AddLabel(labelN);
            index = msbt.LBL1.Labels.IndexOf(label);
            entry = (IEntry)msbt.TXT2.Strings[index];
            entry.Value = msbt.FileEncoding.GetBytes(stringN);
            label = msbt.AddLabel(labelR);
            index = msbt.LBL1.Labels.IndexOf(label);
            entry = (IEntry)msbt.TXT2.Strings[index];
            entry.Value = msbt.FileEncoding.GetBytes(stringR);
        }

        public void SaveFile()
        {
            msbt.Save();
        }
    }
}
