﻿namespace jvmsharp.instructions.math
{
     class IADD : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int v2 = stack.PopInt();
            int v1 = stack.PopInt();
            int result = v1 + v2;
            frame.OperandStack().PushInt(result);
        }
    }

     class LADD : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long v2 = stack.PopLong();
            long v1 = stack.PopLong();
            long result = v1 + v2;
            stack.PushLong(result);
        }
    }

      class FADD : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float v2 = stack.PopFloat();
            float v1 = stack.PopFloat();
            float result = v1 + v2;
            stack.PushFloat(result);
        }
    }

    class DADD : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            double v2 = stack.PopDouble();
            double v1 = stack.PopDouble();
            double result = v1 + v2;
            stack.PushDouble(result);
        }
    }
}
