using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KamiModpackBuilder.FileTypes;
using System.IO;
using ZLibNet;

namespace KamiModpackBuilder.Objects
{
    public class AutoMTBFix
    {
        public struct Nus3BankFile
        {
            public string fighterName;
            public string fileName;
            public ushort cxxNum;
        }
        private List<Nus3BankFile> _Nus3Banks;
        private SmashProjectManager _SmashProjectManager;
        private MTB _MTB;
        private uint nusId, nusIdPos;
        private uint correctNusId, defaultNusId;

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
                if ((ushort)entry[3] - 1 < cXXSlot - 1) {
                    while (internalIds.Count < cXXSlot - 1)
                        internalIds.Add(InternalId);
                    internalIds.Add(customNusId);
                }
                else {
                    internalIds[cXXSlot - 1] = customNusId;
                }
                entry[3] = (ushort)internalIds.Count;
                entry[4] = internalIds;
                _MTB.modifiyExistingEntry(entryNum, entry);
            }
        }
        
        public void Run() {
            for (int i = 0; i < _Nus3Banks.Count; ++i) {
                string currentSoundFile = _Nus3Banks[i].fileName;
                ushort cxxNum = _Nus3Banks[i].cxxNum;
                getNusbankIdInfo(currentSoundFile);
                getNUS3ID(Path.GetFileName(currentSoundFile), cxxNum);
                int correctNusType = NUS3BANK.GetNUS3Type(currentSoundFile);
                if (correctNusId != nusId) {
                    FileData nus = new FileData(currentSoundFile);
                    nus.Endian = Endianness.Little;
                    nus.seek((int)nusIdPos);
                    nus.writeInt((int)correctNusId);
                    nus.SaveToFile(currentSoundFile);
                }
                int entryNum = _MTB.findByDefaultInternalAndNusType((int)defaultNusId, correctNusType);
                if (cxxNum > 0) {
                    EntryEdit(_Nus3Banks[i].fighterName, defaultNusId, (ushort)correctNusType, correctNusId, entryNum, cxxNum);
                }
                cxxNum = 0;
            }
            _MTB.save();
        }

        private void getNUS3ID(string NUS3Name, ushort cxxNum) {
            bool isCXX = false;
            uint defaultInternalID = 0;
            string search = "";
            if (cxxNum > 0) search = "_cxx";
            string NUS3NameNoCXX = null;
            if (search.Length > 0){
                isCXX = true;
                NUS3NameNoCXX = NUS3Name.Replace(search, string.Empty);
            }
            StreamReader f = File.OpenText(Globals.PathHelper.GetApplicationDirectory() + "tools" + Path.DirectorySeparatorChar + "NUS3-IDs.db");
            while (!f.EndOfStream){
                string line = f.ReadLine();
                if (line.Contains(NUS3Name) && line[NUS3Name.Length] == ':') {
                    correctNusId = uint.Parse(line.Substring(NUS3Name.Length + 1));
                    defaultNusId = uint.Parse(line.Substring(NUS3Name.Length + 1));
                    return;
                }
                if (isCXX && line.Contains(NUS3NameNoCXX) && line[NUS3NameNoCXX.Length] == ':')
                    defaultInternalID = uint.Parse(line.Substring(NUS3NameNoCXX.Length + 1));
            }
            if (isCXX){
                uint customInternalID = cxxNum;
                correctNusId = (customInternalID << 16) + defaultInternalID;
                defaultNusId = defaultInternalID;
                return;
            }
        }
        
        private void getNusbankIdInfo(string fname) {
            FileData nus3 = new FileData(fname);
            nus3.Endian = Endianness.Little;
            if (nus3.Magic() != "NUS3") {//NUS3
                nus3.seek(0);
                byte[] data = nus3.read(nus3.eof());
                byte[] nus3_decom = ZLibCompressor.DeCompress(data);
                FileOutput newf = new FileOutput();
                newf.writeBytes(nus3_decom);
                newf.save(fname);
                data = null;
                nus3_decom = null;
                nus3 = new FileData(fname);
                nus3.Endian = Endianness.Little;
            }
            else
                nus3.seek(0);

            nus3.seek(4);
            uint size = nus3.readUInt();
            nus3.skip(8);
            uint tocSize = nus3.readUInt();
            uint contentCount = nus3.readUInt();
            uint offset = 0x14 + tocSize;
            uint propOffset = 0, propSize = 0, binfOffset = 0, binfSize = 0, grpOffset = 0, grpSize = 0,
                dtonOffset = 0, dtonSize = 0, toneOffset = 0, toneSize = 0, junkOffset = 0, junkSize = 0,
                markOffset = 0, markSize = 0, packOffset = 0, packSize = 0;
            for (int i = 0; i < contentCount; ++i) {
                string content = nus3.readString(nus3.pos(),4);
                nus3.skip(4);
                uint contentSize = nus3.readUInt();
                if (content.Equals("PROP"))
                {
                    propOffset = offset;
                    propSize = contentSize;
                }
                else if (content.Equals("BINF"))
                {
                    binfOffset = offset;
                    binfSize = contentSize;
                }
                else if (content.Equals("GRP "))
                {
                    grpOffset = offset;
                    grpSize = contentSize;
                }
                else if (content.Equals("DTON"))
                {
                    dtonOffset = offset;
                    dtonSize = contentSize;
                }
                else if (content.Equals("TONE"))
                {
                    toneOffset = offset;
                    toneSize = contentSize;
                }
                else if (content.Equals("JUNK"))
                {
                    junkOffset = offset;
                    junkSize = contentSize;
                }
                else if (content.Equals("MARK"))
                {
                    markOffset = offset;
                    markSize = contentSize;
                }
                else if (content.Equals("PACK"))
                {
                    packOffset = offset;
                    packSize = contentSize;
                }
                offset += 8 + contentSize;
            }

            nus3.seek((int)binfOffset + 16);
            byte binfStringSize = nus3.readByte();
            string binfString = nus3.readString(nus3.pos(), binfStringSize - 1);
            nus3.skip(binfStringSize);
            int padding = (binfStringSize + 1) % 4;
            if (padding != 0)
                nus3.skip(Math.Abs(padding - 4));
            nusIdPos = (uint)nus3.pos();
            nusId = nus3.readUInt();
            nus3 = null;
        }
    }
}
