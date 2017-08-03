using System;

namespace jvmsharp.classfile
{
    abstract class ConstantInfo
    {
        const byte CONSTANT_Class = 7;
        const byte CONSTANT_Filedref = 9;
        const byte CONSTANT_Methodref = 10;
        const byte CONSTANT_InterfaceMethodref = 11;
        const byte CONSTANT_String = 8;
        const byte CONSTANT_Integer = 3;
        const byte CONSTANT_Float = 4;
        const byte CONSTANT_Long = 5;
        const byte CONSTANT_Double = 6;
        const byte CONSTANT_NameAndType = 12;
        const byte CONSTANT_Utf8 = 1;
        const byte CONSTANT_MethodHandle = 15;
        const byte CONSTANT_MethodType = 16;
        const byte CONSTANT_InvokeDynamic = 18;

        public abstract void readInfo(ref ClassReader reader);

        public static ConstantInfo readConstantInfo(ref ClassReader reader, ref ConstantPool cp)
        {
            byte tag = reader.readUint8();
            ConstantInfo c = newConstantInfo(tag, ref cp);
            c.readInfo(ref reader);
            return c;
        }

        public static ConstantInfo newConstantInfo(byte tag, ref ConstantPool cp)
        {
            switch (tag)
            {
                case CONSTANT_Integer: return new ConstantIntegerInfo();
                case CONSTANT_Float: return new ConstantFloatInfo();
                case CONSTANT_Long: return new ConstantLongInfo();
                case CONSTANT_Double: return new ConstantDoubleInfo();
                case CONSTANT_Utf8: return new ConstantUtf8Info();
                case CONSTANT_String: return new ConstantStringInfo(cp);
                case CONSTANT_Class: return new ConstantClassInfo(cp);
                case CONSTANT_Filedref: return new ConstantFieldrefInfo(cp);
                case CONSTANT_Methodref: return new ConstantMethodrefInfo(cp);
                case CONSTANT_InterfaceMethodref: return new ConstantInterfaceMethodrefInfo(cp);
                case CONSTANT_NameAndType: return new ConstantNameAndTypeInfo();
                case CONSTANT_MethodType: return new ConstantMethodTypeInfo();
                case CONSTANT_InvokeDynamic: return new ConstantInvokeDynamicInfo(cp);
                default: throw new Exception("java.lang.ClassFormatError: constant pool tag!");
            }
        }
    }
}
