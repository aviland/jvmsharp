using System;

namespace jvmsharp.classfile
{
    class ConstantMemberrefInfo : ConstantInfo
    {
        protected ConstantPool cp;
        UInt16 classIndex;
        UInt16 nameAndTypeIndex;

        public override void readInfo(ref ClassReader reader)
        {
            classIndex = reader.readUint16();
            nameAndTypeIndex = reader.readUint16();
        }

        public string ClassName()
        {
            return cp.getClassName(classIndex);
        }

        public Tuple<string, string> NameAndDescriptor()
        {
            return cp.getNameAndType(nameAndTypeIndex);
        }
    }

    class ConstantFieldrefInfo : ConstantMemberrefInfo
    {
        public ConstantFieldrefInfo(ConstantPool cp)
        {
            this.cp = cp;
        }
    }

    class ConstantMethodrefInfo : ConstantMemberrefInfo
    {
        public ConstantMethodrefInfo(ConstantPool cp)
        {
            this.cp = cp;
        }
    }

    class ConstantInterfaceMethodrefInfo : ConstantMemberrefInfo
    {
        public ConstantInterfaceMethodrefInfo(ConstantPool cp)
        {
            this.cp = cp;
        }
    }
}
