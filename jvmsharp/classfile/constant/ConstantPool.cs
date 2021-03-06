﻿using System;

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

        public ConstantPool(ClassFile cf)
        {
            this.cf = cf;
        }

        public void read(ref ClassReader reader)
        {
            ushort cpCount = reader.readUint16();
            this.constantInfos = new ConstantInfo[cpCount];
            for (int i = 1; i < cpCount; i++)
            {
                this.constantInfos[i] = ConstantInfo.readConstantInfo(ref reader, this);
                switch (this.constantInfos[i].GetType().Name)
                {
                    case "ConstantLongInfo":
                    case "ConstantDoubleInfo": i++; break;
                }
            }
        }

        public ConstantInfo[] Infos()
        {
            return constantInfos;
        }

        ConstantInfo getConstantInfo(ushort index)
        {
            ConstantInfo cpInfo = constantInfos[index];
            if (cpInfo != null)
                return cpInfo;
            throw new Exception("Invalid constant pool index!");
        }

        public Tuple<string, string> getNameAndType(ushort index)
        {
            ConstantNameAndTypeInfo ntInfo = (ConstantNameAndTypeInfo)getConstantInfo(index);
            string name = getUtf8(ntInfo.nameIndex);
            string _type = getUtf8(ntInfo.descriptorIndex);
            return Tuple.Create(name, _type);
        }

        public string getClassName(ushort index)
        {
            ConstantClassInfo classInfo = (ConstantClassInfo)getConstantInfo(index);
            return getUtf8(classInfo.nameIndex);
        }

        public string getUtf8(ushort index)
        {
            ConstantUtf8Info cui = (ConstantUtf8Info)getConstantInfo(index);
            return cui.str;
        }
    }
}
