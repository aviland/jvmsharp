using System;

namespace jvmsharp.classfile
{
    struct InnerClassInfo
    {
        UInt16 innerClassInfoIndex;
        UInt16 outerClassInfoIndex;
        UInt16 innerNameIndex;
        UInt16 innerClassAccessFlags;

        public InnerClassInfo(UInt16 innerClassInfoIndex, UInt16 outerClassInfoIndex, UInt16 innerNameIndex, UInt16 innerClassAccessFlags)
        {
            this.innerClassInfoIndex = innerClassInfoIndex;
            this.outerClassInfoIndex = outerClassInfoIndex;
            this.innerNameIndex = innerNameIndex;
            this.innerClassAccessFlags = innerClassAccessFlags;
        }
    }

    struct InnerClassesAttribute : AttributeInfoInterface
    {
        InnerClassInfo[] classes;

        public void readInfo(ref ClassReader reader)
        {
            UInt16 numberOfClasses = reader.readUint16();
            classes = new InnerClassInfo[numberOfClasses];
            for (int i = 0; i < classes.Length; i++)
            {
                classes[i] = new InnerClassInfo(reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16());
            }
        }
    }
}
