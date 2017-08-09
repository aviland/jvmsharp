using System;

namespace jvmsharp.native.java.lang
{
    class Double
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
            var bits = BitConverter.ToInt64(BitConverter.GetBytes(value), 0);
            var stack = frame.OperandStack();
            stack.PushLong(bits);
        }

        static void longBitsToDouble(ref rtda.Frame frame)
        {
            var vars = frame.LocalVars();
            var bits = vars.GetLong(0);
            var value = BitConverter.ToDouble(BitConverter.GetBytes(bits), 0);
            var stack = frame.OperandStack();
            stack.PushDouble(value);
        }

    }
}
