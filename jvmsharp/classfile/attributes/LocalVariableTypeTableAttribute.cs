using System;

namespace jvmsharp.classfile
{
    struct LocalVariableTypeTableEntry
    {
        public UInt16 startPc;
        public UInt16 length;
        public UInt16 nameIndex;
        public UInt16 signatureIndex;
        public UInt16 index;

        public LocalVariableTypeTableEntry(UInt16 startPc, UInt16 length, UInt16 nameIndex, UInt16 signatureIndex, UInt16 index)
        {
            this.startPc = startPc;
            this.length = length;
            this.nameIndex = nameIndex;
            this.signatureIndex = signatureIndex;
            this.index = index;
        }
    }

    struct LocalVariableTypeTableAttribute : AttributeInfoInterface
    {
        LocalVariableTypeTableEntry[] localVariableTypeTable;
        public void readInfo(ref ClassReader reader)
        {
            UInt16 localVariableTypeTableLength = reader.readUint16();
            localVariableTypeTable = new LocalVariableTypeTableEntry[localVariableTypeTableLength];
            for (int i = 0; i < localVariableTypeTableLength; i++)
            {
                localVariableTypeTable[i] = new LocalVariableTypeTableEntry(reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16());
            }
        }
    }
}
