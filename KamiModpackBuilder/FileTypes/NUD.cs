using System;
using System.Collections.Generic;
using System.IO;

namespace KamiModpackBuilder.FileTypes
{
    class NUD
    {
        public Endianness Endian;

        public int boneCount = 0;
        public bool hasBones = false;
        public float[] boundingSphere = new float[4];
        public List<Mesh> meshes = new List<Mesh>();
        public List<MatTexture> allTextures = new List<MatTexture>();
        public FileData Data;
        public string SourceFile;

        public void SaveToFile(string f)
        {
            if (Data == null) return;
            Data.SaveToFile(f);
        }

        // Helpers for reading
        private struct ObjectData
        {
            public int singlebind;
            public int polyCount;
            public int positionb;
            public string name;
        }

        public struct PolyData
        {
            public int polyStart;
            public int vertStart;
            public int verAddStart;
            public int vertCount;
            public int vertSize;
            public int UVSize;
            public int polyCount;
            public int polySize;
            public int polyFlag;
            public int texprop1;
            public int texprop2;
            public int texprop3;
            public int texprop4;
        }

        public class Mesh
        {
            public List<Polygon> Nodes;
        }

        public class Polygon
        {
            public List<Material> materials;
        }

        public class Material
        {
            public List<MatTexture> textures;
        }

        public class MatTexture
        {
            public int hash;
            public int hashPos;
        }

        public void Read(string filename)
        {
            FileData fileData = new FileData(filename);

            Data = fileData;
            SourceFile = filename;

            fileData.Endian = Endianness.Big;
            fileData.seek(0);

            // read header
            string magic = fileData.readString(0, 4);

            if (magic.Equals("NDWD"))
                fileData.Endian = Endianness.Little;

            Endian = fileData.Endian;

            fileData.seek(0xA);
            int polysets = fileData.readUShort();
            boneCount = fileData.readUShort();
            fileData.skip(2);  // somethingsets
            int polyClumpStart = fileData.readInt() + 0x30;
            int polyClumpSize = fileData.readInt();
            int vertClumpStart = polyClumpStart + polyClumpSize;
            int vertClumpSize = fileData.readInt();
            int vertaddClumpStart = vertClumpStart + vertClumpSize;
            int vertaddClumpSize = fileData.readInt();
            int nameStart = vertaddClumpStart + vertaddClumpSize;
            boundingSphere[0] = fileData.readFloat();
            boundingSphere[1] = fileData.readFloat();
            boundingSphere[2] = fileData.readFloat();
            boundingSphere[3] = fileData.readFloat();

            // object descriptors

            ObjectData[] obj = new ObjectData[polysets];
            List<float[]> boundingSpheres = new List<float[]>();
            int[] boneflags = new int[polysets];
            for (int i = 0; i < polysets; i++)
            {
                float[] boundingSphere = new float[8];
                boundingSphere[0] = fileData.readFloat();
                boundingSphere[1] = fileData.readFloat();
                boundingSphere[2] = fileData.readFloat();
                boundingSphere[3] = fileData.readFloat();
                boundingSphere[4] = fileData.readFloat();
                boundingSphere[5] = fileData.readFloat();
                boundingSphere[6] = fileData.readFloat();
                boundingSphere[7] = fileData.readFloat();
                boundingSpheres.Add(boundingSphere);
                int temp = fileData.pos() + 4;
                fileData.seek(nameStart + fileData.readInt());
                obj[i].name = (fileData.readString());
                // read name string
                fileData.seek(temp);
                boneflags[i] = fileData.readInt();
                obj[i].singlebind = fileData.readShort();
                obj[i].polyCount = fileData.readUShort();
                obj[i].positionb = fileData.readInt();
            }

            // reading polygon data
            int meshIndex = 0;
            foreach (var o in obj)
            {
                Mesh m = new Mesh();
                meshes.Add(m);
                m.Nodes = new List<Polygon>();
                //m.Text = o.name;
                //m.boneflag = boneflags[meshIndex];
                //m.singlebind = (short)o.singlebind;
                //m.boundingSphere = boundingSpheres[meshIndex++];

                for (int i = 0; i < o.polyCount; i++)
                {
                    PolyData polyData = new PolyData();

                    polyData.polyStart = fileData.readInt() + polyClumpStart;
                    polyData.vertStart = fileData.readInt() + vertClumpStart;
                    polyData.verAddStart = fileData.readInt() + vertaddClumpStart;
                    polyData.vertCount = fileData.readUShort();
                    polyData.vertSize = fileData.readByte();
                    polyData.UVSize = fileData.readByte();
                    polyData.texprop1 = fileData.readInt();
                    polyData.texprop2 = fileData.readInt();
                    polyData.texprop3 = fileData.readInt();
                    polyData.texprop4 = fileData.readInt();
                    polyData.polyCount = fileData.readUShort();
                    polyData.polySize = fileData.readByte();
                    polyData.polyFlag = fileData.readByte();
                    fileData.skip(0xC);

                    int temp = fileData.pos();

                    // read vertex
                    //Polygon poly = ReadVertex(fileData, polyData, o);
                    Polygon poly = new Polygon();
                    m.Nodes.Add(poly);

                    poly.materials = ReadMaterials(fileData, polyData, nameStart);

                    fileData.seek(temp);
                }
            }
            foreach (Mesh m in meshes)
            {
                if (m.Nodes == null) continue;
                foreach (Polygon p in m.Nodes)
                {
                    foreach (Material mat in p.materials)
                    {
                        foreach (MatTexture t in mat.textures)
                        {
                            bool found = false;
                            for (ushort j = 0; j < allTextures.Count; ++j)
                            {
                                if (t.hashPos == allTextures[j].hashPos)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (found) continue;
                            allTextures.Add(t);
                        }
                    }
                }
            }
        }

        public static List<Material> ReadMaterials(FileData d, PolyData p, int nameOffset)
        {
            int propoff = p.texprop1;
            List<Material> mats = new List<Material>();

            while (propoff != 0)
            {
                d.seek(propoff);

                Material m = new Material();
                mats.Add(m);
                m.textures = new List<MatTexture>();

                d.skip(4);//m.Flags = (uint)d.readInt();
                d.skip(4);
                d.skip(2);//m.srcFactor = d.readUShort();
                ushort texCount = d.readUShort();
                d.skip(2);//m.dstFactor = d.readUShort();
                d.skip(1);//m.alphaTest = d.readByte();
                d.skip(1);//m.alphaFunction = d.readByte();

                d.skip(1); // unknown
                d.skip(1);//m.RefAlpha = d.readByte();
                d.skip(2);//m.cullMode = d.readUShort();
                d.skip(4); // padding
                d.skip(4);//m.unkownWater = d.readInt();
                d.skip(4);//m.zBufferOffset = d.readInt();

                for (ushort i = 0; i < texCount; i++)
                {
                    MatTexture tex = new MatTexture();
                    tex.hashPos = d.pos();
                    tex.hash = d.readInt();
                    d.skip(6); // padding?
                    d.skip(2);//tex.mapMode = d.readUShort();
                    d.skip(1);//tex.wrapModeS = d.readByte();
                    d.skip(1);//tex.wrapModeT = d.readByte();
                    d.skip(1);//tex.minFilter = d.readByte();
                    d.skip(1);//tex.magFilter = d.readByte();
                    d.skip(1);//tex.mipDetail = d.readByte();
                    d.skip(1);//tex.unknown = d.readByte();
                    d.skip(4); // padding?
                    d.skip(2);//tex.unknown2 = d.readShort();
                    m.textures.Add(tex);
                }

                int head = 0x20;

                if (d.Endian != Endianness.Little)
                    while (head != 0)
                    {
                        head = d.readInt();
                        int nameStart = d.readInt();

                        string name = d.readString(nameOffset + nameStart, -1);

                        int pos = d.pos();
                        int valueCount = d.readInt();
                        d.skip(4);

                        // Material properties should always have 4 values. Use 0 for remaining values.
                        float[] values = new float[4];
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (i < valueCount)
                                values[i] = d.readFloat();
                            else
                                values[i] = 0;
                        }
                        //m.entries.Add(name, values);

                        d.seek(pos);

                        if (head == 0)
                            d.skip(0x20 - 8);
                        else
                            d.skip(head - 8);
                    }

                if (propoff == p.texprop1)
                    propoff = p.texprop2;
                else if (propoff == p.texprop2)
                    propoff = p.texprop3;
                else if (propoff == p.texprop3)
                    propoff = p.texprop4;
            }

            return mats;
        }
    }
}
