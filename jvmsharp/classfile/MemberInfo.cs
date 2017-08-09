using System;

namespace jvmsharp.classfile
{
    class MemberInfo : AttributeTable
    {
        ConstantPool cp;
        internal ushort accessFlags;
        ushort nameIndex;
        ushort descriptiorIndex;

        internal ushort AccessFlags()
        {
            return accessFlags;
        }

        public MemberInfo() { }

        public MemberInfo(ConstantPool cp, ushort accessFlags, ushort nameIndex, ushort descriptiorIndex, AttributeInfoInterface[] attributes)
        {
            this.cp = cp;
            this.accessFlags = accessFlags;
            this.nameIndex = nameIndex;
            this.descriptiorIndex = descriptiorIndex;
            this.attributes = attributes;
        }

        public MemberInfo[] readMembers(ref ClassReader reader, ConstantPool cp)
        {
            ushort memberCount = reader.readUint16();
            MemberInfo[] members = new MemberInfo[memberCount];
            for (int i = 0; i < members.Length; i++)
            {
                members[i] = readMember(ref reader, cp);
            }
            return members;
        }

        public MemberInfo readMember(ref ClassReader reader, ConstantPool cp)
        {
            return new MemberInfo(cp, reader.readUint16(), reader.readUint16(), reader.readUint16(), new AttributeTable().readAttributes(ref reader, cp));
        }

        public string Name()
        {
            return cp.getUtf8(nameIndex);
        }

        public string Descriptor()
        {
            return cp.getUtf8(descriptiorIndex);
        }

        public string Signature()
        {
            var signatureAttr = SignatureAttribute();
            if (signatureAttr != null)
                return signatureAttr.Signature();
            return "";
        }
    }
}
