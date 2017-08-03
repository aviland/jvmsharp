using System;

namespace jvmsharp.classfile
{
    class SourceFileAttribute : AttributeInfoInterface
    {
        ConstantPool cp;
        UInt16 sourceFileIndex;

        public SourceFileAttribute(ConstantPool cp)
        {
            this.cp = cp;
        }

        public void readInfo(ref ClassReader reader)
        {
            sourceFileIndex = reader.readUint16();
        }

        public string FileName()
        {
            return cp.getUtf8(sourceFileIndex);
        }
    }
}
