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
            byte b = ReadUint8();
            if (b > 127)//将 byte 转为 sbyte。当 byte 小于 128 时其值保持不变，大于等于 128 时就将其减去 256
            {
                return (sbyte)(b - 256);
            }
            else return (sbyte)b;
        }

        public ushort ReadUint16()
        {
            byte b1 = ReadUint8();
            byte b2 = ReadUint8();
            return (ushort)((b1 << 8) | b2);
        }

        public short ReadInt16()
        {
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
