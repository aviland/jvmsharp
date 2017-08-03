namespace jvmsharp.instructions.comparisons
{
    class LCMP : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long v2 = stack.PopLong();
            long v1 = stack.PopLong();
            if (v1 > v2)
                stack.PushInt(1);
            else if (v1 == v2)
                stack.PushInt(0);
            else
                stack.PushInt(-1);
        }
    }
}
