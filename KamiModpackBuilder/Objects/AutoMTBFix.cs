using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KamiModpackBuilder.FileTypes;
using System.IO;

namespace KamiModpackBuilder.Objects
{
    public class AutoMTBFix
    {
        public struct Nus3BankFile
        {
            public string fighterName;
            public string fileName;
        }
        private List<Nus3BankFile> _Nus3Banks;
        private SmashProjectManager _SmashProjectManager;
        MTB _MTB;

        public AutoMTBFix(List<Nus3BankFile> nus3Banks, SmashProjectManager project)
        {
            _Nus3Banks = nus3Banks;
            _SmashProjectManager = project;

            _MTB = new MTB(Globals.PathHelper.FolderEditorMods + "data" + Path.DirectorySeparatorChar + "sound" + Path.DirectorySeparatorChar + "config" + Path.DirectorySeparatorChar + "fightermodelbanktable.mtb");
        }

        private void EntryEdit(string fighterName, uint InternalId, ushort nusType, uint customNusId, int entryNum, ushort cXXSlot)
        {
            if (entryNum == -1) {
                //entry doesnt exist in the current mtb
                List<uint> customInternalIds = new List<uint>();
                for (int soundBankSlot = 0; soundBankSlot < cXXSlot; ++soundBankSlot) {
                    if (soundBankSlot == cXXSlot - 1)
                        customInternalIds.Add(customNusId);
                    else
                        customInternalIds.Add(InternalId);
                }
                object[] entry = new object[] { fighterName, InternalId, nusType, cXXSlot, customInternalIds };
                _MTB.addNewEntry(entry);
            }
            else {
                //existing entry
                object[] entry = _MTB.getEntry(entryNum);
                List<uint> internalIds;
                internalIds = (List<uint>)entry[4];
                if ((ushort)entry[3] - 1 < cXXSlot - 1){
                    while (internalIds.Count < cXXSlot - 1)
                        internalIds.Add(InternalId);
                    internalIds.Add(customNusId);
                }
                else {
                    internalIds[cXXSlot - 1] = customNusId;
                }
                entry[3] = internalIds.Count;
                entry[4] = internalIds;
                _MTB.modifiyExistingEntry(entryNum, entry);
            }
        }
        /*
        public void Run() {
            for (int i = 0; i < _Nus3Banks.Count; ++i) {
                string nameFighter = _Nus3Banks[i].fighterName;
                string currentSoundFile = _Nus3Banks[i].fileName;
                nusId, nusIdPos = getNusbankIdInfo(currentSoundFile);
                correctNusId,defaultNusId = getNUS3ID(filename);
                correctNusType = getNUS3Type(filename);
                if (correctNusId != nusId) {
                    nus = open(os.path.abspath(currentSoundFile.strip('"').strip("'")), 'r+b');
                    nus.seek(nusIdPos);
                    writeu32le(nus, correctNusId);
                    nus.close();
                }
                int entryNum = _MTB.findByDefaultInternalAndNusType(defaultNusId, correctNusType);
                search = re.search("_c([0-9]{2,3})", filename);
                ushort cXXSlot = 0;
                if (search)
                    cXXSlot = (ushort)(search.group(1));
                if (cXXSlot > 0) {
                    EntryEdit(nameFighter, defaultNusId, correctNusType, correctNusId, entryNum, cXXSlot);
                }
                cXXSlot = -1;
            }
            _MTB.save();
        }*/
    }
}
