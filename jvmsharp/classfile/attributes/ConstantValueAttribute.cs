namespace jvmsharp.classfile
{
    class ConstantValueAttribute : AttributeInfoInterface
    {
        ushort constantValueIndex;

        public override void readInfo(ref ClassReader reader)
        {
            constantValueIndex = reader.readUint16();
        }

        public ushort ConstantValueIndex() { return constantValueIndex; }
    }
}
