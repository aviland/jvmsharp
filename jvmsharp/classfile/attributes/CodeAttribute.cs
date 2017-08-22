using System;

namespace jvmsharp.classfile
{
    struct ExceptionTableEntry
    {
        internal ushort startPc;
        internal ushort endPc;
        internal ushort handlerPc;
        internal ushort catchType;

        public ExceptionTableEntry(ushort startPc,         ushort endPc,         ushort handlerPc,         ushort catchType)
        {
            this.startPc = startPc;
            this.endPc = endPc;
            this.handlerPc = handlerPc;
            this.catchType = catchType;
        }
    }

    class CodeAttribute : AttributeInfoInterface
    {
        internal ConstantPool cp;
        internal ushort maxStack;
        internal ushort maxLocals;
        internal byte[] code;
        internal ExceptionTableEntry[] exceptionTable;
        internal AttributeInfoInterface[] attributes;

        public CodeAttribute(ConstantPool cp)
        {
            this.cp = cp;
        }

        public byte[] Code() { return code; }

        public uint MaxStack()
        {
            return maxStack;
        }

        public uint MaxLocals()
        {
            return maxLocals;
        }

        public override void readInfo(ref ClassReader reader)
        {
            maxStack = reader.readUint16();
            maxLocals = reader.readUint16();
            uint codeLength = reader.readUint32();
            code = reader.readBytes(codeLength);
            exceptionTable = readExceptionTable(ref reader);
            attributes = new AttributeInfo().readAttributes(ref reader,ref cp);
        }

        internal ExceptionTableEntry[] ExceptionTable() { return exceptionTable; }

        internal ExceptionTableEntry[] readExceptionTable(ref ClassReader reader)
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

        internal LineNumberTableAttribute LineNumberTableAttribute()
        {
            foreach (var attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "LineNumberTableAttribute":
                        return (LineNumberTableAttribute)attrInfo;
                }
            }
            return null;
        }
    }
}
