namespace jvmsharp.classfile
{
    struct BootstrapMethod
    {
        private ushort bootstrapMethodRef;
        private ushort[] bootstrapArguments;

        public BootstrapMethod(ushort bootstrapMethodRef, ushort[] bootstrapArguments)
        {
            this.bootstrapMethodRef = bootstrapMethodRef;
            this.bootstrapArguments = bootstrapArguments;
        }
    }

    class BootstrapMethodsAttribute : AttributeInfoInterface
    {
        public BootstrapMethod[] bootstrapMethods;
        public override void readInfo(ref ClassReader reader)
        {
            ushort numBootstrapMethods = reader.readUint16();
            bootstrapMethods = new BootstrapMethod[numBootstrapMethods];
            for (int i = 0; i < bootstrapMethods.Length; i++)
            {
                bootstrapMethods[i] = new BootstrapMethod(reader.readUint16(), reader.readUint16s());
            }
        }
    }
}
