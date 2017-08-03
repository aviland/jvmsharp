using System;

namespace jvmsharp.classfile
{

    partial class ConstantPool
    {
        public ConstantInfo[] constantInfos;
        public ClassFile cf;
    }

    partial class ConstantPool
    {
        
        public ConstantPool() { }

        public ConstantPool(ConstantInfo[] ConstantPools)
        {
            constantInfos = ConstantPools;
        }

        public ConstantPool read(ref ClassReader reader)
        {
            UInt16 cpCount = reader.readUint16();
            ConstantPool cp = new ConstantPool();
            cp.constantInfos = new ConstantInfo[cpCount];
            for (int i = 1; i < cpCount; i++)
            {
                cp.constantInfos[i] = ConstantInfo.readConstantInfo(ref reader, ref cp);
                switch (cp.constantInfos[i].GetType().Name)
                {
                    case "ConstantLongInfo":
                    case "ConstantDoubleInfo": i++; break;
                }
            }
            return cp;
        }

        public ConstantInfo[] Infos()
        {
            return constantInfos;
        }

        ConstantInfo getConstantInfo(UInt16 index)
        {
            ConstantInfo cpInfo = constantInfos[index];
            if (cpInfo != null)
                return cpInfo;
            throw new Exception("Invalid constant pool index!");
        }

        public Tuple<string, string> getNameAndType(UInt16 index)
        {
            ConstantNameAndTypeInfo ntInfo = (ConstantNameAndTypeInfo)getConstantInfo(index);
            string name = getUtf8(ntInfo.nameIndex);
            string _type = getUtf8(ntInfo.descriptorIndex);
            return Tuple.Create(name, _type);
        }

        public string getClassName(UInt16 index)
        {
            ConstantClassInfo classInfo = (ConstantClassInfo)getConstantInfo(index);
            return getUtf8(classInfo.nameIndex);
        }

        public string getUtf8(UInt16 index)
        {
            ConstantUtf8Info cui = (ConstantUtf8Info)getConstantInfo(index);
            return cui.str;
        }
    }
}
