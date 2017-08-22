using System;

namespace jvmsharp.classfile
{
    abstract class AttributeInfoInterface
    {
        public abstract void readInfo(ref ClassReader reader);
    }
    class AttributeInfo
    {
        public AttributeInfoInterface[] readAttributes(ref ClassReader reader, ref ConstantPool cp)
        {
            ushort attributesCount = reader.readUint16();
            AttributeInfoInterface[] attributes = new AttributeInfoInterface[attributesCount];
            for (int i = 0; i < attributesCount; i++)
            {
                attributes[i] = readAttribute(ref reader, ref cp);
            }
            return attributes;
        }

        AttributeInfoInterface readAttribute(ref ClassReader reader, ref ConstantPool cp)
        {
            ushort attrNameIndex = reader.readUint16();
            string attrName = cp.getUtf8(attrNameIndex);
            UInt32 attrLen = reader.readUint32();
            AttributeInfoInterface attrInfo = newAttributeInfo(attrName, attrLen, cp);
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
                    return new UnparsedAttribute(attrName,attrLen,null);
            }
        }
    }
}
