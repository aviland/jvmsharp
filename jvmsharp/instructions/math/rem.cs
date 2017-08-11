using System;

namespace jvmsharp.instructions.math
{
    unsafe class DREM : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            double v2 = stack.PopDouble();
            double v1 = stack.PopDouble();
            double result = v1 % v2;
            stack.PushDouble(result);
        }
    }

    unsafe class FREM : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float v2 = stack.PopFloat();
            float v1 = stack.PopFloat();
            if (v2 == 0f)
            {
                throw new Exception("java.lang.ArithmeticException: / by zero");
            }
            float result = v1 % v2;
            stack.PushFloat(result);
        }
    }

    unsafe class IREM : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int v2 = stack.PopInt();
            int v1 = stack.PopInt();
            if (v2 == 0)
            {
                throw new Exception("java.lang.ArithmeticException: / by zero");
            }
            int result = v1 % v2;
            stack.PushInt(result);
        }
    }

   unsafe class LREM : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long v2 = stack.PopLong();
            long v1 = stack.PopLong();
            if (v2 == 0L)
            {
                throw new Exception("java.lang.ArithmeticException: / by zero");
            }
            long result = v1 % v2;
            stack.PushLong(result);
        }
    }
}
