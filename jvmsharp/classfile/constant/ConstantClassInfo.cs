namespace jvmsharp.classfile
{
    class ConstantClassInfo : ConstantInfo
    {
        public ConstantPool cp;
        public ushort nameIndex;

        public ConstantClassInfo(ConstantPool cp)
        {
            this.cp = cp;
        }

        public override void   readInfo(ref ClassReader reader)
        {
            nameIndex = reader.readUint16();
        }

        public string Name()
        {
            return cp.getUtf8(nameIndex);
        }
    }
}
