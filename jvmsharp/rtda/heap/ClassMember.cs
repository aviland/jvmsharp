namespace jvmsharp.rtda.heap
{
    class ClassMember 
    {
        internal ushort accessFlags;
        internal string name;
        internal string descriptor;
        internal Class clas;
        internal string signature;
        internal byte[] annotationData;// RuntimeVisibleAnnotations_attribute

        internal void copyMemberInfo(ref classfile.MemberInfo memberfInfo)
        {
            this.accessFlags = memberfInfo.AccessFlags();
            name = memberfInfo.Name();
            descriptor = memberfInfo.Descriptor();
        }
        public bool IsPublic()
        {
            return 0 != (accessFlags & AccessFlags. ACC_PUBLIC);
        }

        public bool IsPrivate()
        {
            return 0 != (accessFlags & AccessFlags.ACC_PRIVATE);
        }
        public bool IsProtected()
        {
            return 0 != (accessFlags & AccessFlags.ACC_PROTECTED);
        }

        public bool IsStatic()
        {
            return 0 != (accessFlags & AccessFlags.ACC_STATIC);
        }
        public bool IsFinal()
        {
            return 0 != (accessFlags & AccessFlags.ACC_FINAL);
        }
        public bool IsSynthetic()
        {
            return 0 != (accessFlags & AccessFlags.ACC_SYNTHETIC);
        }

        public string Name()
        {
            return name;
        }
        public string Descriptor()
        {
            return descriptor;
        }
        internal string Signature()
        {
            return this.signature;
        }

        internal byte[] AnnotationData()
        {
            return annotationData;
        }
        public Class Class()
        {
            return clas;
        }

        internal bool isAccessibleTo(Class d)
        {
            if (IsPublic())
                return true;
            Class c = clas;
            if (IsProtected())
                return d == c || d.IsSubClassOf(c) || c.getPackageName() == d.getPackageName();
            if (!IsPrivate())
                return c.getPackageName() == d.getPackageName();
            return d == c;
        }

        internal ConstantPool ConstantPool()
        {
            return clas.constantPool;
        }

    }
}
