namespace jvmsharp.classfile
{
    class SignatureAttribute : AttributeInfoInterface
    {
        ConstantPool cp;
        ushort signatureIndex;

        public SignatureAttribute(ConstantPool cp)
        {
            this.cp = cp;
        }

        public void readInfo(ref ClassReader reader)
        {
            signatureIndex = reader.readUint16();
        }

        public string Signature()
        {
            return cp.getUtf8(signatureIndex);
        }
    }
}
