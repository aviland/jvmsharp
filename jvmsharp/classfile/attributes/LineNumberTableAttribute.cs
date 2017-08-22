namespace jvmsharp.classfile
{
    struct LineNumberTableEntry
    {
        public ushort startPc;
        public ushort lineNumber;

        public LineNumberTableEntry(ushort startPc, ushort lineNumber)
        {
            this.startPc = startPc;
            this.lineNumber = lineNumber;
        }
    }

    class LineNumberTableAttribute : AttributeInfoInterface
    {
        LineNumberTableEntry[] lineNumberTable;

        public override void readInfo(ref ClassReader reader)
        {
            ushort lineNumberTableLength = reader.readUint16();
            lineNumberTable = new LineNumberTableEntry[lineNumberTableLength];
            for (int i = 0; i < lineNumberTableLength; i++)
            {
                lineNumberTable[i] = new LineNumberTableEntry(reader.readUint16(), reader.readUint16());
            }
        }

        public int GetLineNumber(int pc)
        {
            for (int i = lineNumberTable.Length - 1; i >= 0; i--)
            {
                LineNumberTableEntry entry = lineNumberTable[i];
                if (pc >= entry.startPc)
                {
                    return entry.lineNumber;
                }
            }
            return -1;
        }
    }
}
