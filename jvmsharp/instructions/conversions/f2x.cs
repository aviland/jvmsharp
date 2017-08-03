using System;

namespace jvmsharp.instructions.conversions
{
    class F2I : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float f = stack.PopFloat();
            int i = Convert.ToInt32(f);
            stack.PushInt(i);
        }
    }

    class F2L : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float f = stack.PopFloat();
            long l = Convert.ToInt64(f);
            stack.PushFloat(l);
        }
    }

    class F2D : NoOperandsInstruction
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
