namespace jvmsharp.classfile
{
    struct InnerClassInfo
    {
        ushort innerClassInfoIndex;
        ushort outerClassInfoIndex;
        ushort innerNameIndex;
        ushort innerClassAccessFlags;

        public InnerClassInfo(ushort innerClassInfoIndex, ushort outerClassInfoIndex, ushort innerNameIndex, ushort innerClassAccessFlags)
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
            ushort numberOfClasses = reader.readUint16();
            classes = new InnerClassInfo[numberOfClasses];
            for (int i = 0; i < classes.Length; i++)
            {
                classes[i] = new InnerClassInfo(reader.readUint16(), reader.readUint16(), reader.readUint16(), reader.readUint16());
            }
        }
    }
}
