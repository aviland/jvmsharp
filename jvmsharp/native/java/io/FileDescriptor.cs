using jvmsharp.rtda;

namespace jvmsharp.native.java.io
{
    class FileDescriptor
    {
        public static void init()
        {
            Registry.Register("java/io/FileDescriptor", "set", "(I)J", set);
        }

        private static void set(ref Frame frame)
        {
            frame.OperandStack().PushLong(0);
        }
    }
}
