using System;

namespace jvmsharp.classfile
{
    struct LocalVariableTableEntry
    {
        UInt16 startPc;
        UInt16 length;
        UInt16 nameIndex;
        UInt16 descriptorIndex;
        UInt16 index;

        public LocalVariableTableEntry(UInt16 startPc, UInt16 length, UInt16 nameIndex, UInt16 descriptorIndex, UInt16 index)
        {
            this.startPc = startPc;
            this.length = length;
            this.nameIndex = nameIndex;
            this.descriptorIndex = descriptorIndex;
            this.index = index;
        }
    }

    class LocalVariableTableAttribute : AttributeInfoInterface
    {
        LocalVariableTableEntry[] localVariableTable;

        public void readInfo(ref ClassReader reader)
        {
            UInt16 localVariableTableLength = reader.readUint16();
            localVariableTable = new LocalVariableTableEntry[localVariableTableLength];
            for (int i = 0; i < localVariableTableLength; i++)
            {
                localVariableTable[i] = new LocalVariableTableEntry(reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16());
            }
        }
    }
}
