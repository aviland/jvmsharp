using System;

namespace jvmsharp.instructions.conversions
{
    class L2I : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long l = stack.PopLong();
            int i = Convert.ToInt32(l);
            stack.PushInt(i);
        }
    }

    class L2F: NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long l = stack.PopLong();
            float f= Convert.ToSingle(l);
            stack.PushFloat(f);
        }
    }

    class L2D : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long l = stack.PopLong();
            double d = Convert.ToDouble(l);
            stack.PushDouble(d);
        }
    }
}
