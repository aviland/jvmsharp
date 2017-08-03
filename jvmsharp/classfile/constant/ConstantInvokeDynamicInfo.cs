using System;

namespace jvmsharp.classfile
{
    class ConstantInvokeDynamicInfo : ConstantInfo
    {
        ConstantPool cp;
        UInt16 bootstrapMethodAttrIndex;
        UInt16 nameAndTypeIndex;

        public ConstantInvokeDynamicInfo(ConstantPool cp)
        {
            this.cp = cp;
        }

        public override void readInfo(ref ClassReader reader)
        {
            bootstrapMethodAttrIndex = reader.readUint16();
            nameAndTypeIndex = reader.readUint16();
        }

        public Tuple<string, string> NameAndType()
        {
            return cp.getNameAndType(nameAndTypeIndex);
        }

        public Tuple<UInt16, UInt16[]> BootstrapMethodInfo()
        {
            BootstrapMethodsAttribute bmAttr = cp.cf.BootstrapMethodsAttribute();
            BootstrapMethod bm = bmAttr.bootstrapMethods[bootstrapMethodAttrIndex];
            return Tuple.Create<UInt16, UInt16[]>(bm.GetBootstrapMethodRef(), bm.GetBootstrapArguments());
        }
    }

    class ConstantMethodHandleInfo : ConstantInfo
    {
        public byte referenceKind;
        public UInt16 referenceIndex;

        public override void readInfo(ref ClassReader reader)
        {
            referenceKind = reader.readUint8();
            referenceIndex = reader.readUint16();
        }
    }

    class ConstantMethodTypeInfo : ConstantInfo
    {
        UInt16 descriptorIndex;
        public override void readInfo(ref ClassReader reader)
        {
            descriptorIndex = reader.readUint16();
        }
    }
}
