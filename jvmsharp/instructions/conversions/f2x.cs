using System;

namespace jvmsharp.instructions.conversions
{
 unsafe   class F2I : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float f = stack.PopFloat();
            int i = Convert.ToInt32(f);
            stack.PushInt(i);
        }
    }

    unsafe class F2L : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float f = stack.PopFloat();
            long l = Convert.ToInt64(f);
            stack.PushLong(l);
        }
    }

    unsafe class F2D : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float f = stack.PopFloat();
            double d = Convert.ToDouble(f);
            stack.PushDouble(d);
        }
    }
}
