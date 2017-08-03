using System;

namespace jvmsharp.classfile
{
    struct BootstrapMethod
    {
        private UInt16 bootstrapMethodRef;
        private UInt16[] bootstrapArguments;

        public UInt16 GetBootstrapMethodRef()
        {
            return bootstrapMethodRef;
        }

        public UInt16[] GetBootstrapArguments()
        {
            return bootstrapArguments;
        }

        public BootstrapMethod(UInt16 bootstrapMethodRef, UInt16[] bootstrapArguments)
        {
            this.bootstrapMethodRef = bootstrapMethodRef;
            this.bootstrapArguments = bootstrapArguments;
        }
    }

    class BootstrapMethodsAttribute : AttributeInfoInterface
    {
        public BootstrapMethod[] bootstrapMethods;
        public void readInfo(ref ClassReader reader)
        {
            UInt16 numBootstrapMethods = reader.readUint16();
            bootstrapMethods = new BootstrapMethod[numBootstrapMethods];
            for (int i = 0; i < bootstrapMethods.Length; i++)
            {
                bootstrapMethods[i] = new BootstrapMethod(reader.readUint16(), reader.readUint16s());
            }
        }
    }
}
