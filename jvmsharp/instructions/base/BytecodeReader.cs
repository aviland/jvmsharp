using System;

namespace jvmsharp.instructions
{
    struct BytecodeReader
    {
        internal byte[] code;
        internal int pc;

        public void Reset(byte[] code, int pc)
        {
            this.code = code;
            this.pc = pc;
        }

        public byte ReadUint8()
        {
            byte newd = code[pc];
            pc++;
            return newd;
        }

        public int GetPC()
        {
            return pc;
        }

        public sbyte ReadInt8()
        {
            /*      byte b = ReadUint8();
                   if (b > 127)//将 byte 转为 sbyte。当 byte 小于 128 时其值保持不变，大于等于 128 时就将其减去 256
                   {
                       return (sbyte)(b - 256);
                   }
                   else return (sbyte)b;*/
            return (sbyte)ReadUint8();
        }

        public UInt16 ReadUint16()
        {
            UInt16 b1 = ReadUint8();
            UInt16 b2 = ReadUint8();
            return Convert.ToUInt16((b1 << 8) | b2);
        }

        public short ReadInt16()
        {
          /*  ushort s = ReadUint16();
            if (s > 32767)
            {
                return (short)(s - 65536);
            }*/
            return (short)ReadUint16();
        }

        public int ReadInt32()
        {
            byte b1 = ReadUint8();
            byte b2 = ReadUint8();
            byte b3 = ReadUint8();
            byte b4 = ReadUint8();
            return b1 << 24 | b2 << 16 | b3 << 8 | b4;
        }

        public int[] ReadInt32s(int n)
        {
            int[] ints = new int[n];
            for (int i =0;i<n;i++)
            {
                ints[i] = ReadInt32();
            }
            return ints;
        }

        public void SkipPadding()
        {
            for (; pc % 4 != 0;)
                ReadUint8();
        }
    }
}
