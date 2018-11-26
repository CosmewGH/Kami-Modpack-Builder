using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using KamiModpackBuilder.FileTypes;
using KamiModpackBuilder.Objects;
using KamiModpackBuilder.Globals;

namespace KamiModpackBuilder.Objects
{
    class CharacterDBParam
    {
        private PARAM Param;
        private SmashProjectManager _SmashProjectManager;

        public CharacterDBParam(SmashProjectManager project, bool createNew = true)
        {
            Param = new PARAM();
            if (createNew || !File.Exists(PathHelper.FolderEditorMods + "data(us_en)/param/ui/ui_character_db.bin"))
            {
                _SmashProjectManager = project;
                _SmashProjectManager.ExtractResource("data(us_en)/param/ui/ui_character_db.bin", PathHelper.FolderEditorMods, true);
            }
            Param.LoadFile(PathHelper.FolderEditorMods + "data(us_en)/param/ui/ui_character_db.bin");
        }

        public void SetCharacterSlotCount(int charID, int slotCount, bool increaseOnly = true)
        {
            PARAM.GroupWrapper group = (PARAM.GroupWrapper)Param.Nodes[0];
            for (int i = 0; i < group.Nodes.Count; ++i)
            {
                PARAM.ValuesWrapper values = (PARAM.ValuesWrapper)group.Nodes[i];
                if ((int)values.Parameters[6].Value == charID)
                {
                    if (increaseOnly)
                    {
                        if (slotCount > (uint)values.Parameters[7].Value) values.Parameters[7].Value = (uint)slotCount;
                    }
                    else values.Parameters[7].Value = (uint)slotCount;
                    return;
                }
            }
        }

        public int GetCharacterSlotCount(int charID)
        {
            PARAM.GroupWrapper group = (PARAM.GroupWrapper)Param.Nodes[0];
            for (int i = 0; i < group.Nodes.Count; ++i)
            {
                PARAM.ValuesWrapper values = (PARAM.ValuesWrapper)group.Nodes[i];
                if ((int)values.Parameters[6].Value == charID)
                {
                    return (int)((uint)values.Parameters[7].Value);
                }
            }
            return -1;
        }

        public void SetCharacterSlotNameIndex(int charID, int slot, int nameIndex)
        {
            PARAM.GroupWrapper group = (PARAM.GroupWrapper)Param.Nodes[0];
            for (int i = 0; i < group.Nodes.Count; ++i)
            {
                PARAM.ValuesWrapper values = (PARAM.ValuesWrapper)group.Nodes[i];
                if ((int)values.Parameters[6].Value == charID)
                {
                    values.Parameters[37+slot].Value = (byte)nameIndex;
                    return;
                }
            }
        }

        public void SaveFiles()
        {
            Param.SaveFile(PathHelper.FolderEditorMods + "data/param/ui/ui_character_db.bin");
            Param.SaveFile(PathHelper.FolderEditorMods + "data(us_en)/param/ui/ui_character_db.bin");
            Param.SaveFile(PathHelper.FolderEditorMods + "data(us_fr)/param/ui/ui_character_db.bin");
            Param.SaveFile(PathHelper.FolderEditorMods + "data(us_sp)/param/ui/ui_character_db.bin");
        }
    }
}
