using System;

namespace jvmsharp.classfile
{
    class ConstantValueAttribute : AttributeInfoInterface
    {
        UInt16 constantValueIndex;

        public void readInfo(ref ClassReader reader)
        {
            constantValueIndex = reader.readUint16();
        }

        public UInt16 ConstantValueIndex() { return constantValueIndex; }
    }
}
