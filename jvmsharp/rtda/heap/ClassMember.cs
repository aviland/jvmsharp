namespace jvmsharp.rtda.heap
{
    class ClassMember : AccessFlags
    {
        protected string name;
        protected string descriptor;
        protected Class clas;
     //   protected string signature;
       // protected byte[] annotationData;// RuntimeVisibleAnnotations_attribute

        protected void copyMemberInfo(ref classfile.MemberInfo memberfInfo)
        {
            accessFlags = memberfInfo.AccessFlags();
            name = memberfInfo.Name();
            descriptor = memberfInfo.Descriptor();
        }

        public string Name()
        {
            return name;
        }
        public string Descriptor()
        {
            return descriptor;
        }
        public Class Class()
        {
            return clas;
        }

        internal bool isAccessibleTo(ref Class d)
        {
            if (IsPublic())
                return true;
            Class c = clas;
            if (IsProtected())
                return d == c || d.isSubClassOf( c) || c.getPackageName() == d.getPackageName();
            if (!IsPrivate())
                return c.getPackageName() == d.getPackageName();
            return d == c;
        }

        public ConstantPool ConstantPool()
        {
            return clas.ConstantPool();
        }
    }
}
