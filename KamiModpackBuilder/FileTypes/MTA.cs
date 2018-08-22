using System;
using System.Collections.Generic;
using System.IO;

namespace KamiModpackBuilder.FileTypes
{
    public class MTA
    {
        /*
        public List<MatEntry> matEntries = new List<MatEntry>();
        */
        public Endianness Endian = Endianness.Big;
        public FileData Data;
        public string SourceFile;
        public struct TextureID
        {
            public int id;
            public int offset;
        }
        public List<TextureID> ids = new List<TextureID>();

        public void SaveToFile(string f)
        {
            if (Data == null) return;
            Data.SaveToFile(f);
        }

        public void Read(string filename, short idStart)
        {
            Data = new FileData(filename);
            SourceFile = filename;

            read(Data, idStart);
        }
        /*
        public void read(FileData f)
        {
            f.Endian = Endian;
            if (f.size() < 4)
                return;

            f.seek(24);
            int matCount = f.readInt();
            int matOffset = f.readInt();
            int returnPos;
            f.seek(matOffset);
            for (int i = 0; i < matCount; i++)
            {
                returnPos = f.pos() + 4;
                f.seek(f.readInt());
                MatEntry tempMatEntry = new MatEntry();
                tempMatEntry.read(f);
                matEntries.Add(tempMatEntry);
                f.seek(returnPos);
            }
        }
        */
        public void read(FileData f, short idStart)
        {
            f.Endian = Endian;
            if (f.size() < 4)
                return;

            while (true)
            {
                if (f.pos() > f.eof() - 8) return;

                int offset = f.pos();
                short idHead = f.readShort();
                if (idHead == idStart)
                {
                    TextureID id = new TextureID();
                    id.offset = offset;
                    id.id = (idHead << 16) + f.readShort();
                    ids.Add(id);
                }
                else
                {
                    f.skip(2);
                }
            }
        }
    }
    /*
    public class MatEntry
    {
        public bool hasPat = false;
        public PatData pat0 = new PatData();

        public void read(FileData f)
        {
            f.skip(16);
            hasPat = (0 != f.readByte());
            f.skip(3);
            int patOffset = f.readInt();
            if (hasPat)
            {
                f.seek(patOffset);
                int patDataPos = f.readInt();
                if (patDataPos != 0)
                {
                    f.seek(patDataPos);
                    pat0.read(f);
                }
            }
        }
    }

    public class PatData
    {
        public struct keyframe
        {
            public int frameNum;
            public int texId;
            public int texIdPos;
        }

        public int defaultTexIdPos;
        public int defaultTexId;
        public int frameCount;
        public List<keyframe> keyframes = new List<keyframe>();
        
        public void read(FileData f)
        {
            keyframe temp;
            defaultTexIdPos = f.pos();
            defaultTexId = f.readInt();
            int keyframeCount = f.readInt();
            int keyframeOffset = f.readInt();
            frameCount = f.readInt() + 1;
            f.skip(4);
            if (keyframeOffset != f.eof())
            {
                f.seek(keyframeOffset);
                for (int i = 0; i < keyframeCount; i++)
                {
                    temp.texIdPos = f.pos();
                    temp.texId = f.readInt();
                    temp.frameNum = f.readInt();
                    keyframes.Add(temp);
                }
            }
        }

    }*/
}