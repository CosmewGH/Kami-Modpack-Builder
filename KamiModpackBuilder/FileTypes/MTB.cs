using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiModpackBuilder.FileTypes
{
    class MTB
    {
        private string filename;
        private uint header;
        private List<object[]> entries;
        private uint offsetStart;
        private List<uint> offsetTable;

        public MTB(string fname) {
            filename = fname;
            header = 0x0042544D;
            entries = new List<object[]>();

            object[] entry = new object[5];

            entry[0] = "";
            entry[1] = 2045;
            entry[2] = GetNUS3Type("_se");
            entry[3] = 1;
            entry[4] = new List<uint> { 2046 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 2052;
            entry[2] = GetNUS3Type("_se_common");
            entry[3] = 1;
            entry[4] = new List<uint> { 2053 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 2133;
            entry[2] = GetNUS3Type("_se");
            entry[3] = 1;
            entry[4] = new List<uint> { 2134 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 2135;
            entry[2] = GetNUS3Type("_se_common");
            entry[3] = 1;
            entry[4] = new List<uint> { 2136 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 2138;
            entry[2] = GetNUS3Type("_se");
            entry[3] = 1;
            entry[4] = new List<uint> { 2139 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 6039;
            entry[2] = GetNUS3Type("_vc");
            entry[3] = 1;
            entry[4] = new List<uint> { 6045 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 6051;
            entry[2] = GetNUS3Type("_vc");
            entry[3] = 1;
            entry[4] = new List<uint> { 6056 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 6059;
            entry[2] = GetNUS3Type("_vc");
            entry[3] = 1;
            entry[4] = new List<uint> { 6060 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 6062;
            entry[2] = GetNUS3Type("_vc");
            entry[3] = 7;
            entry[4] = new List<uint> { 6063, 6064, 6065, 6066, 6067, 6068, 6069 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 6133;
            entry[2] = GetNUS3Type("_vc_ouen");
            entry[3] = 7;
            entry[4] = new List<uint> { 6135, 6136, 6137, 6138, 6139, 6140, 6141 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 6100;
            entry[2] = GetNUS3Type("_vc_ouen");
            entry[3] = 1;
            entry[4] = new List<uint> { 6142 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 6165;
            entry[2] = GetNUS3Type("_vc");
            entry[3] = 1;
            entry[4] = new List<uint> { 6166 };
            addNewEntry(entry);
            entry = new object[5];
            entry[0] = "";
            entry[1] = 6170;
            entry[2] = GetNUS3Type("_vc");
            entry[3] = 1;
            entry[4] = new List<uint> { 6171 };
            addNewEntry(entry);
        }

        private int GetNUS3Type(string NUS3Name)
        {
            if (NUS3Name.Contains("_se") && NUS3Name.Contains("_common")) return 4;
            else if (NUS3Name.Contains("_vc") && NUS3Name.Contains("_ouen")) return 3;
            else if (NUS3Name.Contains("_vc") && !NUS3Name.Contains("_se") && !NUS3Name.Contains("_ouen")) return 2;
            else if (NUS3Name.Contains("_se") && NUS3Name.Contains("_vc") && !NUS3Name.Contains("_common")) return 1;
            else return 5;
        }

        public void recalcTable()
        {
            offsetStart = (uint)(entries.Count * 0x4) + 0x10;
            offsetTable = new List<uint>();
            uint currentOffset = 0;
            foreach (object[] i in entries)
            {
                offsetTable.Add(currentOffset);
                currentOffset += 0x1A + (0x4 * (uint)(i[3]));
            }
        }

        public void addNewEntry(object[] entry)
        {
            entries.Add(entry);
            recalcTable();
        }

        public object[] getEntry(int entryNum) {
            if (entryNum < entries.Count) return entries[entryNum];
            return null;
        }

        public int modifiyExistingEntry(int entryNum, object[] entry) {
            if (entryNum != -1) {
                entries[entryNum] = entry;
                return 0;
            }
            return -1;
        }

        public void removeEntry(int entryNumber) {
            entries.RemoveAt(entryNumber);
            recalcTable();
        }

        public int findByDefaultInternalAndNusType(int defaultInternal, int nusType) {
            int entryCount = 0;
            foreach (object[] entry in entries) {
                if (defaultInternal == (int)entry[1] && nusType == (int)entry[2]) return entryCount;
                entryCount += 1;
            }
            return -1;
        }

        public void save() {
            recalcTable();
            uint entryCount = (uint)entries.Count;
            FileOutput f = new FileOutput();
            f.Endian = System.IO.Endianness.Little;
            f.writeUInt(header);
            f.writeUInt(entryCount);
            f.writeUInt(offsetStart - 0x10);
            foreach (uint i in offsetTable)
                f.writeUInt(i);
            foreach (object[] i in entries) {
                string name = (string)i[0];
                uint defaultInternal = (uint)i[1];
                ushort nusType = (ushort)i[2];
                ushort nusCount = (ushort)i[3];
                List<uint> internalIds = (List<uint>)i[4];
                f.writeString(name);
                f.writeBytes(new byte[0x10 - name.Length]);
                f.writeUInt(defaultInternal);
                f.writeUShort(nusType);
                f.writeUShort(nusCount);
                foreach (uint j in internalIds)
                    f.writeUInt(j);
                f.writeUShort(0);
            }
            f.save(filename);
        }
    }
}
