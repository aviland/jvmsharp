namespace jvmsharp.instructions.math
{
  unsafe  class IDIV : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int v2 = stack.PopInt();
            int v1 = stack.PopInt();
            int result = v1 / v2;
            stack.PushInt(result);
        }
    }

    unsafe class LDIV : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long v2 = stack.PopLong();
            long v1 =stack.PopLong();
            long result = v1 / v2;
            stack.PushLong(result);
        }
    }

    unsafe class FDIV : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float v2 =stack.PopFloat();
            float v1 =stack.PopFloat();
            float result = v1 / v2;
            stack.PushFloat(result);
        }
    }

    unsafe class DDIV : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            double v2 = stack.PopDouble();
            double v1 = stack.PopDouble();
            double result = v1 / v2;
            stack.PushDouble(result);
        }
    }
}
