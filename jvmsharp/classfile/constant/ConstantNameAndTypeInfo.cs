namespace jvmsharp.classfile
{
    class ConstantNameAndTypeInfo:ConstantInfo
    {
        public ushort nameIndex;
        public ushort descriptorIndex;

        public override void readInfo(ref ClassReader reader)
        {
            nameIndex = reader.readUint16();
            descriptorIndex = reader.readUint16();
        }
    }
}
