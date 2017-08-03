using System;
using System.Linq;

namespace jvmsharp.classfile
{
    unsafe struct ClassReader
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

        public UInt16 readUint16()
        {
            byte[] newd = new byte[2];

            Array.Copy(data, 0, newd, 0, 2);
            Array.Reverse(newd);//intel cpu是小端芯片，需要倒序
            data = data.Skip(2).ToArray();
            //  Console.WriteLine(BitConverter.ToUInt16(newd, 0));
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
            return BitConverter.ToUInt32(newd, 0);
        }

        public UInt16[] readUint16s()
        {
            UInt16 n = readUint16();
            UInt16[] s = new UInt16[n];
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
