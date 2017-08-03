using System;

namespace jvmsharp.classfile
{
    struct ExceptionTableEntry
    {
        public UInt16 startPc;
        public UInt16 endPc;
        public UInt16 handlerPc;
        public UInt16 catchType;

        UInt16 StartPc() { return startPc; }
        UInt16 EndPc() { return endPc; }
        UInt16 HandlerPc() { return handlerPc; }
        UInt16 CatchType() { return catchType; }
    }

    class CodeAttribute : AttributeInfoInterface
    {
        private ConstantPool cp;
       private UInt16 maxStack;
       private UInt16 maxLocals;
       private byte[] code;
       private ExceptionTableEntry[] exceptionTable;
       private AttributeInfoInterface[] attributes;

        public byte[] Code() {  return code; }

        public UInt16 MaxStack()
        {
            return maxStack;
        }

        public UInt16 MaxLocals()
        {
            return maxLocals;
        }
        public void SetConstantPool(ConstantPool cp)
        {
            this.cp = cp;
        }
        public void readInfo(ref ClassReader reader)
        {
            maxStack = reader.readUint16();
            maxLocals = reader.readUint16();
            uint codeLength = reader.readUint32();
            code = reader.readBytes(codeLength);
            exceptionTable = readExceptionTable(ref reader);
            attributes = new AttributeInfo().readAttributes(ref reader, cp);
        }

        ExceptionTableEntry[] ExceptionTable() { return exceptionTable; }

        ExceptionTableEntry[] readExceptionTable(ref ClassReader reader)
        {
            UInt16 exceptionTableLength = reader.readUint16();
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
