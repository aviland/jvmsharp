using System;

namespace jvmsharp.instructions.conversions
{
     class D2F : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            double d = stack.PopDouble();
            float f = Convert.ToSingle(d);
            frame.operandStack.PushFloat(f);
        }
    }

     class D2I : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            double d = stack.PopDouble();
            int i = Convert.ToInt32(d);
            frame.operandStack.PushInt(i);
        }
    }

     class D2L : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            double d = stack.PopDouble();
            long i = Convert.ToInt64(d);
            frame.operandStack.PushLong(i);
        }
    }
}
