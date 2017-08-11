namespace jvmsharp.native.java.lang
{
    class Throwable
    {
        public static void init()
        {
            Registry.Register("java/lang/Throwable", "fillInStackTrace", "()Ljava/lang/String;", fillInStackTrace);
        }

        static void fillInStackTrace(ref rtda.Frame frame)
        {
            var thi = frame.LocalVars().GetThis();
            var interned = rtda.StringPool.InternString(ref thi);
            frame.OperandStack().PushRef(interned);
        }
    }
}
