﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;
using SFGraphics.GLObjects.Textures;
using SFGraphics.GLObjects.Textures.TextureFormats;
using KamiModpackBuilder.Globals;

namespace KamiModpackBuilder.FileTypes
{
    public class TextureSurface
    {
        public List<byte[]> mipmaps = new List<byte[]>();
        public uint cubemapFace = 0; //Not set currently
    }

    public class NutTexture
    {
        //Each texture should contain either 1 or 6 surfaces
        //Each surface should contain (1 <= n <= 255) mipmaps
        //Each surface in a texture should have the same amount of mipmaps and dimensions for them

        public List<TextureSurface> surfaces = new List<TextureSurface>();

        public byte MipMapsPerSurface
        {
            get { return (byte)surfaces[0].mipmaps.Count; }
        }

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

        // Loading mip maps is only supported for DDS currently.
        public bool isDds = false;

        public int HashOffset;

        public int Width;
        public int Height;

        public PixelInternalFormat pixelInternalFormat;
        public OpenTK.Graphics.OpenGL.PixelFormat pixelFormat;
        public PixelType pixelType = PixelType.UnsignedByte;

        public uint DdsCaps2
        {
            get
            {
                if (surfaces.Count == 6)
                    return (uint)DDS.DDSCAPS2.CUBEMAP_ALLFACES;
                else
                    return (uint)0;
            }
        }

        //Return a list containing every mipmap from every surface
        public List<byte[]> GetAllMipmaps()
        {
            List<byte[]> mipmaps = new List<byte[]>();
            foreach (TextureSurface surface in surfaces)
            {
                foreach (byte[] mipmap in surface.mipmaps)
                {
                    mipmaps.Add(mipmap);
                }
            }
            return mipmaps;
        }

        //Move channel 0 to channel 3 (ABGR -> BGRA)
        public void SwapChannelOrderUp()
        {
            foreach (byte[] mip in GetAllMipmaps())
            {
                for (int t = 0; t < mip.Length; t += 4)
                {
                    byte t1 = mip[t];
                    mip[t] = mip[t + 1];
                    mip[t + 1] = mip[t + 2];
                    mip[t + 2] = mip[t + 3];
                    mip[t + 3] = t1;
                }
            }
        }

        //Move channel 3 to channel 0 (BGRA -> ABGR)
        public void SwapChannelOrderDown()
        {
            foreach (byte[] mip in GetAllMipmaps())
            {
                for (int t = 0; t < mip.Length; t += 4)
                {
                    byte t1 = mip[t + 3];
                    mip[t + 3] = mip[t + 2];
                    mip[t + 2] = mip[t + 1];
                    mip[t + 1] = mip[t];
                    mip[t] = t1;
                }
            }
        }

        public int ImageSize
        {
            get
            {
                switch (pixelInternalFormat)
                {
                    case PixelInternalFormat.CompressedRedRgtc1:
                    case PixelInternalFormat.CompressedRgbaS3tcDxt1Ext:
                        return (Width * Height / 2);
                    case PixelInternalFormat.CompressedRgRgtc2:
                    case PixelInternalFormat.CompressedRgbaS3tcDxt3Ext:
                    case PixelInternalFormat.CompressedRgbaS3tcDxt5Ext:
                        return (Width * Height);
                    case PixelInternalFormat.Rgba16:
                        return surfaces[0].mipmaps[0].Length / 2;
                    case PixelInternalFormat.Rgba:
                        return surfaces[0].mipmaps[0].Length;
                    default:
                        return surfaces[0].mipmaps[0].Length;
                }
            }
        }

        public int getNutFormat()
        {
            switch (pixelInternalFormat)
            {
                case PixelInternalFormat.CompressedRgbaS3tcDxt1Ext:
                    return 0;
                case PixelInternalFormat.CompressedRgbaS3tcDxt3Ext:
                    return 1;
                case PixelInternalFormat.CompressedRgbaS3tcDxt5Ext:
                    return 2;
                case PixelInternalFormat.Rgb16:
                    return 8;
                case PixelInternalFormat.Rgba:
                    if (pixelFormat == OpenTK.Graphics.OpenGL.PixelFormat.Rgba)
                        return 14;
                    else
                    if (pixelFormat == OpenTK.Graphics.OpenGL.PixelFormat.AbgrExt)
                        return 16;
                    else
                        return 17;
                case PixelInternalFormat.CompressedRedRgtc1:
                    return 21;
                case PixelInternalFormat.CompressedRgRgtc2:
                    return 22;
                default:
                    throw new NotImplementedException($"Unknown pixel format 0x{pixelInternalFormat:X}");
            }
        }

        public void setPixelFormatFromNutFormat(int typet)
        {
            switch (typet)
            {
                case 0x0:
                    pixelInternalFormat = PixelInternalFormat.CompressedRgbaS3tcDxt1Ext;
                    break;
                case 0x1:
                    pixelInternalFormat = PixelInternalFormat.CompressedRgbaS3tcDxt3Ext;
                    break;
                case 0x2:
                    pixelInternalFormat = PixelInternalFormat.CompressedRgbaS3tcDxt5Ext;
                    break;
                case 8:
                    pixelInternalFormat = PixelInternalFormat.Rgb16;
                    pixelFormat = OpenTK.Graphics.OpenGL.PixelFormat.Rgb;
                    pixelType = PixelType.UnsignedShort565Reversed;
                    break;
                case 12:
                    pixelInternalFormat = PixelInternalFormat.Rgba16;
                    pixelFormat = OpenTK.Graphics.OpenGL.PixelFormat.Rgba;
                    break;
                case 14:
                    pixelInternalFormat = PixelInternalFormat.Rgba;
                    pixelFormat = OpenTK.Graphics.OpenGL.PixelFormat.Rgba;
                    break;
                case 16:
                    pixelInternalFormat = PixelInternalFormat.Rgba;
                    pixelFormat = OpenTK.Graphics.OpenGL.PixelFormat.AbgrExt;
                    break;
                case 17:
                    pixelInternalFormat = PixelInternalFormat.Rgba;
                    pixelFormat = OpenTK.Graphics.OpenGL.PixelFormat.Rgba;
                    break;
                case 21:
                    pixelInternalFormat = PixelInternalFormat.CompressedRedRgtc1;
                    break;
                case 22:
                    pixelInternalFormat = PixelInternalFormat.CompressedRgRgtc2;
                    break;
                default:
                    throw new NotImplementedException($"Unknown nut texture format 0x{typet:X}");
            }
        }
    }

    class NUT
    {
        // Dictionary<hash ID, Texture>
        public Dictionary<int, Texture> glTexByHashId = new Dictionary<int, Texture>();

        public ushort Version = 0x200;

        public Endianness Endian;

        public List<NutTexture> Textures = new List<NutTexture>();
        public FileData SourceData;
        public string SourceFile;

        private bool doRebuild = false;
        private bool hasTextures = false;

        public void SaveToFile(string f)
        {
            if (!doRebuild)
            {
                if (SourceData == null) return;
                SourceData.SaveToFile(f);
            }
            else
            {
                File.WriteAllBytes(f, Rebuild());
            }
        }

        public bool getTextureByID(int hash, out NutTexture suc)
        {
            suc = null;
            foreach (NutTexture t in Textures)
                if (t.HashId == hash)
                {
                    suc = t;
                    return true;
                }

            return false;
        }

        public void ConvertToDdsNut(bool regenerateMipMaps = true)
        {
            for (int i = 0; i < Textures.Count; i++)
            {
                NutTexture originalTexture = (NutTexture)Textures[i];

                // Reading/writing mipmaps is only supported for DDS textures,
                // so we will need to convert all the textures.
                DDS dds = new DDS(originalTexture);
                NutTexture ddsTexture = dds.ToNutTexture();
                ddsTexture.HashId = originalTexture.HashId;

                if (regenerateMipMaps)
                    RegenerateMipmapsFromTexture2D(ddsTexture);

                Textures[i] = ddsTexture;
            }
        }

        public byte[] Rebuild()
        {
            FileOutput o = new FileOutput();
            FileOutput data = new FileOutput();

            //We always want BE for the first six bytes
            o.Endian = Endianness.Big;
            data.Endian = Endianness.Big;

            if (Endian == Endianness.Big)
            {
                o.writeUInt(0x4E545033); //NTP3
            }
            else if (Endian == Endianness.Little)
            {
                o.writeUInt(0x4E545744); //NTWD
            }

            //Most NTWU NUTs are 0x020E, which isn't valid for NTP3/NTWD
            if (Version > 0x0200)
                Version = 0x0200;
            o.writeUShort(Version);

            //After that, endian is used appropriately
            o.Endian = Endian;
            data.Endian = Endian;

            o.writeUShort((ushort)Textures.Count);
            o.writeInt(0);
            o.writeInt(0);

            //calculate total header size
            uint headerLength = 0;

            foreach (NutTexture texture in Textures)
            {
                byte surfaceCount = (byte)texture.surfaces.Count;
                bool isCubemap = surfaceCount == 6;
                if (surfaceCount < 1 || surfaceCount > 6)
                    throw new NotImplementedException($"Unsupported surface amount {surfaceCount} for texture with hash 0x{texture.HashId:X}. 1 to 6 faces are required.");
                else if (surfaceCount > 1 && surfaceCount < 6)
                    throw new NotImplementedException($"Unsupported cubemap face amount for texture with hash 0x{texture.HashId:X}. Six faces are required.");
                byte mipmapCount = (byte)texture.surfaces[0].mipmaps.Count;

                ushort headerSize = 0x50;
                if (isCubemap)
                {
                    headerSize += 0x10;
                }
                if (mipmapCount > 1)
                {
                    headerSize += (ushort)(mipmapCount * 4);
                    while (headerSize % 0x10 != 0)
                        headerSize += 1;
                }

                headerLength += headerSize;
            }

            // write headers+data
            foreach (NutTexture texture in Textures)
            {
                byte surfaceCount = (byte)texture.surfaces.Count;
                bool isCubemap = surfaceCount == 6;
                byte mipmapCount = (byte)texture.surfaces[0].mipmaps.Count;

                uint dataSize = 0;

                foreach (var mip in texture.GetAllMipmaps())
                {
                    dataSize += (uint)mip.Length;
                    while (dataSize % 0x10 != 0)
                        dataSize += 1;
                }

                ushort headerSize = 0x50;
                if (isCubemap)
                {
                    headerSize += 0x10;
                }
                if (mipmapCount > 1)
                {
                    headerSize += (ushort)(mipmapCount * 4);
                    while (headerSize % 0x10 != 0)
                        headerSize += 1;
                }

                o.writeUInt(dataSize + headerSize);
                o.writeUInt(0);
                o.writeUInt(dataSize);
                o.writeUShort(headerSize);
                o.writeUShort(0);

                o.writeByte(0);
                o.writeByte(mipmapCount);
                o.writeByte(0);
                o.writeByte(texture.getNutFormat());
                o.writeShort(texture.Width);
                o.writeShort(texture.Height);
                o.writeInt(0);
                o.writeUInt(texture.DdsCaps2);

                if (Version < 0x0200)
                {
                    o.writeUInt(0);
                }
                else if (Version >= 0x0200)
                {
                    o.writeUInt((uint)(headerLength + data.size()));
                }
                headerLength -= headerSize;
                o.writeInt(0);
                o.writeInt(0);
                o.writeInt(0);

                if (isCubemap)
                {
                    o.writeInt(texture.surfaces[0].mipmaps[0].Length);
                    o.writeInt(texture.surfaces[0].mipmaps[0].Length);
                    o.writeInt(0);
                    o.writeInt(0);
                }

                if (texture.getNutFormat() == 14 || texture.getNutFormat() == 17)
                {
                    texture.SwapChannelOrderDown();
                }

                for (byte surfaceLevel = 0; surfaceLevel < surfaceCount; ++surfaceLevel)
                {
                    for (byte mipLevel = 0; mipLevel < mipmapCount; ++mipLevel)
                    {
                        int ds = data.size();
                        data.writeBytes(texture.surfaces[surfaceLevel].mipmaps[mipLevel]);
                        data.align(0x10);
                        if (mipmapCount > 1 && surfaceLevel == 0)
                            o.writeInt(data.size() - ds);
                    }
                }
                o.align(0x10);

                if (texture.getNutFormat() == 14 || texture.getNutFormat() == 17)
                {
                    texture.SwapChannelOrderUp();
                }

                o.writeBytes(new byte[] { 0x65, 0x58, 0x74, 0x00 }); // "eXt\0"
                o.writeInt(0x20);
                o.writeInt(0x10);
                o.writeInt(0x00);

                o.writeBytes(new byte[] { 0x47, 0x49, 0x44, 0x58 }); // "GIDX"
                o.writeInt(0x10);
                o.writeInt(texture.HashId);
                o.writeInt(0);

                if (Version < 0x0200)
                {
                    o.writeOutput(data);
                    data = new FileOutput();
                }
            }

            if (Version >= 0x0200)
                o.writeOutput(data);

            return o.getBytes();
        }

        public void Read(string f, bool skipTextures = true)
        {
            SourceFile = f;
            Read(new FileData(f), skipTextures);
        }

        public void Read(FileData d, bool skipTextures = true)
        {
            hasTextures = !skipTextures;
            SourceData = d;
            Endian = Endianness.Big;
            d.Endian = Endian;
            uint magic = d.readUInt();

            if (magic == 0x4E545033)
            {
                ReadNTP3(d, skipTextures);
            }
            else if (magic == 0x4E545755)
            {
                ReadNTWU(d, skipTextures);
            }
            else if (magic == 0x4E545744)
            {
                d.Endian = Endianness.Little;
                ReadNTP3(d, skipTextures);
            }
        }

        public void ReadNTP3(FileData d, bool skipTextures = true)
        {
            d.seek(0x6);

            ushort count = d.readUShort();

            d.skip(0x8);
            int headerPtr = 0x10;

            for (ushort i = 0; i < count; ++i)
            {
                d.seek(headerPtr);

                NutTexture tex = new NutTexture();
                tex.isDds = true;
                tex.pixelInternalFormat = PixelInternalFormat.Rgba32ui;

                int totalSize = d.readInt();
                d.skip(4);
                int dataSize = d.readInt();
                int headerSize = d.readUShort();
                d.skip(2);

                //It might seem that mipmapCount and pixelFormat would be shorts, but they're bytes because they stay in the same place regardless of endianness
                d.skip(1);
                byte mipmapCount = d.readByte();
                d.skip(1);
                tex.setPixelFormatFromNutFormat(d.readByte());
                tex.Width = d.readUShort();
                tex.Height = d.readUShort();
                d.skip(4); //0 in dds nuts (like NTP3) and 1 in gtx nuts; texture type?
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

                int dataOffset = 0;
                if (Version < 0x0200)
                {
                    dataOffset = headerPtr + headerSize;
                    d.readInt();
                }
                else if (Version >= 0x0200)
                {
                    dataOffset = d.readInt() + headerPtr;
                }
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

                if (!skipTextures)
                {
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
                }

                if (Version < 0x0200)
                    headerPtr += totalSize;
                else if (Version >= 0x0200)
                    headerPtr += headerSize;

                Textures.Add(tex);
            }
        }
        
        public void ReadNTWU(FileData d, bool skipTextures = true)
        {
            d.seek(0x6);

            ushort count = d.readUShort();

            d.skip(0x8);
            int headerPtr = 0x10;

            for (ushort i = 0; i < count; ++i)
            {
                d.seek(headerPtr);

                NutTexture tex = new NutTexture();
                tex.pixelInternalFormat = PixelInternalFormat.Rgba32ui;

                int totalSize = d.readInt();
                d.skip(4);
                int dataSize = d.readInt();
                int headerSize = d.readUShort();
                d.skip(2);

                d.skip(1);
                byte mipmapCount = d.readByte();
                d.skip(1);
                tex.setPixelFormatFromNutFormat(d.readByte());
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

                if (!skipTextures)
                {
                    d.skip(4); // padding align 8

                    d.seek(gtxHeaderOffset);
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
                    }
                }

                headerPtr += headerSize;

                Textures.Add(tex);
            }
        }

        public void RefreshGlTexturesByHashId()
        {
            glTexByHashId.Clear();

            foreach (NutTexture tex in Textures)
            {
                if (!glTexByHashId.ContainsKey(tex.HashId))
                {
                    // Check if the texture is a cube map.
                    if (tex.surfaces.Count == 6)
                        glTexByHashId.Add(tex.HashId, CreateTextureCubeMap(tex));
                    else
                        glTexByHashId.Add(tex.HashId, CreateTexture2D(tex));
                }
            }
        }

        public static void RegenerateMipmapsFromTexture2D(NutTexture tex)
        {
            if (!TextureFormatTools.IsCompressed(tex.pixelInternalFormat))
                return;

            //Rendering.OpenTKSharedResources.dummyResourceWindow.MakeCurrent();

            // Create an OpenGL texture with generated mipmaps.
            Texture2D texture2D = new Texture2D();
            texture2D.LoadImageData(tex.Width, tex.Height, tex.surfaces[0].mipmaps[0], (InternalFormat)tex.pixelInternalFormat);

            texture2D.Bind();

            for (int i = 0; i < tex.surfaces[0].mipmaps.Count; i++)
            {
                // Get the image size for the current mip level of the bound texture.
                int imageSize;
                GL.GetTexLevelParameter(TextureTarget.Texture2D, i,
                    GetTextureParameter.TextureCompressedImageSize, out imageSize);

                byte[] mipLevelData = new byte[imageSize];

                // Replace the Nut texture with the OpenGL texture's data.
                GL.GetCompressedTexImage(TextureTarget.Texture2D, i, mipLevelData);
                tex.surfaces[0].mipmaps[i] = mipLevelData;
            }
        }
        /*
        public static bool texIdUsed(int texId)
        {
            foreach (var nut in Runtime.TextureContainers)
                foreach (NutTexture tex in nut.Nodes)
                    if (tex.HashId == texId)
                        return true;
            return false;
        }

        public void ChangeTextureIds(int newTexId)
        {
            // Check if tex ID fixing would cause any naming conflicts. 
            if (TexIdDuplicate4thByte())
            {
                MessageBox.Show("The first six digits should be the same for all textures to prevent duplicate IDs after changing the Tex ID.",
                    "Duplicate Texture ID");
                return;
            }

            foreach (NutTexture tex in Textures)
            {
                Texture originalTexture = glTexByHashId[tex.HashId];
                glTexByHashId.Remove(tex.HashId);

                // Only change the first 3 bytes.
                tex.HashId = tex.HashId & 0xFF;
                int first3Bytes = (int)(newTexId & 0xFFFFFF00);
                tex.HashId = tex.HashId | first3Bytes;

                glTexByHashId.Add(tex.HashId, originalTexture);
            }
        }

        public bool TexIdDuplicate4thByte()
        {
            // Check for duplicates. 
            List<byte> previous4thBytes = new List<byte>();
            foreach (NutTexture tex in Textures)
            {
                byte fourthByte = (byte)(tex.HashId & 0xFF);
                if (!(previous4thBytes.Contains(fourthByte)))
                    previous4thBytes.Add(fourthByte);
                else
                    return true;

            }

            return false;
        }
        */
        public static Texture2D CreateTexture2D(NutTexture nutTexture, int surfaceIndex = 0)
        {
            bool compressedFormatWithMipMaps = TextureFormatTools.IsCompressed(nutTexture.pixelInternalFormat);

            List<byte[]> mipmaps = nutTexture.surfaces[surfaceIndex].mipmaps;

            if (compressedFormatWithMipMaps)
            {
                // HACK: Skip loading mipmaps for non square textures for now.
                // The existing mipmaps don't display properly for some reason.
                if (nutTexture.surfaces[0].mipmaps.Count > 1 && nutTexture.isDds && (nutTexture.Width == nutTexture.Height))
                {
                    // Reading mipmaps past the first level is only supported for DDS currently.
                    Texture2D texture = new Texture2D();
                    texture.LoadImageData(nutTexture.Width, nutTexture.Height, nutTexture.surfaces[surfaceIndex].mipmaps,
                        (InternalFormat)nutTexture.pixelInternalFormat);
                    return texture;
                }
                else
                {
                    // Only load the first level and generate the rest.
                    Texture2D texture = new Texture2D();
                    texture.LoadImageData(nutTexture.Width, nutTexture.Height, mipmaps[0], (InternalFormat)nutTexture.pixelInternalFormat);
                    return texture;
                }
            }
            else
            {
                // Uncompressed.
                Texture2D texture = new Texture2D();
                texture.LoadImageData(nutTexture.Width, nutTexture.Height, mipmaps[0],
                    new TextureFormatUncompressed(nutTexture.pixelInternalFormat, nutTexture.pixelFormat, nutTexture.pixelType));
                return texture;
            }
        }

        public bool ContainsGtxTextures()
        {
            foreach (NutTexture texture in Textures)
            {
                if (!texture.isDds)
                    return true;
            }
            return false;
        }

        public static TextureCubeMap CreateTextureCubeMap(NutTexture t)
        {
            if (TextureFormatTools.IsCompressed(t.pixelInternalFormat))
            {
                // Compressed cubemap with mipmaps.
                TextureCubeMap texture = new TextureCubeMap();
                texture.LoadImageData(t.Width, (InternalFormat)t.pixelInternalFormat,
                    t.surfaces[0].mipmaps, t.surfaces[1].mipmaps, t.surfaces[2].mipmaps,
                    t.surfaces[3].mipmaps, t.surfaces[4].mipmaps, t.surfaces[5].mipmaps);
                return texture;
            }
            else
            {
                // Uncompressed cube map with no mipmaps.
                TextureCubeMap texture = new TextureCubeMap();
                texture.LoadImageData(t.Width, new TextureFormatUncompressed(t.pixelInternalFormat, t.pixelFormat, t.pixelType),
                    t.surfaces[0].mipmaps[0], t.surfaces[1].mipmaps[0], t.surfaces[2].mipmaps[0],
                    t.surfaces[3].mipmaps[0], t.surfaces[4].mipmaps[0], t.surfaces[5].mipmaps[0]);
                return texture;
            }
        }

        public static Bitmap BitmapFromPortraitNut(string filename, bool isNormalMap = false)
        {
            try
            {
                NUT n = new NUT();
                n.Read(filename, false);
                if (n.Textures.Count <= 0) return null;
                Texture2D texture = NUT.CreateTexture2D(n.Textures[0]);
                byte[] d = texture.GetImageData(0);
                if (!isNormalMap) d = RGBAFlipRedAndBlue(d);
                else d = RGBAShiftColorsDown(d);
                return DDS.toBitmap(d, texture.Width, texture.Height, DDS.DDSFormat.RGBA);
            }
            catch(Exception e)
            {
                LogHelper.Error(e.Message);
            }
            return null;
        }

        private static byte[] RGBAFlipRedAndBlue(byte[] data)
        {
            int size = data.Length;
            byte[] output = new byte[size];
            for (int i = 0; i < size - 3; i += 4)
            {
                output[i] = data[i + 2];
                output[i + 1] = data[i + 1];
                output[i + 2] = data[i];
                output[i + 3] = data[i + 3];
            }
            return output;
        }

        private static byte[] RGBAShiftColorsDown(byte[] data)
        {
            int size = data.Length;
            byte[] output = new byte[size];
            for (int i = 0; i < size - 3; i += 4)
            {
                output[i] = data[i + 2];
                output[i + 1] = data[i];
                output[i + 2] = data[i + 1];
                output[i + 3] = data[i + 3];
            }
            return output;
        }
    }
}
