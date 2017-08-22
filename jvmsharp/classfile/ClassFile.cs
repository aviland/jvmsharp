using System;

namespace jvmsharp.classfile
{
    class ClassFile
    {
        // uint magic;
        internal ushort minorVersion;
        internal ushort majorVersion;
        internal ConstantPool constantPool;//常量池
        internal UInt16 accessFlags;
        internal ushort thisClass;//该类信息
        internal ushort superClass;//超类信息
        internal ushort[] interfaces;//接口信息
        internal MemberInfo[] fields;//字段信息
        internal MemberInfo[] methods;//方法信息
        internal AttributeInfoInterface[] attributes;

        public ClassFile Parse(ref byte[] classData)//二进制数据解析
        {
            ClassReader cr = new ClassReader(ref classData);//类读取器初始化
            ClassFile cf = new ClassFile();
            cf.read(ref cr);
            return cf;
        }

        public void read(ref ClassReader reader)
        {
            readAndCheckMaigc(ref reader);//魔数验证
            readAndCheckVersion(ref reader);//版本验证
            readConstantPool(ref reader);//常量池读取
            accessFlags = reader.readUint16();
            thisClass = reader.readUint16();
            superClass = reader.readUint16();
            interfaces = reader.readUint16s();
            fields = new MemberInfo().readMembers(ref reader, ref constantPool);
            methods = new MemberInfo().readMembers(ref reader,ref  constantPool);
            attributes = new AttributeInfo().readAttributes(ref reader, ref constantPool);
        }

        public void readAndCheckMaigc(ref ClassReader reader)
        {
            uint magic = reader.readUint32();
            if (magic != 0xCAFEBABE)
            {
                throw new Exception("java.lang.ClassFormatError: magic!");
            }
        }

        public void readAndCheckVersion(ref ClassReader reader)
        {
            minorVersion = reader.readUint16();
            majorVersion = reader.readUint16();
            switch (majorVersion)
            {
                case 45: return;
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                    if (minorVersion == 0) return;
                    break;
            }
            throw new Exception("java.lang.UnsupportedClassVersionError!");
        }

        public void readConstantPool(ref ClassReader reader)
        {
            constantPool = new ConstantPool(this);
            constantPool.read(ref reader);
        }

        public ushort MinorVersion() { return minorVersion; }

        public ushort MajorVersion() { return majorVersion; }

        public ConstantPool ConstantPool() { return constantPool; }

        public string ClassName() { return constantPool.getClassName(thisClass); }

        public string SuperClassName()
        {
            if (superClass > 0)
                return constantPool.getClassName(superClass);
            return "no super class";
        }

        public MemberInfo[] Fields() { return fields; }

        public MemberInfo[] Methods() { return methods; }

        public string[] InterfacesNames()
        {
            string[] interfacesNames = new string[interfaces.Length];
            for (int i = 0; i < interfaces.Length; i++)
            {
                interfacesNames[i] = constantPool.getClassName(interfaces[i]);
            }
            return interfacesNames;
        }

        internal SourceFileAttribute SourceFileAttribute()
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

        public UInt16 AccessFlags() { return accessFlags; }

        public AttributeInfoInterface[] Attributes() { return attributes; }
    }
}