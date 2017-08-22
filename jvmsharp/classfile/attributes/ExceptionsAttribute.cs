namespace jvmsharp.classfile
{
    class ExceptionsAttribute : AttributeInfoInterface
    {
        ushort[] exceptionIndexTable;

        public override void readInfo(ref ClassReader reader)
        {
            exceptionIndexTable = reader.readUint16s();
        }

       internal ushort[] ExceptionIndexTable() { return exceptionIndexTable; }
    }
}
