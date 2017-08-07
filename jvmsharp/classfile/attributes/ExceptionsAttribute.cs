namespace jvmsharp.classfile
{
    class ExceptionsAttribute : AttributeInfoInterface
    {
        ushort[] exceptionIndexTable;

        public void readInfo(ref ClassReader reader)
        {
            exceptionIndexTable = reader.readUint16s();
        }

        ushort[] ExceptionIndexTable() { return exceptionIndexTable; }
    }
}
