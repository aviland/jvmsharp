namespace jvmsharp.classfile
{
    struct ExceptionTableEntry
    {
        public ushort startPc;
        public ushort endPc;
        public ushort handlerPc;
        public ushort catchType;

        ushort StartPc() { return startPc; }
        ushort EndPc() { return endPc; }
        ushort HandlerPc() { return handlerPc; }
        ushort CatchType() { return catchType; }
    }

    class CodeAttribute : AttributeInfoInterface
    {
        private ConstantPool cp;
       private ushort maxStack;
       private ushort maxLocals;
       private byte[] code;
       private ExceptionTableEntry[] exceptionTable;
       private AttributeInfoInterface[] attributes;

        public CodeAttribute(ConstantPool cp)
        {
            this.cp = cp;
        }

        public byte[] Code() {  return code; }

        public ushort MaxStack()
        {
            return maxStack;
        }

        public ushort MaxLocals()
        {
            return maxLocals;
        }

        public void readInfo(ref ClassReader reader)
        {
            maxStack = reader.readUint16();
            maxLocals = reader.readUint16();
            uint codeLength = reader.readUint32();
            code = reader.readBytes(codeLength);
            exceptionTable = readExceptionTable(ref reader);
            attributes = new AttributeTable().readAttributes(ref reader, cp);
        }

        ExceptionTableEntry[] ExceptionTable() { return exceptionTable; }

        ExceptionTableEntry[] readExceptionTable(ref ClassReader reader)
        {
            ushort exceptionTableLength = reader.readUint16();
            ExceptionTableEntry[] exceptionTable = new ExceptionTableEntry[exceptionTableLength];
            for (int i = 0; i < exceptionTable.Length; i++)
            {
                exceptionTable[i] = new ExceptionTableEntry();
                exceptionTable[i].startPc = reader.readUint16();
                exceptionTable[i].endPc = reader.readUint16();
                exceptionTable[i].handlerPc = reader.readUint16();
                exceptionTable[i].catchType = reader.readUint16();
            }
            return exceptionTable;
        }
    }
}
