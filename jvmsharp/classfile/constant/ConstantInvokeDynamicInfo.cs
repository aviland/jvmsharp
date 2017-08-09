using System;

namespace jvmsharp.classfile
{
    class ConstantInvokeDynamicInfo : ConstantInfo
    {
        ConstantPool cp;
        ushort bootstrapMethodAttrIndex;
        ushort nameAndTypeIndex;

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

        public Tuple<ushort, ushort[]> BootstrapMethodInfo()
        {
        //    Console.WriteLine(cp.cf == null);
            BootstrapMethodsAttribute bmAttr = cp.cf.BootstrapMethodsAttribute();
            BootstrapMethod bm = bmAttr.bootstrapMethods[bootstrapMethodAttrIndex];
            return Tuple.Create<UInt16, UInt16[]>(bm.GetBootstrapMethodRef(), bm.GetBootstrapArguments());
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
