using System;

namespace jvmsharp.rtda.heap
{
    struct ConstantInvokeDynamic
    {
        string name;
        string _type;
        UInt16 bootstrapMethodRef; // method handle
        UInt16[] bootstrapArguments;
        ConstantPool cp;

        public ConstantInvokeDynamic(ref ConstantPool cp, classfile.ConstantInvokeDynamicInfo indyInfo)
        {
            Tuple<string, string> t = indyInfo.NameAndType();
            Tuple<UInt16, UInt16[]> boots = indyInfo.BootstrapMethodInfo();
            name = t.Item1;
            _type = t.Item2;
            bootstrapMethodRef = boots.Item1;
            bootstrapArguments = boots.Item2;
            this.cp = cp;
        }
    }
}
