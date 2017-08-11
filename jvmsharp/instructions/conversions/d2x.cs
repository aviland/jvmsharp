using System;

namespace jvmsharp.instructions.conversions
{
    unsafe class D2F : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            double d = stack.PopDouble();
            float f = Convert.ToSingle(d);
            stack.PushFloat(f);
        }
    }

    unsafe class D2I : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            double d = stack.PopDouble();
            int i = Convert.ToInt32(d);
            stack.PushInt(i);
        }
    }

    unsafe class D2L : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            double d = stack.PopDouble();
            long i = Convert.ToInt64(d);
            stack.PushLong(i);
        }
    }
}
