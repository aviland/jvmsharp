using System;

namespace jvmsharp.classfile
{
    struct LineNumberTableEntry
    {
        public UInt16 startPc;
        public UInt16 lineNumber;

        public LineNumberTableEntry(UInt16 startPc, UInt16 lineNumber)
        {
            this.startPc = startPc;
            this.lineNumber = lineNumber;
        }
    }

    class LineNumberTableAttribute : AttributeInfoInterface
    {
        LineNumberTableEntry[] lineNumberTable;

        public void readInfo(ref ClassReader reader)
        {
            UInt16 lineNumberTableLength = reader.readUint16();
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
