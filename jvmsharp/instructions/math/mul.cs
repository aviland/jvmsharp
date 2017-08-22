namespace jvmsharp.instructions.math
{
     class IMUL : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int v2 = frame.OperandStack().PopInt();
            int v1 = frame.OperandStack().PopInt();
            int result = v1 * v2;
            frame.OperandStack().PushInt(result);
        }
    }

     class LMUL : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long v2 = stack.PopLong();
            long v1 = stack.PopLong();
            long result = v1 * v2;
            stack.PushLong(result);
        }
    }

    unsafe class FMUL : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float v2 = stack.PopFloat();
            float v1 = stack.PopFloat();
            float result = v1 * v2;
            stack.PushFloat(result);
        }
    }

    unsafe class DMUL : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            double v2 = stack.PopDouble();
            double v1 = stack.PopDouble();
            double result = v1 * v2;
            stack.PushDouble(result);
        }
    }
}
