namespace jvmsharp.classfile
{
    class UnparsedAttribute : AttributeInfoInterface
    {
        public string name;
        public uint length;
        public byte[] info;

        public UnparsedAttribute(  string name, uint length)
        {
            this.name = name;
            this.length = length;
        }

        public UnparsedAttribute(string name, uint length, byte[] info)
        {
            this.name = name;
            this.length = length;
            this.info = info;
        }

        public override void readInfo(ref ClassReader reader)
        {
            info = reader.readBytes(length);
        }

        public byte[] Info()
        {
            return info;
        }
    }
}
