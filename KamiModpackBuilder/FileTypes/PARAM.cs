using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using KamiModpackBuilder.Globals;

namespace KamiModpackBuilder.FileTypes
{
    public class PARAM
    {
        public enum ParameterType : byte
        {
            s8 = 1,
            u8 = 2,
            s16 = 3,
            u16 = 4,
            s32 = 5,
            u32 = 6,
            f32 = 7,
            str = 8,
            group = 0x20
        }

        private string fileLoaded = string.Empty;
        public List<ValuesWrapper> Nodes = new List<ValuesWrapper>();

        public void LoadFile(string file)
        {
            if (File.Exists(file))
            {
                fileLoaded = file;
                Nodes.Clear();
                ParseParams(file);
            }
        }

        public void SaveFile(string filepath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            using (FileStream stream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(0x0000FFFF);
                    writer.Write(0);
                    foreach (ValuesWrapper node in Nodes)
                    {

                        byte[] data = null;
                        if (node is ValuesWrapper)
                            data = ((ValuesWrapper)node).GetBytes();
                        else if (node is GroupWrapper)
                            data = ((GroupWrapper)node).GetBytes();

                        if (data != null)
                            writer.Write(data, 0, data.Length);
                    }
                }
            }
        }

        public void SaveFile()
        {
            SaveFile(fileLoaded);
        }

        private void ParseParams(string filepath)
        {
            using (FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var reader = new BinaryReader(stream))
                {
                    stream.Seek(0x08, SeekOrigin.Begin);
                    var wrp = new ValuesWrapper();

                    while (stream.Position != stream.Length)
                    {
                        ParameterType type = (ParameterType)stream.ReadByte();
                        switch (type)
                        {
                            case ParameterType.s8:
                                wrp.Parameters.Add(new ParamEntry(reader.ReadSByte(), type));
                                break;
                            case ParameterType.u8:
                                wrp.Parameters.Add(new ParamEntry(reader.ReadByte(), type));
                                break;
                            case ParameterType.s16:
                                wrp.Parameters.Add(new ParamEntry(reader.ReadInt16().Reverse(), type));
                                break;
                            case ParameterType.u16:
                                wrp.Parameters.Add(new ParamEntry(reader.ReadUInt16().Reverse(), type));
                                break;
                            case ParameterType.s32:
                                wrp.Parameters.Add(new ParamEntry(reader.ReadInt32().Reverse(), type));
                                break;
                            case ParameterType.u32:
                                wrp.Parameters.Add(new ParamEntry(reader.ReadUInt32().Reverse(), type));
                                break;
                            case ParameterType.f32:
                                wrp.Parameters.Add(new ParamEntry(reader.ReadSingle().Reverse(), type));
                                break;
                            case ParameterType.str:
                                int tmp = reader.ReadInt32().Reverse();
                                wrp.Parameters.Add(new ParamEntry(new string(reader.ReadChars(tmp)), type));
                                break;
                            case ParameterType.group:
                                if (wrp.Parameters.Count > 0)
                                {
                                    wrp.Wrap();
                                    Nodes.Add(wrp);
                                }
                                wrp = new GroupWrapper();
                                ((GroupWrapper)wrp).EntryCount = reader.ReadInt32().Reverse();
                                break;
                            default:
                                throw new NotImplementedException($"unk typecode: {type} at offset: {stream.Position:X}");
                        }
                    }
                    wrp.Wrap();
                    Nodes.Add(wrp);
                }
            }
        }

        /// <summary>
        /// Returns if the specified file is a valid PARAM file that can be manipulated.
        /// </summary>
        /// <param name="relativePath">Path to the file, starting from the 'data' folder. (Or equivalent 'data' region folders)</param>
        /// <returns>If the specified file is a valid PARAM file.</returns>
        public static bool IsParamFile(string relativePath)
        {
            if (!relativePath.EndsWith(".bin"))
                return false;

            //Per folder
            if (relativePath.StartsWith("param/") ||
                relativePath.StartsWith("render/default/") ||
                (relativePath.StartsWith("stage/") && relativePath.Contains("param/")) ||
                relativePath.StartsWith("ui/render/")
                )
                return true;

            //By name
            string name = relativePath.Substring(relativePath.LastIndexOf("/") + 1);
            if (name == "CollisionAttribute_cafe.bin" ||
                name == "StageParam_test1.bin" ||
                name == "StageParam_test2.bin" ||
                name == "render_common_param.bin" ||
                name == "light_set_param.bin" ||
                name == "render_param.bin" ||
                 name == "render_special_param.bin")
                return true;

            return false;
        }

        public class GroupWrapper : ValuesWrapper
        {
            static GroupWrapper()
            {
            }
            public GroupWrapper()
            {

            }

            public int EntryCount { get; set; }
            public List<ParameterType> types = new List<ParameterType>();

            public override void Wrap()
            {
                var groups = Parameters.Chunk(EntryCount);

                foreach (ParamEntry ent in groups.ElementAt(0))
                    types.Add(ent.Type);

                Parameters.Clear();
                int i = 0;
                foreach (ParamEntry[] thing in groups)
                {
                    Nodes.Add(new ValuesWrapper() { Parameters = thing.ToList() });
                    i++;
                }
            }
            public override byte[] GetBytes()
            {
                List<byte> output = new List<byte>() { 0x20 };
                output.AddRange(BitConverter.GetBytes(EntryCount).Reverse());

                foreach (ValuesWrapper node in Nodes)
                {
                    foreach (ParamEntry val in node.Parameters)
                    {
                        output.AddRange(val.GetBytes());
                    }
                }
                return output.ToArray();
            }
        }

        public class ValuesWrapper
        {
            public ValuesWrapper()
            {
                Parameters = new List<ParamEntry>();
            }
            static ValuesWrapper()
            {

            }

            public List<ValuesWrapper> Nodes = new List<ValuesWrapper>();
            public virtual void Wrap() { }
            public virtual byte[] GetBytes()
            {
                var output = new byte[0];
                foreach (ParamEntry param in Parameters)
                {
                    output = output.Concat(param.GetBytes()).ToArray();
                }
                return output;
            }
            public List<ParamEntry> Parameters { get; set; }
        }

        public class ParamEntry
        {
            public ParamEntry(object value, ParameterType type)
            {
                Value = value;
                Type = type;
            }
            public ParameterType Type { get; set; }
            public object Value { get; set; }
            public int Size
            {
                get
                {
                    switch (this.Type)
                    {
                        case ParameterType.s8:
                        case ParameterType.u8:
                            return 2;
                        case ParameterType.s16:
                        case ParameterType.u16:
                            return 3;
                        case ParameterType.s32:
                        case ParameterType.u32:
                        case ParameterType.f32:
                            return 5;
                        case ParameterType.str:
                            return ((string)this.Value).Length + 1;
                        default:
                            return 0;
                    }
                }
            }
            public byte[] GetBytes()
            {
                List<byte> data = new List<byte>();
                data.Add((byte)this.Type);
                switch (this.Type)
                {
                    case ParameterType.s8:
                        data.Add((byte)((sbyte)this.Value));
                        return data.ToArray();
                    case ParameterType.u8:
                        data.Add((byte)this.Value);
                        return data.ToArray();
                    case ParameterType.s16:
                        data.AddRange(BitConverter.GetBytes((short)this.Value).Reverse());
                        return data.ToArray();
                    case ParameterType.u16:
                        data.AddRange(BitConverter.GetBytes((ushort)this.Value).Reverse());
                        return data.ToArray();
                    case ParameterType.s32:
                        data.AddRange(BitConverter.GetBytes((int)this.Value).Reverse());
                        return data.ToArray();
                    case ParameterType.u32:
                        data.AddRange(BitConverter.GetBytes((uint)this.Value).Reverse());
                        return data.ToArray();
                    case ParameterType.f32:
                        data.AddRange(BitConverter.GetBytes((float)this.Value).Reverse());
                        return data.ToArray();
                    case ParameterType.str:
                        data.AddRange(BitConverter.GetBytes(((string)this.Value).Length).Reverse());
                        data.AddRange(Encoding.ASCII.GetBytes((string)this.Value));
                        return data.ToArray();
                    default:
                        return null;
                }
            }
        }
    }

}
