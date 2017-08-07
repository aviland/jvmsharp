namespace jvmsharp.classfile
{
    class MemberInfo
    {
        ConstantPool cp;
     internal   ushort accessFlags;
        ushort nameIndex;
        ushort descriptiorIndex;
        AttributeInfoInterface[] attributes;
        /* group 1 */
        #region
        public ConstantValueAttribute ConstantValueAttribute()
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

        public BootstrapMethodsAttribute BootstrapMethodsAttribute()
        {
            foreach (AttributeInfoInterface attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "BootstrapMethodsAttribute":
                        return (BootstrapMethodsAttribute)attrInfo;
                }
            }
            return null;
        }
        #endregion
        /* group 2 */
        #region
        public EnclosingMethodAttribute EnclosingMethodAttribute()
        {
            foreach (AttributeInfoInterface attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "EnclosingMethodAttribute":
                        return (EnclosingMethodAttribute)attrInfo;
                }
            }
            return null;
        }

        public SignatureAttribute SignatureAttribute()
        {
            foreach (AttributeInfoInterface attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "SignatureAttribute":
                        return (SignatureAttribute)attrInfo;
                }
            }
            return null;
        }
        #endregion
        /* group 3 */
        #region
        public SourceFileAttribute SourceFileAttribute()
        {
            foreach (AttributeInfoInterface attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "SourceFileAttribute":
                        return (SourceFileAttribute)attrInfo;
                }
            }
            return null;
        }
        public LineNumberTableAttribute LineNumberTableAttribute()
        {
            foreach (AttributeInfoInterface attrInfo in attributes)
            {
                switch (attrInfo.GetType().Name)
                {
                    case "LineNumberTableAttribute":
                        return (LineNumberTableAttribute)attrInfo;
                }
            }
            return null;
        }
        #endregion
        /* unparsed */
        #region
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
        #endregion

        internal ushort AccessFlags()
        {
            return accessFlags;
        }

        public MemberInfo() {    }

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
            for(int i = 0; i < members.Length; i++)
            {
                members[i] = readMember(ref reader, cp);
            }
            return members;
        }

        public MemberInfo readMember(ref ClassReader reader, ConstantPool cp)
        {
            return new MemberInfo(cp, reader.readUint16(), reader.readUint16(), reader.readUint16(), new AttributeInfo().readAttributes(ref reader, cp));
        }

        public string Name()
        {
            return cp.getUtf8(nameIndex);
        }

        public string Descriptor()
        {
            return cp.getUtf8(descriptiorIndex);
        }
    }
}
