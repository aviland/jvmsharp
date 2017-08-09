using System;

namespace jvmsharp.native.java.lang
{
    class Float
    {
        public static void init()
        {
            Registry.Register("java/lang/Float", "floatToRawIntBits", "(F)I", floatToRawIntBits);
        }

        static void floatToRawIntBits(ref rtda.Frame frame)
        {
            var vars = frame.LocalVars();
                float value = vars.GetFloat(0);
                int bits = Convert.ToInt32(value);
                var stack = frame.OperandStack();
                stack.PushInt(bits); // todo 
            
        }
    }
}
