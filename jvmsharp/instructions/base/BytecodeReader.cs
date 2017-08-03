using System;

namespace jvmsharp.instructions
{
    struct BytecodeReader
    {
        private byte[] code;
        private int pc;

        public void Reset(ref byte[] code, ref int pc)
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
            return Convert.ToSByte(ReadUint8());
        }

        public UInt16 ReadUint16()
        {
            byte b1 = ReadUint8();
            byte b2 = ReadUint8();
            return (UInt16)((b1 << 8) | b2);
        }

        public Int16 ReadInt16()
        {
            return (Int16)ReadUint16();
        }

        public Int32 ReadInt32()
        {
            byte b1 = ReadUint8();
            byte b2 = ReadUint8();
            byte b3 = ReadUint8();
            byte b4 = ReadUint8();
            return b1 << 24 | b2 << 16 | b3 << 8 | b4;
        }

        public Int32[] ReadInt32s(int n)
        {
            int[] ints = new int[n];
            foreach (int i in ints)
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
