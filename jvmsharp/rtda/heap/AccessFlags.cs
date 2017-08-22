namespace jvmsharp.rtda.heap
{
    class AccessFlags
    {
        internal const ushort ACC_PUBLIC = 0x0001;
        internal const ushort ACC_PRIVATE = 0x0002;
        internal const ushort ACC_PROTECTED = 0x0004;
        internal const ushort ACC_STATIC = 0x0008;
        internal const ushort ACC_FINAL = 0x0010;
        internal const ushort ACC_SUPER = 0x0020;
        internal const ushort ACC_SYNCHRONIZED = 0x0020;
        internal const ushort ACC_VOLATILE = 0x0040;
        internal const ushort ACC_BRIDGE = 0x0040;
        internal const ushort ACC_TRANSIENT = 0x0080;
        internal const ushort ACC_VARARGS = 0x0080;
        internal const ushort ACC_NATIVE = 0x0100;
        internal const ushort ACC_INTERFACE = 0x0200;
        internal const ushort ACC_ABSTRACT = 0x0400;
        internal const ushort ACC_STRICT = 0x0800;
        internal const ushort ACC_SYNTHETIC = 0x1000;
        internal const ushort ACC_ANNOTATION = 0x2000;
        internal const ushort ACC_ENUM = 0x4000;
    }
}
