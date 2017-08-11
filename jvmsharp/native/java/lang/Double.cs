using System;

namespace jvmsharp.native.java.lang
{
   unsafe class Double
    {
        public static void init()
        {
            Registry.Register("java/lang/Double", "doubleToRawLongBits", "(D)J", doubleToRawLongBits);
            Registry.Register("java/lang/Double", "longBitsToDouble", "(J)D", longBitsToDouble);
        }

        static void doubleToRawLongBits(ref rtda.Frame frame)
        {
            var vars = frame.LocalVars();
            double value = vars.GetDouble(0);
            long bits = BitConverter.ToInt64(BitConverter.GetBytes(value), 0);
            var stack = frame.OperandStack();
            stack.PushLong(bits);
        }

        static void longBitsToDouble(ref rtda.Frame frame)
        {
            var vars = frame.LocalVars();
            long bits = vars.GetLong(0);
            double value = BitConverter.ToDouble(BitConverter.GetBytes(bits), 0);
            var stack = frame.OperandStack();
            stack.PushDouble(value);
        }
    }
}
