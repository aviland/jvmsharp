using System;
using System.Linq;

namespace jvmsharp.classfile
{
    struct ClassReader
    {
        public byte[] data;

        public ClassReader(ref byte[] classData)
        {
            data = classData;
        }

        public byte readUint8()
        {
            byte b = data[0];
            data = data.Skip(1).ToArray();
            return b;
        }

        public ushort readUint16()
        {
            byte[] newd = new byte[2];
            Array.Copy(data, 0, newd, 0, 2);
            Array.Reverse(newd);//intel cpu是小端芯片，需要倒序
            data = data.Skip(2).ToArray();
            return BitConverter.ToUInt16(newd, 0);
        }

        public UInt32 readUint32()
        {
            byte[] newd = new byte[4];
            Array.Copy(data, newd, 4);
            Array.Reverse(newd);
            data = data.Skip(4).ToArray();
            return BitConverter.ToUInt32(newd, 0);
        }


        public UInt64 readUint64()
        {
            byte[] newd = new byte[8];
            Array.Copy(data, newd, 8);
            Array.Reverse(newd);
            data = data.Skip(8).ToArray();
            return BitConverter.ToUInt64(newd, 0);
        }

        public ushort[] readUint16s()
        {
            ushort n = readUint16();
            ushort[] s = new ushort[n];
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = readUint16();
            }
            return s;
        }

        public byte[] readBytes(uint n)
        {
            byte[] bytes = data.Take((int)n).ToArray();
            data = data.Skip((int)n).ToArray();
            return bytes;
        }
    }
}
