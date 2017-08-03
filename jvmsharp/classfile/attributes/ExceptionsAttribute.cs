using System;

namespace jvmsharp.classfile
{
    class ExceptionsAttribute : AttributeInfoInterface
    {
        UInt16[] exceptionIndexTable;

        public void readInfo(ref ClassReader reader)
        {
            exceptionIndexTable = reader.readUint16s();
        }

        UInt16[] ExceptionIndexTable() { return exceptionIndexTable; }
    }
}
