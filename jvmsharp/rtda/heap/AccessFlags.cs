using System;

namespace jvmsharp.rtda.heap
{
    class AccessFlags
    {
        const UInt16 ACC_PUBLIC = 0x0001;
        const UInt16 ACC_PRIVATE = 0x0002;
        const UInt16 ACC_PROTECTED = 0x0004;
        const UInt16 ACC_STATIC = 0x0008;
        const UInt16 ACC_FINAL = 0x0010;
        const UInt16 ACC_SUPER = 0x0020;
        const UInt16 ACC_SYNCHRONIZED = 0x0020;
        const UInt16 ACC_VOLATILE = 0x0040;
        const UInt16 ACC_BRIDGE = 0x0040;
        const UInt16 ACC_TRANSIENT = 0x0080;
        const UInt16 ACC_VARARGS = 0x0080;
        const UInt16 ACC_NATIVE = 0x0100;
        const UInt16 ACC_INTERFACE = 0x0200;
        const UInt16 ACC_ABSTRACT = 0x0400;
        const UInt16 ACC_STRICT = 0x0800;
        const UInt16 ACC_SYNTHETIC = 0x1000;
        const UInt16 ACC_ANNOTATION = 0x2000;
        const UInt16 ACC_ENUM = 0x4000;

        protected UInt16 accessFlags;

        public UInt16 GetAccessFlags()
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
