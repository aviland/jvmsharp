using jvmsharp.rtda;

namespace jvmsharp.native.sun.io
{
    class Win32ErrorMode : Native
    {
        public void init()
        {
            Registry.Register("sun/io/Win32ErrorMode", "setErrorMode", "(J)J", setErrorMode);
        }

        private void setErrorMode(ref Frame frame)
        {
            frame.OperandStack().PushLong(0);
        }
    }
}
