namespace jvmsharp.rtda.heap
{
    class AccessFlags
    {
      internal  const ushort ACC_PUBLIC = 0x0001;
         const ushort ACC_PRIVATE = 0x0002;
         const ushort ACC_PROTECTED = 0x0004;
         const ushort ACC_STATIC = 0x0008;
         const ushort ACC_FINAL = 0x0010;
         const ushort ACC_SUPER = 0x0020;
         const ushort ACC_SYNCHRONIZED = 0x0020;
         const ushort ACC_VOLATILE = 0x0040;
         const ushort ACC_BRIDGE = 0x0040;
         const ushort ACC_TRANSIENT = 0x0080;
         const ushort ACC_VARARGS = 0x0080;
         const ushort ACC_NATIVE = 0x0100;
         const ushort ACC_INTERFACE = 0x0200;
         const ushort ACC_ABSTRACT = 0x0400;
         const ushort ACC_STRICT = 0x0800;
         const ushort ACC_SYNTHETIC = 0x1000;
         const ushort ACC_ANNOTATION = 0x2000;
         const ushort ACC_ENUM = 0x4000;

        internal ushort accessFlags;

        public ushort GetAccessFlags()
        {
            return accessFlags;
        }

        public bool IsPublic()
        {
            return 0 != (accessFlags & ACC_PUBLIC);
        }

        public bool IsPrivate()
        {
            return 0 != (accessFlags & ACC_PRIVATE);
        }

        public bool IsProtected()
        {
            return 0 != (accessFlags & ACC_PROTECTED);
        }

        public bool IsStatic()
        {
            return 0 != (accessFlags & ACC_STATIC);
        }

        public bool IsFinal()
        {
            return 0 != (accessFlags & ACC_FINAL);
        }

        public bool IsSuper()
        {
            return 0 != (accessFlags & ACC_SUPER);
        }

        public bool IsSynchronized()
        {
            return 0 != (accessFlags & ACC_SYNCHRONIZED);
        }

        public bool IsVolatile()
        {
            return 0 != (accessFlags & ACC_VOLATILE);
        }

        public bool IsBridge()
        {
            return 0 != (accessFlags & ACC_BRIDGE);
        }

        public bool IsTransient()
        {
            return 0 != (accessFlags & ACC_TRANSIENT);
        }

        public bool IsVarargs()
        {
            return 0 != (accessFlags & ACC_VARARGS);
        }

        public bool IsNative()
        {
            return 0 != (accessFlags & ACC_NATIVE);
        }

        public bool IsInterface()
        {
            return 0 != (accessFlags & ACC_INTERFACE);
        }

        public bool IsAbstract()
        {
            return 0 != (accessFlags & ACC_ABSTRACT);
        }

        public bool IsStrict()
        {
            return 0 != (accessFlags & ACC_STRICT);
        }

        public bool IsSynthetic()
        {
            return 0 != (accessFlags & ACC_SYNTHETIC);
        }

        public bool IsAnnotation()
        {
            return 0 != (accessFlags & ACC_ANNOTATION);
        }

        public bool IsEnum()
        {
            return 0 != (accessFlags & ACC_ENUM);
        }
    }
}
