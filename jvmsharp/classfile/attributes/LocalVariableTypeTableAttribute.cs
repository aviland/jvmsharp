namespace jvmsharp.classfile
{
    struct LocalVariableTypeTableEntry
    {
        public ushort startPc;
        public ushort length;
        public ushort nameIndex;
        public ushort signatureIndex;
        public ushort index;

        public LocalVariableTypeTableEntry(ushort startPc, ushort length, ushort nameIndex, ushort signatureIndex, ushort index)
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
            ushort localVariableTypeTableLength = reader.readUint16();
            localVariableTypeTable = new LocalVariableTypeTableEntry[localVariableTypeTableLength];
            for (int i = 0; i < localVariableTypeTableLength; i++)
            {
                localVariableTypeTable[i] = new LocalVariableTypeTableEntry(reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16());
            }
        }
    }
}
