using System;

namespace jvmsharp.classfile
{
    interface AttributeInfoInterface
    {
        void readInfo(ref ClassReader reader);
    }

    class AttributeTable
    {
      internal  AttributeInfoInterface[] attributes;
        public AttributeInfoInterface[] readAttributes(ref ClassReader reader, ConstantPool cp)
        {
            ushort attributesCount = reader.readUint16();
            AttributeInfoInterface[] attributes = new AttributeInfoInterface[attributesCount];
            for (int i = 0; i < attributesCount; i++)
            {
                attributes[i] = readAttribute(ref reader, cp);
            }
            return attributes;
        }

        AttributeInfoInterface readAttribute(ref ClassReader reader, ConstantPool cp)
        {
            ushort attrNameIndex = reader.readUint16();
            string attrName = cp.getUtf8(attrNameIndex);
            UInt32 attrLen = reader.readUint32();
            AttributeInfoInterface attrInfo = newAttributeInfo(attrName, attrLen, cp);
            if (attrInfo == null)
            {
                attrInfo = new UnparsedAttribute(attrName, attrLen);
            }
            attrInfo.readInfo(ref reader);
            return attrInfo;
        }

        AttributeInfoInterface newAttributeInfo(string attrName, UInt32 attrLen, ConstantPool cp)
        {
            switch (attrName)
            {
                case "BootstrapMethods":
                    return new BootstrapMethodsAttribute();
                case "Code": return new CodeAttribute(cp);
                case "ConstantValue":
                    return new ConstantValueAttribute();
                case "Deprecated":
                    return new DeprecatedAttribute();
                case "EnclosingMethod":
                    return new EnclosingMethodAttribute(cp);
                case "Exceptions":
                    return new ExceptionsAttribute();
                case "InnerClasses":
                    return new InnerClassesAttribute();
                case "LineNumberTable":
                    return new LineNumberTableAttribute();
                case "LocalVariableTable":
                    return new LocalVariableTableAttribute();
                case "LocalVariableTypeTable":
                    return new LocalVariableTypeTableAttribute();
                case "Signature":
                    return new SignatureAttribute(cp);
                case "SourceFile":
                    return new SourceFileAttribute(cp);
                case "Synthetic":
                    return new SyntheticAttribute();
                default:
                    return null;
            }
        }

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

    }
}
