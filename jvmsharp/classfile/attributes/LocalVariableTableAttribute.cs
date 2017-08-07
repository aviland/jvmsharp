namespace jvmsharp.classfile
{
    struct LocalVariableTableEntry
    {
        ushort startPc;
        ushort length;
        ushort nameIndex;
        ushort descriptorIndex;
        ushort index;

        public LocalVariableTableEntry(ushort startPc, ushort length, ushort nameIndex, ushort descriptorIndex, ushort index)
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
            ushort localVariableTableLength = reader.readUint16();
            localVariableTable = new LocalVariableTableEntry[localVariableTableLength];
            for (int i = 0; i < localVariableTableLength; i++)
            {
                localVariableTable[i] = new LocalVariableTableEntry(reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16());
            }
        }
    }
}
