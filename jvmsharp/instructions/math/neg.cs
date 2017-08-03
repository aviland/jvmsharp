namespace jvmsharp.instructions.math
{
    class INEG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int val = stack.PopInt();
            stack.PushInt(-val);
        }
    }

    class LNEG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long val = stack.PopLong();
            stack.PushLong(-val);
        }
    }

    class FNEG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float val = stack.PopFloat();
            stack.PushFloat(-val);
        }
    }

    class DNEG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            double val = stack.PopDouble();
            stack.PushDouble(-val);
        }
    }
}
