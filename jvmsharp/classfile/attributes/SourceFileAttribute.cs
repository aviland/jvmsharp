﻿namespace jvmsharp.classfile
{
    class SourceFileAttribute : AttributeInfoInterface
    {
        ConstantPool cp;
        ushort sourceFileIndex;

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
