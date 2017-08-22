using System;

namespace jvmsharp.classfile
{
    class MemberInfo
    {
        ConstantPool cp;
        internal ushort accessFlags;
        ushort nameIndex;
        ushort descriptiorIndex;
        internal AttributeInfoInterface[] attributes;

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

        public MemberInfo[] readMembers(ref ClassReader reader, ref ConstantPool cp)
        {
            ushort memberCount = reader.readUint16();
            MemberInfo[] members = new MemberInfo[memberCount];
            for (int i = 0; i < members.Length; i++)
            {
                members[i] = readMember(ref reader, ref cp);
            }
            return members;
        }

        public MemberInfo readMember(ref ClassReader reader, ref ConstantPool cp)
        {
            MemberInfo mi = new MemberInfo();
            mi.cp = cp;
            mi.accessFlags = reader.readUint16();
            mi.nameIndex = reader.readUint16();
            mi.descriptiorIndex = reader.readUint16();
            mi.attributes = new AttributeInfo().readAttributes(ref reader, ref cp);
            return mi;
        }

        public string Name()
        {
            return cp.getUtf8(nameIndex);
        }

        public string Descriptor()
        {
            return cp.getUtf8(descriptiorIndex);
        }

        internal ConstantValueAttribute ConstantValueAttribute()
        {
            foreach (AttributeInfoInterface attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "ConstantValueAttribute":
                        return (ConstantValueAttribute)attrInfo;
                }
            }
            return null;
        }
        public CodeAttribute CodeAttribute()
        {
            foreach (AttributeInfoInterface attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "CodeAttribute":
                        return (CodeAttribute)attrInfo;
                }
            }
            return null;
        }

        public ExceptionsAttribute ExceptionsAttribute()
        {
            foreach (AttributeInfoInterface attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "ExceptionsAttribute":
                        return (ExceptionsAttribute)attrInfo;
                }
            }
            return null;
        }
        public byte[] RuntimeVisibleAnnotationsAttributeData()
        {
            return getUnparsedAttributeData("RuntimeVisibleAnnotations");
        }

        public byte[] RuntimeVisibleParameterAnnotationsAttributeData()
        {
            return getUnparsedAttributeData("RuntimeVisibleParameterAnnotationsAttribute");
        }

        public byte[] AnnotationDefaultAttributeData()
        {
            return getUnparsedAttributeData("AnnotationDefault");
        }

       
        public byte[] getUnparsedAttributeData(string name)
        {
            foreach (AttributeInfoInterface attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "UnparsedAttribute":
                        UnparsedAttribute unparsedAttr = (UnparsedAttribute)attrInfo;
                        if (unparsedAttr.name == name)
                        {
                            return unparsedAttr.info;
                        }
                        break;
                }
            }
            return null;
        }

    }
}
