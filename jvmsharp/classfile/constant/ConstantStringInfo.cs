using System;

namespace jvmsharp.classfile
{
    class ConstantStringInfo : ConstantInfo
    {
        ConstantPool cp;
        UInt16 stringIndex;

        public  ConstantStringInfo(ConstantPool cp)
        {
            this.cp = cp;
        }
        public override void readInfo(ref ClassReader reader)
        {
            stringIndex = reader.readUint16();
        }

        public string String()
        {
            return cp.getUtf8(stringIndex);
        }
    }
}
