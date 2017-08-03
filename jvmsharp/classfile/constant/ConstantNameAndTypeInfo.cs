using System;

namespace jvmsharp.classfile
{
    class ConstantNameAndTypeInfo:ConstantInfo
    {
        public UInt16 nameIndex;
        public UInt16 descriptorIndex;

        public override void readInfo(ref ClassReader reader)
        {
            nameIndex = reader.readUint16();
            descriptorIndex = reader.readUint16();
        }
    }
}
