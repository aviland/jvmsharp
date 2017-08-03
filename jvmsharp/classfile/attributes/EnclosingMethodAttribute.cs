using System;

namespace jvmsharp.classfile
{
    class EnclosingMethodAttribute : AttributeInfoInterface
    {
        ConstantPool cp;
        UInt16 classIndex;
        UInt16 methodIndex;

        public EnclosingMethodAttribute(ConstantPool cp)
        {
            this.cp = cp;
        }

        public void readInfo(ref ClassReader reader)
        {
            classIndex = reader.readUint16();
            methodIndex = reader.readUint16();
        }

        string ClassName()
        {
            return cp.getClassName(classIndex);
        }

        Tuple<string, string> MethodNameAndDescriptor()
        {
            if (methodIndex > 0)
                return cp.getNameAndType(methodIndex);
            else return Tuple.Create<string, string>("", "");
        }
    }
}
