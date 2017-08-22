using System;

namespace jvmsharp.classfile
{
    class ConstantInvokeDynamicInfo : ConstantInfo
    {
        ConstantPool cp;
        ushort bootstrapMethodAttrIndex;
        ushort nameAndTypeIndex;

        public override void readInfo(ref ClassReader reader)
        {
            bootstrapMethodAttrIndex = reader.readUint16();
            nameAndTypeIndex = reader.readUint16();
        }
    }

    class ConstantMethodHandleInfo : ConstantInfo
    {
        public byte referenceKind;
        public ushort referenceIndex;

        public override void readInfo(ref ClassReader reader)
        {
            referenceKind = reader.readUint8();
            referenceIndex = reader.readUint16();
        }
    }

    class ConstantMethodTypeInfo : ConstantInfo
    {
        ushort descriptorIndex;
        public override void readInfo(ref ClassReader reader)
        {
            descriptorIndex = reader.readUint16();
        }
    }
}
