namespace jvmsharp.instructions.math
{
    unsafe class INEG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int val = -stack.PopInt();
            stack.PushInt(val);
        }
    }

    unsafe class LNEG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long val = -stack.PopLong();
            stack.PushLong(val);
        }
    }

    unsafe class FNEG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float val = -stack.PopFloat();
            stack.PushFloat(val);
        }
    }

    unsafe class DNEG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            double val = -stack.PopDouble();
            stack.PushDouble(val);
        }
    }
}
