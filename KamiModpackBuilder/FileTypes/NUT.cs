using System;
using System.Collections.Generic;
using System.IO;

namespace KamiModpackBuilder.FileTypes
{
    public class NutTexture
    {
        public int HashId
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        private int id;

        public int HashOffset;

        public int Width;
        public int Height;
    }

    class NUT
    {
        public ushort Version = 0x200;
        public Endianness Endian;
        public List<NutTexture> Textures = new List<NutTexture>();
        public FileData Data;
        public string SourceFile;

        public void SaveToFile(string f)
        {
            if (Data == null) return;
            Data.SaveToFile(f);
        }

        public void Read(string f)
        {
            SourceFile = f;
            Read(new FileData(f));
        }

        public void Read(FileData d)
        {
            Data = d;
            Endian = Endianness.Big;
            d.Endian = Endian;
            uint magic = d.readUInt();

            if (magic == 0x4E545033)
            {
                ReadNTP3(d);
            }
            else if (magic == 0x4E545755)
            {
                ReadNTWU(d);
            }
            else if (magic == 0x4E545744)
            {
                d.Endian = Endianness.Little;
                ReadNTP3(d);
            }
        }

        public void ReadNTP3(FileData d)
        {
            d.seek(0x4);

            Version = d.readUShort();
            ushort count = d.readUShort();
            if (Version == 0x100)
                count -= 1;

            d.skip(0x8);
            int headerPtr = 0x10;

            for (ushort i = 0; i < count; ++i)
            {
                d.seek(headerPtr);

                NutTexture tex = new NutTexture();

                int totalSize = d.readInt();
                d.skip(4);
                int dataSize = d.readInt();
                int headerSize = d.readUShort();
                d.skip(2);

                //It might seem that mipmapCount and pixelFormat would be shorts, but they're bytes because they stay in the same place regardless of endianness
                d.skip(1);
                byte mipmapCount = d.readByte();
                d.skip(1);

                d.skip(1);//skipping some texture stuff for now
                //tex.setPixelFormatFromNutFormat(d.readByte());
                tex.Width = d.readUShort();
                tex.Height = d.readUShort();

                d.skip(4);
                uint caps2 = d.readUInt();

                bool isCubemap = false;
                byte surfaceCount = 1;
                if ((caps2 & (uint)DDS.DDSCAPS2.CUBEMAP) == (uint)DDS.DDSCAPS2.CUBEMAP)
                {
                    //Only supporting all six faces
                    if ((caps2 & (uint)DDS.DDSCAPS2.CUBEMAP_ALLFACES) == (uint)DDS.DDSCAPS2.CUBEMAP_ALLFACES)
                    {
                        isCubemap = true;
                        surfaceCount = 6;
                    }
                    else
                    {
                        throw new NotImplementedException($"Unsupported cubemap face amount for texture {i} with hash 0x{tex.HashId:X}. Six faces are required.");
                    }
                }

                int dataOffset = d.readInt() + headerPtr;
                d.readInt();
                d.readInt();
                d.readInt();

                //The size of a single cubemap face (discounting mipmaps). I don't know why it is repeated. If mipmaps are present, this is also specified in the mipSize section anyway.
                int cmapSize1 = 0;
                int cmapSize2 = 0;
                if (isCubemap)
                {
                    cmapSize1 = d.readInt();
                    cmapSize2 = d.readInt();
                    d.skip(8);
                }

                int[] mipSizes = new int[mipmapCount];
                if (mipmapCount == 1)
                {
                    if (isCubemap)
                        mipSizes[0] = cmapSize1;
                    else
                        mipSizes[0] = dataSize;
                }
                else
                {
                    for (byte mipLevel = 0; mipLevel < mipmapCount; ++mipLevel)
                    {
                        mipSizes[mipLevel] = d.readInt();
                    }
                    d.align(0x10);
                }

                d.skip(0x10); //eXt data - always the same

                d.skip(4); //GIDX
                d.readInt(); //Always 0x10
                tex.HashOffset = d.pos();
                tex.HashId = d.readInt();
                d.skip(4); // padding align 8

                if (Version == 0x100)
                    dataOffset = d.pos();

                /*skipping texture stuff for now
                for (byte surfaceLevel = 0; surfaceLevel < surfaceCount; ++surfaceLevel)
                {
                    TextureSurface surface = new TextureSurface();
                    for (byte mipLevel = 0; mipLevel < mipmapCount; ++mipLevel)
                    {
                        byte[] texArray = d.getSection(dataOffset, mipSizes[mipLevel]);
                        surface.mipmaps.Add(texArray);
                        dataOffset += mipSizes[mipLevel];
                    }
                    tex.surfaces.Add(surface);
                }

                if (tex.getNutFormat() == 14 || tex.getNutFormat() == 17)
                {
                    tex.SwapChannelOrderUp();
                }
                */

                Textures.Add(tex);

                headerPtr += headerSize;
            }
        }
        
        public void ReadNTWU(FileData d)
        {
            d.seek(0x4);

            Version = d.readUShort();
            ushort count = d.readUShort();

            d.skip(0x8);
            int headerPtr = 0x10;

            for (ushort i = 0; i < count; ++i)
            {
                d.seek(headerPtr);

                NutTexture tex = new NutTexture();
                //tex.pixelInternalFormat = PixelInternalFormat.Rgba32ui;

                int totalSize = d.readInt();
                d.skip(4);
                int dataSize = d.readInt();
                int headerSize = d.readUShort();
                d.skip(2);

                d.skip(1);
                byte mipmapCount = d.readByte();
                d.skip(1);
                d.skip(1);//tex.setPixelFormatFromNutFormat(d.readByte());
                tex.Width = d.readUShort();
                tex.Height = d.readUShort();
                d.readInt(); //Always 1?
                uint caps2 = d.readUInt();

                bool isCubemap = false;
                byte surfaceCount = 1;
                if ((caps2 & (uint)DDS.DDSCAPS2.CUBEMAP) == (uint)DDS.DDSCAPS2.CUBEMAP)
                {
                    //Only supporting all six faces
                    if ((caps2 & (uint)DDS.DDSCAPS2.CUBEMAP_ALLFACES) == (uint)DDS.DDSCAPS2.CUBEMAP_ALLFACES)
                    {
                        isCubemap = true;
                        surfaceCount = 6;
                    }
                    else
                    {
                        throw new NotImplementedException($"Unsupported cubemap face amount for texture {i} with hash 0x{tex.HashId:X}. Six faces are required.");
                    }
                }

                int dataOffset = d.readInt() + headerPtr;
                int mipDataOffset = d.readInt() + headerPtr;
                int gtxHeaderOffset = d.readInt() + headerPtr;
                d.readInt();

                int cmapSize1 = 0;
                int cmapSize2 = 0;
                if (isCubemap)
                {
                    cmapSize1 = d.readInt();
                    cmapSize2 = d.readInt();
                    d.skip(8);
                }

                int imageSize = 0; //Total size of first mipmap of every surface
                int mipSize = 0; //Total size of mipmaps other than the first of every surface
                if (mipmapCount == 1)
                {
                    if (isCubemap)
                        imageSize = cmapSize1;
                    else
                        imageSize = dataSize;
                }
                else
                {
                    imageSize = d.readInt();
                    mipSize = d.readInt();
                    d.skip((mipmapCount - 2) * 4);
                    d.align(0x10);
                }

                d.skip(0x10); //eXt data - always the same

                d.skip(4); //GIDX
                d.readInt(); //Always 0x10
                tex.HashOffset = d.pos();
                tex.HashId = d.readInt();
                d.skip(4); // padding align 8

                /*d.seek(gtxHeaderOffset);
                
                GTX.GX2Surface gtxHeader = new GTX.GX2Surface();

                gtxHeader.dim = d.readInt();
                gtxHeader.width = d.readInt();
                gtxHeader.height = d.readInt();
                gtxHeader.depth = d.readInt();
                gtxHeader.numMips = d.readInt();
                gtxHeader.format = d.readInt();
                gtxHeader.aa = d.readInt();
                gtxHeader.use = d.readInt();
                gtxHeader.imageSize = d.readInt();
                gtxHeader.imagePtr = d.readInt();
                gtxHeader.mipSize = d.readInt();
                gtxHeader.mipPtr = d.readInt();
                gtxHeader.tileMode = d.readInt();
                gtxHeader.swizzle = d.readInt();
                gtxHeader.alignment = d.readInt();
                gtxHeader.pitch = d.readInt();

                //mipOffsets[0] is not in this list and is simply the start of the data (dataOffset)
                //mipOffsets[1] is relative to the start of the data (dataOffset + mipOffsets[1])
                //Other mipOffsets are relative to mipOffset[1] (dataOffset + mipOffsets[1] + mipOffsets[i])
                int[] mipOffsets = new int[mipmapCount];
                mipOffsets[0] = 0;
                for (byte mipLevel = 1; mipLevel < mipmapCount; ++mipLevel)
                {
                    mipOffsets[mipLevel] = 0;
                    mipOffsets[mipLevel] = mipOffsets[1] + d.readInt();
                }

                for (byte surfaceLevel = 0; surfaceLevel < surfaceCount; ++surfaceLevel)
                {
                    tex.surfaces.Add(new TextureSurface());
                }

                int w = tex.Width, h = tex.Height;
                for (byte mipLevel = 0; mipLevel < mipmapCount; ++mipLevel)
                {
                    int p = gtxHeader.pitch / (gtxHeader.width / w);

                    int size;
                    if (mipmapCount == 1)
                        size = imageSize;
                    else if (mipLevel + 1 == mipmapCount)
                        size = (mipSize + mipOffsets[1]) - mipOffsets[mipLevel];
                    else
                        size = mipOffsets[mipLevel + 1] - mipOffsets[mipLevel];

                    size /= surfaceCount;

                    for (byte surfaceLevel = 0; surfaceLevel < surfaceCount; ++surfaceLevel)
                    {
                        gtxHeader.data = d.getSection(dataOffset + mipOffsets[mipLevel] + (size * surfaceLevel), size);

                        //Real size
                        //Leave the below line commented for now because it breaks RGBA textures
                        //size = ((w + 3) >> 2) * ((h + 3) >> 2) * (GTX.getBPP(gtxHeader.format) / 8);
                        if (size < (GTX.getBPP(gtxHeader.format) / 8))
                            size = (GTX.getBPP(gtxHeader.format) / 8);

                        byte[] deswiz = GTX.swizzleBC(
                            gtxHeader.data,
                            w,
                            h,
                            gtxHeader.format,
                            gtxHeader.tileMode,
                            p,
                            gtxHeader.swizzle
                        );
                        tex.surfaces[surfaceLevel].mipmaps.Add(new FileData(deswiz).getSection(0, size));
                    }

                    w /= 2;
                    h /= 2;

                    if (w < 1)
                        w = 1;
                    if (h < 1)
                        h = 1;
                }*/

                Textures.Add(tex);

                headerPtr += headerSize;
            }
        }
    }
}
